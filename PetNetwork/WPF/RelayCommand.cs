using System.Windows.Input;

namespace PetNetwork.WPF;

public class RelayCommand : ICommand
{
    private readonly Action<object> _execute;
    private readonly Predicate<object>? _canExecute;

    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    public RelayCommand(Action<object> execute, Predicate<object>? canExecute = null)
    {
        _execute = execute ?? throw new NullReferenceException("Method given to command cannot be null");
        _canExecute = canExecute;
    }

    public RelayCommand(Action<object> execute) : this(execute, null) { }


    public bool CanExecute(object? parameter = null) => _canExecute == null || _canExecute(parameter);

    public void Execute(object? parameter = null) => _execute.Invoke(parameter);
}
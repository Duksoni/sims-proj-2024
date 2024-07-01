using PetNetwork.WPF.ViewModels;

namespace PetNetwork.WPF.Views.Windows;

/// <summary>
/// Interaction logic for AssignPaymentToPetView.xaml
/// </summary>
public partial class AssignPaymentToPetView
{
    public AssignPaymentToPetView(PaymentViewModel selectedPayment)
    {
        InitializeComponent();
        var viewModel = new AssignPaymentToPetViewModel(selectedPayment);
        DataContext = viewModel;
        viewModel.PropertyChanged += (_, args) => {
            if (args.PropertyName == nameof(viewModel.Assigned))
                DialogResult = true;
        };
    }
}
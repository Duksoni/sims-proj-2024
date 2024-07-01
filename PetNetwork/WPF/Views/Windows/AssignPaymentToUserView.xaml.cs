using PetNetwork.WPF.ViewModels;

namespace PetNetwork.WPF.Views.Windows;

/// <summary>
/// Interaction logic for AssignPaymentToUserView.xaml
/// </summary>
public partial class AssignPaymentToUserView
{
    public AssignPaymentToUserView(PaymentViewModel selectedPayment)
    {
        InitializeComponent();
        var viewModel = new AssignPaymentToUserViewModel(selectedPayment);
        DataContext = viewModel;
        viewModel.PropertyChanged += (_, args) => {
            if (args.PropertyName == nameof(viewModel.Assigned))
                DialogResult = true;
        };
    }
}
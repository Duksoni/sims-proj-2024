using PetNetwork.WPF.ViewModels;
using System.Diagnostics;
using System.Windows.Navigation;

namespace PetNetwork.WPF.Views.Windows;

/// <summary>
/// Interaction logic for PaymentImportView.xaml
/// </summary>
public partial class PaymentImportView
{
    public PaymentImportView()
    {
        var viewModel = new PaymentImportViewModel();
        InitializeComponent();
        DataContext = viewModel;
        AssignToUserBtn.Command = new RelayCommand(_ => 
            { new AssignPaymentToUserView(viewModel.SelectedPayment!).ShowDialog(); },
            _ => viewModel.SelectedPayment != null);

        viewModel.PropertyChanged += (_, args) => {
            if (args.PropertyName == nameof(viewModel.ImportsSaved))
                DialogResult = true;
        };
    }

    private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
    {
        Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri) { UseShellExecute = true });
        e.Handled = true;
    }
}
using PetNetwork.Domain.Enums;
using PetNetwork.WPF.ViewModels;
using PetNetwork.WPF.Views.Windows;

namespace PetNetwork.WPF.Views.UserControls;

/// <summary>
/// Interaction logic for AllPaymentsView.xaml
/// </summary>
public partial class AllPaymentsView
{
    public AllPaymentsView()
    {
        InitializeComponent();
        var viewModel = new AllPaymentsViewModel();
        DataContext = viewModel;

        ImportPaymentsBtn.Click += (_, _) =>
        {
            var importedPayments = new PaymentImportView().ShowDialog();
            if (importedPayments != true) return;
            viewModel.SetTable();
            
        };

        AddDonationBtn.Click += (_, _) =>
        {
            var addedSuccessfuly = new AddDonationView().ShowDialog();
            if (addedSuccessfuly != true) return;
            viewModel.SetTable();
        };

        AssignToPetBtn.Command = new RelayCommand(_ => 
            { new AssignPaymentToPetView(viewModel.SelectedPayment!).ShowDialog(); },
            _ => viewModel.SelectedPayment is { PaymentType: PaymentType.ForPet});
    }
}
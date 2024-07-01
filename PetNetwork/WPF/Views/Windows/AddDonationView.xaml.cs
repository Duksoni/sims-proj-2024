using PetNetwork.WPF.ViewModels;
using System.Windows.Controls;

namespace PetNetwork.WPF.Views.Windows;

/// <summary>
/// Interaction logic for AddDonationView.xaml
/// </summary>
public partial class AddDonationView
{
    public AddDonationView()
    {
        InitializeComponent();
        var viewModel = new AddDonationViewModel();
        DataContext = viewModel;

        viewModel.PropertyChanged += (_, args) => {
            if (args.PropertyName == nameof(viewModel.Submitted))
                DialogResult = true;
        };

        PaymentDatePicker.DisplayDateEnd = DateTime.Today;
        PaymentDatePicker.DisplayDateStart = DateTime.Parse("2024-06-25");
        PaymentDatePicker.SelectedDate = DateTime.Now;
        PaymentDatePicker.SelectedDateChanged += (sender, _) => { TrySettingPaymentDate(sender as DatePicker); };
    }

    private static void TrySettingPaymentDate(DatePicker? datePicker)
    {
        if (datePicker == null) return;

        if (datePicker.SelectedDate < DateTime.Parse("2024-06-25") || datePicker.SelectedDate > DateTime.Today)
            datePicker.SelectedDate = DateTime.Today;

        if (datePicker.SelectedDate != null) return;
        datePicker.SelectedDate = DateTime.Now;
    }
}
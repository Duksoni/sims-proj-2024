using PetNetwork.WPF.ViewModels;
using System.Windows;

namespace PetNetwork.WPF.Views.Windows
{
    /// <summary>
    /// Interaction logic for AddPetView.xaml
    /// </summary>
    public partial class AddPetView : Window
    {
        public AddPetView()
        {
            InitializeComponent();
            var viewModel = new AddPetViewModel();
            DataContext = viewModel;

            viewModel.PropertyChanged += (_, args) =>
            {
                if (args.PropertyName == nameof(viewModel.Added))
                    DialogResult = true;
            };
        }
    }
}

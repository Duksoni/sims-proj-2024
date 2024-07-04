using PetNetwork.WPF.ViewModels;
using System.Windows;

namespace PetNetwork.WPF.Views.Windows
{
    /// <summary>
    /// Interaction logic for UpdatePetView.xaml
    /// </summary>
    public partial class UpdatePetView : Window
    {
        public UpdatePetView(PetViewModel SelectedPet)
        {
            InitializeComponent();
            var viewModel = new UpdatePetViewModel(SelectedPet);
            DataContext = viewModel;

            viewModel.PropertyChanged += (_, args) => {
                if (args.PropertyName == nameof(viewModel.Updated))
                    DialogResult = true;
            };
        }
    }
}

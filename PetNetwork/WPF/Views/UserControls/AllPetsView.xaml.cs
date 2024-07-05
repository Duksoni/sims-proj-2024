using PetNetwork.WPF.ViewModels;
using PetNetwork.WPF.Views.Windows;

namespace PetNetwork.WPF.Views.UserControls
{
    /// <summary>
    /// Interaction logic for AllPetsView.xaml
    /// </summary>
    public partial class AllPetsView
    {
        public AllPetsView()
        {
            InitializeComponent();
            var viewModel = new AllPetsViewModel(this);
            DataContext = viewModel;
        }
    }
}

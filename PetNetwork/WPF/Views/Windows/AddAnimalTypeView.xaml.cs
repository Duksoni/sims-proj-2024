using PetNetwork.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PetNetwork.WPF.Views.Windows
{
    /// <summary>
    /// Interaction logic for AddAnimalTypeView.xaml
    /// </summary>
    public partial class AddAnimalTypeView : Window
    {
        public AddAnimalTypeView()
        {
            InitializeComponent();
            var viewModel = new AddAnimalTypeViewModel();
            DataContext = viewModel;

            viewModel.PropertyChanged += (_, args) => {
                if (args.PropertyName == nameof(viewModel.Added))
                    DialogResult = true;
            };
        }
    }
}

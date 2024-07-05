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
    /// Interaction logic for RemoveAnimalTypeView.xaml
    /// </summary>
    public partial class RemoveAnimalTypeView : Window
    {
        public RemoveAnimalTypeView()
        {
            InitializeComponent();
            var viewModel = new RemoveAnimalTypeViewModel();
            DataContext = viewModel;

            viewModel.PropertyChanged += (_, args) => {
                if (args.PropertyName == nameof(viewModel.Removed))
                    DialogResult = true;
            };
        }
    }
}

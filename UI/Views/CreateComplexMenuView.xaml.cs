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
using BusinessLogic.Services.Interfaces;
using Lab4.ViewModels;
using MenuManager.DB;

namespace Lab4.Views
{
    /// <summary>
    /// Interaction logic for CreateComplexMenuView.xaml
    /// </summary>
    public partial class CreateComplexMenuView : Window
    {
        public CreateComplexMenuView(IDishService service)
        {
            InitializeComponent();
            DataContext = new CreateComplexMenuViewModel(service);
        }
    }
}

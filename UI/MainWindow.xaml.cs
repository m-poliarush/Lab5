using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BusinessLogic.Profiles;
using BusinessLogic.Services.Interfaces;
using BusinessLogic.Services;
using DomainData.UoW;
using Lab4.ViewModels;
using MenuManager.DB;
using Microsoft.Extensions.DependencyInjection;

namespace Lab4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

        }
    }
}
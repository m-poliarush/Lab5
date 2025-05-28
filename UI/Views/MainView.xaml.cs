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
using BusinessLogic.Profiles;
using BusinessLogic.Services.Interfaces;
using BusinessLogic.Services;
using DomainData.UoW;
using Lab4.ViewModels;
using MenuManager.DB;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Lab4.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {

        public IServiceProvider serviceProvider { get; private set; }

        public MainView()
        {
            var services = new ServiceCollection();

            services.AddScoped<MenuContext>();

            services.AddAutoMapper(typeof(DailyMenuProfile).Assembly);

            services.AddScoped<IDailyMenuService, DailyMenuService>();
            services.AddScoped<IDishService, DishService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<MainViewModel>();

            serviceProvider = services.BuildServiceProvider();

            DataContext = serviceProvider.GetRequiredService<MainViewModel>();

            InitializeComponent();

            
        }
    }
}

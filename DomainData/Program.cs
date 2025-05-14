// See https://aka.ms/new-console-template for more information
using System.Globalization;
using MenuManager.DB;
using MenuManager.DB.Models;
using MenuManager.Repository.DailyMenusRepository;
using MenuManager.Repository.DishesRepository;
using MenuManager.Repository.OrdersRepository;


Console.WriteLine("Hello, World!");


MenuContext c = new MenuContext();  
DailyMenusRepository dmr = new DailyMenusRepository(c);
DishesRepository dishes = new DishesRepository(c);
OrdersRepository orders = new OrdersRepository(c);
var day = dmr.GetMenuByID(2);
Console.WriteLine();
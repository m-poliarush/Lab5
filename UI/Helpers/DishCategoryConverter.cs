using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using MenuManager.DB.Models;

namespace Lab4.Helpers
{
    public class DishStatusToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DishCategory category)
            {
                switch (category)
                {
                    case DishCategory.First: return "Перша страва";
                    case DishCategory.Side: return "Гарнір";
                    case DishCategory.Main: return "Основна страва";
                    case DishCategory.Complex: return "Комплексне меню";
                    default: return category.ToString();
                }
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

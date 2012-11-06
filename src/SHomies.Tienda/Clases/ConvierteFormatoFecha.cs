using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Globalization;
using SHomies.Utilitario;

namespace SHomies.Tienda.Clases
{
    public class ConvierteFormatoFecha : IValueConverter
    {
        public object Convert(object value,
                          Type targetType,
                          object parameter,
                          CultureInfo culture)
        {

            return value == null ? string.Empty : Funcion.ConvertTo<DateTime>(value).ToShortDateString();
        }
        public object ConvertBack(object value, Type targetType, object parameter,
        CultureInfo culture)
        {
            throw new NotSupportedException("Error Al convertir no aceptado para este grupo");
        }
    }
}

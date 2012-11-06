using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Globalization;
using SHomies.Utilitario;

namespace SHomies.Tienda.Clases
{
    public class ConvierteFormatoDecimal : IValueConverter
    {
        public object Convert(object value,
                          Type targetType,
                          object parameter,
                          CultureInfo culture)
        {

            return value == null ? Funcion.FormatoDecimal(0) : Funcion.FormatoDecimal((decimal)value);
        }
        public object ConvertBack(object value, Type targetType, object parameter,
        CultureInfo culture)
        {
            throw new NotSupportedException("Error Al convertir no aceptado para este grupo");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Globalization;

namespace SHomies.Tienda.Clases
{
    public class ConvierteEstadoToImagen : IValueConverter
    {
        public object Convert(object value,
                          Type targetType,
                          object parameter,
                          CultureInfo culture)
        {
            int state = Utilitario.Funcion.ConvertTo<int>(value);
            if (state == 0)
            {
                return "/Images/inactivo.png";
            }
            else if (state == 1)
            {
                return "/Images/activo.png";
            }
            else if (state == 2)
            {
                return "/Images/bloqueado.png";
            }
            else
            {
                return "/Images/bloqueado.png";
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter,
        CultureInfo culture)
        {
            throw new NotSupportedException("Error Al convertir no aceptado para este grupo");
        }
    }
    public class ConvierteEstadoToCadena : IValueConverter
    {
        public object Convert(object value,
                          Type targetType,
                          object parameter,
                          CultureInfo culture)
        {
            int state = Utilitario.Funcion.ConvertTo<int>(value);
            if (state == 0)
            {
                return "Inactivo";
            }
            else if (state == 1)
            {
                return "Activo";
            }
            else if (state == 2)
            {
                return "Bloqueado";
            }
            else
            {
                return "Indefinido";
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter,
        CultureInfo culture)
        {
            throw new NotSupportedException("Error Al convertir no aceptado para este grupo");
        }
    }
}

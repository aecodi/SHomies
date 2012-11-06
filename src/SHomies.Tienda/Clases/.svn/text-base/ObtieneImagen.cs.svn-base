using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Globalization;
using System.Windows;
using System.Windows.Media;

namespace SHomies.Tienda.Clases
{
    public class ObtieneImagen 
    {
        public static readonly DependencyProperty ImageProperty;

        static ObtieneImagen()
        {
            var metadata = new FrameworkPropertyMetadata((ImageSource) null);
            ImageProperty = DependencyProperty.RegisterAttached("Image",
                                                              typeof(ImageSource),
                                                              typeof(ObtieneImagen),
                                                              metadata);
                        
        }
        public static ImageSource GetImage(DependencyObject iObjeto)
        {
            return (ImageSource)iObjeto.GetValue(ImageProperty);
        }

        public static void SetImage(DependencyObject iObjeto, ImageSource iValue)
        {            
            iObjeto.SetValue(ImageProperty, iValue);
        }
    }
}

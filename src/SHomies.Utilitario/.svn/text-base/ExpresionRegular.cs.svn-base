using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SHomies.Utilitario
{
    public class ExpresionRegular
    {
        public static bool IsNumeric(string cadena)
        {
            bool response = false;
            if (Regex.IsMatch(cadena, "^[0-9]+$"))
                response = true;
            return response;
        }
        public static bool IsNumericAndCharacter(string cadena)
        {
            bool response = false;
            if (Regex.IsMatch(cadena, "^[a-zA-Z0-9]+$"))
                response = true;
            return response;
        }
        public static bool IsCharacter(string cadena)
        {
            bool response = false;
            if (Regex.IsMatch(cadena, "^[a-zA-Z]+$"))
                response = true;
            return response;
        }
        public static bool IsCharactersInName(string cadena)
        {
            bool response = false;
            if (Regex.IsMatch(cadena, @"^[a-zA-Z\sñÑÁÉÍÓÚÜ]+$"))
                response = true;
            return response;

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DemoWebApp.BusinessRules
{
    public static class IsbnValidator
    {
        public static bool Validate(string isbnNumber)
        {
            return Regex.IsMatch(isbnNumber, @"^[\d\s]*$");
        }
    }
}

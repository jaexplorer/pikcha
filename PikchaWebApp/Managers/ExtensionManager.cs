using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PikchaWebApp.Managers
{
    public static class ExtensionManager
    {
        // to copy proerties from another object
        public static void CopyPropertiesFrom(this object self, object parent)
        {
            var fromProperties = parent.GetType().GetProperties();
            var toProperties = self.GetType().GetProperties();

            foreach (var fromProperty in fromProperties)
            {
                foreach (var toProperty in toProperties)
                {
                    if (fromProperty.Name == toProperty.Name && fromProperty.PropertyType == toProperty.PropertyType)
                    {
                        toProperty.SetValue(self, fromProperty.GetValue(parent));
                        break;
                    }
                }
            }
        }


        public static string ToHumanReadableNumber(this int self)
        {
            string[] readableFormats = new string[]
                    {" k", " k", " m", " b"}; // even less than 1000 -> indicate in thousands

           int index = 0;
            double valueToConvert = self;           
            if(valueToConvert <= 1000)
            {
                valueToConvert /= 1000;
            }
            else
            {
                while (valueToConvert > 1000)
                {
                    valueToConvert /= 1000;
                    index++;
                }
            }
                    
            return ("" + Math.Round(valueToConvert, 2) + readableFormats[index]);
            
        }

      

    }
}

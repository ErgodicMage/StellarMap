using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace StellarMap.Traveller.Parsers
{
    public static class BorderParser
    {
        public static string HexRegex = @"^\d{4}$";
        public static Border ParseBorder(string val)
        {
            var values = val.Split(null as char[]);

            if (values is null)
                return null;

            Border border = new Border();

            foreach (string v in values)
            {
                if (Regex.IsMatch(v, HexRegex))
                {
                    border.Positions.Add(new Hex(v));
                }
                else
                {
                    border.Color = v;
                    break;
                }
            }

            return border;
        }
    }
}

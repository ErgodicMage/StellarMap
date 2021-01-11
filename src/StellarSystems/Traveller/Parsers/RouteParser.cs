using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace StellarMap.Traveller.Parsers
{
    public static class RouteParser
    {
        public const string OffsetRegex = @"^[-+]?[01]$";
        public static Route Parse(string value)
        {
            var values = value.Split(null as char[], StringSplitOptions.RemoveEmptyEntries);

            if (values != null && values.Length < 2)
                return null;

            Route route = new Route();

            int cnt = 0;

            if (Regex.IsMatch(values[cnt], OffsetRegex))
                route.StartOffsetX = ParserUtilities.ParseShort(values[cnt++]);
            if (Regex.IsMatch(values[cnt], OffsetRegex))
                route.StartOffsetY = ParserUtilities.ParseShort(values[cnt++]); 
            route.Start = new Hex(values[cnt++]);
            if (Regex.IsMatch(values[cnt], OffsetRegex))
                route.EndOffsetX = ParserUtilities.ParseShort(values[cnt++]);
            if (Regex.IsMatch(values[cnt], OffsetRegex))
                route.EndOffsetY = ParserUtilities.ParseShort(values[cnt++]); 
            route.End = new Hex(values[cnt++]);

            if (cnt > value.Length)
                route.Color = values[cnt];

            return route;
        }
    }
}

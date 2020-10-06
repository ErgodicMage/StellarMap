using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace StellarMap.Traveller.Parsers
{
    public class WorldSECParser
    {
        #region Regex 
        // for generic parsing of sec world text. Should accomodate all legacy sec file formats
        public static readonly Regex worldRegex = new Regex(@"^" +
            @"( \s*             (?<name>        [^\s.](.*?[^\+\s.])?  ) )? \+?\.* " +    // Name
            @"( \s*             (?<hex>         \d\d\d\d              ) )      " +    // Hex
            @"( \s+             (?<uwp>         \w{7}-\w              ) )      " +    // UWP (Universal World Profile)
            @"( \s+             (?<base>        \w | \*               ) )?     " +    // Base
            @"( \s{1,3}         (?<codes>       .{10,}?               ) )      " +    // Codes
            @"( \s+             (?<zone>        \w                    ) )?     " +    // Zone
            @"( \s+             (?<pbg>         \d[0-9A-F][0-9A-F]    ) )      " +    // PGB (Population multiplier, Belts, Gas giants)
            @"( \s+  (\w\w\/)?  (?<allegiance>  (\w\w\b|\w-|--)       ) )      " +    // Allegiance
            @"( \s*             (?<stellar>     .*                    ) )      "        // Stellar data (etc)
            , RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.Singleline | RegexOptions.ExplicitCapture | RegexOptions.IgnorePatternWhitespace);
        #endregion

        public World ParseWorld(TravellerMap map, string line)
        {
            Match worldMatch = worldRegex.Match(line);

            if (!worldMatch.Success)
                return null;
            string name = worldMatch.Groups["name"].Value;

            string h = worldMatch.Groups["hex"].Value;
            Hex hex = new Hex(h);

            string uwp = worldMatch.Groups["uwp"].Value;

            World world = new World(name, hex, uwp);
            map.Add<World>(world);

            world.Base = worldMatch.Groups["base"].Value;
            world.Codes = worldMatch.Groups["codes"].Value;
            world.Zone = worldMatch.Groups["zone"].Value;
            world.PBG = worldMatch.Groups["pbg"].Value;
            world.Allegiance = worldMatch.Groups["allegiance"].Value;
            world.StellarData = worldMatch.Groups["stellar"].Value;

            return world;
        }
    }
}

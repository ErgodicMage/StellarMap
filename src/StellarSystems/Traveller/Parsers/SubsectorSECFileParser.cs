using System.IO;

namespace StellarMap.Traveller.Parsers
{
    public class SubsectorSECFileParser
    {
        public Subsector ParseSubsector(TravellerMap map, string name, TextReader reader)
        {
            Subsector subsector = new Subsector(name);
            map.Add<Subsector>(subsector);

            WorldSECParser worldParser = new WorldSECParser();

            string line = null;
            int linecount = 0;
            while ((line = reader.ReadLine()) != null)
            {
                linecount++;
                if (linecount < 13)
                    continue;

                World world = worldParser.ParseWorld(map, line);

                subsector.Add(world);
            }

            return subsector;
        }
    }
}

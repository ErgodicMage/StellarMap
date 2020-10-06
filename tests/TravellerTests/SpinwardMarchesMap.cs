using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using StellarMap.Core.Bodies;
using StellarMap.Core.Bodies;

using StellarMap.Traveller;
using StellarMap.Traveller.Parsers;

namespace TravellerTests
{
    public class SpinwardMarchesMap
    {
        public SpinwardMarchesMap(TravellerMap map)
        {
            Map = map;

            map.MetaData.Add("Basic", "Author", "Ergodic Mage");
            map.MetaData.Add("Basic", "Version", "0.1");
            map.MetaData.Add("Basic", "Date", "10/4/2020");
            map.MetaData.Add("Basic", "Description", "This is the initial version of the Spinward Marches map for testing purposes.");
        }

        TravellerMap Map { get; set; }

        public World CreateAramisWorld()
        {
            WorldSECParser parser = new WorldSECParser();
            World aramis = parser.ParseWorld(Map, "Aramis        3110 A5A0556-B  A He Ni Cp           710 Im M2 V           ");

            //Map.Add<World>(aramis);

            return aramis;
        }

        public Subsector CreateAramisSubsector()
        {
            //Subsector aramissubsector = new Subsector("Aramis");
            //Map.Add<Subsector>(aramissubsector);

            //World aramisworld = CreateAramisWorld();
            //aramissubsector.Add(aramisworld);

            //return aramissubsector;

            string text = TestingUtilities.ReadResource("Files", "AramisSubsector.sec");
            StringReader reader = new StringReader(text);

            SubsectorSECFileParser parser = new SubsectorSECFileParser();
            Subsector aramis = parser.ParseSubsector(Map, "Aramis", reader);

            return aramis;
        }
    }
}

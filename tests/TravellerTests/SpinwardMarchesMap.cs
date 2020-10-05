using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using StellarMap.Core.Bodies;
using StellarMap.Core.Bodies;

using StellarMap.Traveller;

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
            Hex hex = new Hex(25, 40);
            World aramis = new World("Aramis", hex, "B659772-6");
            aramis.Bases = "A";
            aramis.Codes = "He Ni Cp";
            aramis.Zone = string.Empty;
            aramis.PBG = "710";
            aramis.Allegiance = "Im";
            aramis.StellarData = "M2 V";

            Map.Add<World>(aramis);

            return aramis;
        }

        public Subsector CreateAramisSubsector()
        {
            Subsector aramissubsector = new Subsector("Aramis");
            Map.Add<Subsector>(aramissubsector);

            World aramisworld = CreateAramisWorld();
            aramissubsector.Add(aramisworld);

            return aramissubsector;
        }
    }
}

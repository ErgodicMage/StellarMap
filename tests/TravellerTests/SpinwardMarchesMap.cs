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

        public Sector CreateSector()
        {
            Sector sector = new Sector("Spinward Marches");
            Map.Add<Sector>(sector);

            Subsector sub = CreateSubsector("Cronor");
            sector.Add(sub);

            sub = CreateSubsector("Jewell");
            sector.Add(sub);

            sub = CreateSubsector("Regina");
            sector.Add(sub);

            sub = CreateSubsector("Aramis");
            sector.Add(sub);

            sub = CreateSubsector("Querion");
            sector.Add(sub);

            sub = CreateSubsector("Vilis");
            sector.Add(sub);

            sub = CreateSubsector("Lanth");
            sector.Add(sub);

            sub = CreateSubsector("Rhylanor");
            sector.Add(sub);

            sub = CreateSubsector("Darrian");
            sector.Add(sub);

            sub = CreateSubsector("Sword Worlds");
            sector.Add(sub);

            sub = CreateSubsector("Lunion");
            sector.Add(sub);

            sub = CreateSubsector("Mora");
            sector.Add(sub);

            sub = CreateSubsector("Five Sisters");
            sector.Add(sub);

            sub = CreateSubsector("District 268");
            sector.Add(sub);

            sub = CreateSubsector("Glisten");
            sector.Add(sub);

            sub = CreateSubsector("Trin's Veil");
            sector.Add(sub);

            return sector;
        }

        public World CreateAramisWorld()
        {
            WorldSECParser parser = new WorldSECParser();
            World aramis = parser.ParseWorld(Map, "Aramis        3110 A5A0556-B  A He Ni Cp           710 Im M2 V           ");

            return aramis;
        }

        public Subsector CreateSubsector(string name)
        {
            string text = TestingUtilities.ReadResource("Files", $"{name}.sec");
            StringReader reader = new StringReader(text);

            SubsectorSECFileParser parser = new SubsectorSECFileParser();
            Subsector subsector = parser.ParseSubsector(Map, name, reader);

            return subsector;
        }

        public Subsector CreateAramisSubsector()
        {
            string text = TestingUtilities.ReadResource("Files", "Aramis.sec");
            StringReader reader = new StringReader(text);

            SubsectorSECFileParser parser = new SubsectorSECFileParser();
            Subsector aramis = parser.ParseSubsector(Map, "Aramis", reader);

            return aramis;
        }

        public Subsector CreateCronorSubsector()
        {
            string text = TestingUtilities.ReadResource("Files", "Cronor.sec");
            StringReader reader = new StringReader(text);

            SubsectorSECFileParser parser = new SubsectorSECFileParser();
            Subsector aramis = parser.ParseSubsector(Map, "Cronor", reader);

            return aramis;
        }
    }
}

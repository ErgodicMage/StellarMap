namespace TravellerTests;

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
        sector.Add("A", sub);

        sub = CreateSubsector("Jewell");
        sector.Add("B", sub);

        sub = CreateSubsector("Regina");
        sector.Add("C", sub);

        sub = CreateSubsector("Aramis");
        sector.Add("D", sub);

        sub = CreateSubsector("Querion");
        sector.Add("E", sub);

        sub = CreateSubsector("Vilis");
        sector.Add("F", sub);

        sub = CreateSubsector("Lanth");
        sector.Add("G", sub);

        sub = CreateSubsector("Rhylanor");
        sector.Add("H", sub);

        sub = CreateSubsector("Darrian");
        sector.Add("I", sub);

        sub = CreateSubsector("Sword Worlds");
        sector.Add("J", sub);

        sub = CreateSubsector("Lunion");
        sector.Add("K", sub);

        sub = CreateSubsector("Mora");
        sector.Add("L", sub);

        sub = CreateSubsector("Five Sisters");
        sector.Add("M", sub);

        sub = CreateSubsector("District 268");
        sector.Add("N", sub);

        sub = CreateSubsector("Glisten");
        sector.Add("O", sub);

        sub = CreateSubsector("Trin's Veil");
        sector.Add("P", sub);

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

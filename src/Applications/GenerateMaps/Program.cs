using Microsoft.Extensions.Configuration;

namespace StellarMap.GenerateMaps;

static class Program
{
    static void Main()
    {
        IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            //.AddEnvironmentVariables()
            .Build();

        foreach (var kvp in config.AsEnumerable())
            Console.WriteLine(kvp.Key + ":" + kvp.Value);

        dataDir = config["DataPath"];

        LocateStarsInCube(20);

        JsonGenerateLocalSector();
        JsonRetrieveLocalSector();

        ZipGenerateLocalSector();
        ZipRetrieveLocalSector();

        StoreSolarSystem();
        RetrieveSolarSystem();

        Console.WriteLine("Complete");
    }

    static string dataDir;

    public static void JsonGenerateLocalSector()
    {
        ProgressionMap localsector = new ProgressionMap("Local Sector");

        LocalSectorMap create = new LocalSectorMap(localsector);
        create.CreateLocalSector();

        IMapStorage store = MapStorageFactory.GetStorage(MapStorageFactory.JsonStorage);

        string filename = Path.Combine(dataDir, "LocalSector.json");

        if (File.Exists(filename))
            File.Delete(filename);

        using StreamWriter writer = new StreamWriter(filename);
        store.Store(localsector, writer);
    }

    public static void JsonRetrieveLocalSector()
    {
        string filename = Path.Combine(dataDir, "LocalSector.json");

        IStellarMap map;
        IMapStorage store = MapStorageFactory.GetStorage(MapStorageFactory.JsonStorage);

        using StreamReader reader = new StreamReader(filename);
        map = store.Retreive<ProgressionMap>(reader);

        Console.WriteLine(map.ToString());
    }

    public static void ZipGenerateLocalSector()
    {
        ProgressionMap localsector = new ProgressionMap("Local Sector");

        LocalSectorMap create = new LocalSectorMap(localsector);
        create.CreateLocalSector();

        IMapStorage store = MapStorageFactory.GetStorage(MapStorageFactory.ZipStorage);

        string filename = Path.Combine(dataDir, "LocalSector.zip");

        if (File.Exists(filename))
            File.Delete(filename);

        using StreamWriter writer = new StreamWriter(filename);
        store.Store(localsector, writer);
    }

    public static void ZipRetrieveLocalSector()
    {
        string filename = Path.Combine(dataDir, "LocalSector.zip");

        IStellarMap map;
        IMapStorage store = MapStorageFactory.GetStorage(MapStorageFactory.ZipStorage);

        using StreamReader reader = new StreamReader(filename);
            map = store.Retreive<ProgressionMap>(reader);


        Console.WriteLine(map.ToString());
    }
    public static void StoreSolarSystem()
    {
        IStellarMap map = new BaseStellarMap("SolarSystem");
        PhysicalSolarSystemCreator.CreateSolarSystem(map);

        IMapStorage store = MapStorageFactory.GetStorage(MapStorageFactory.JsonStorage);

        string filename = Path.Combine(dataDir, "SolarSystem.json");

        if (File.Exists(filename))
            File.Delete(filename);

        using StreamWriter writer = new StreamWriter(filename);
        store.Store(map, writer);
    }

    public static void RetrieveSolarSystem()
    {
        string filename = Path.Combine(dataDir, "SolarSystem.json");

        IStellarMap map;
        IMapStorage store = MapStorageFactory.GetStorage(MapStorageFactory.JsonStorage);

        using StreamReader reader = new StreamReader(filename);
        map = store.Retreive<BaseStellarMap>(reader);

        Console.WriteLine(map.ToString());
    }

    public static void LocateStarsInCube(int ly)        
    {
        string outfile = Path.Combine(dataDir, "AreasForStars_" + ly.ToString() + "LY.txt");
        string catalogueFile = Path.Combine(dataDir, "HabHYG.csv");
            
        HabHYGCsvReader reader = new HabHYGCsvReader();
        reader.Load(catalogueFile);

        var records = reader.Catalogue.Where<HabHygRecord>(c => c.Distance < ((1.5 * ly) / 3.261633));
        IList<HabHygRecord> stars = records.ToList<HabHygRecord>();

        CubeAreaStarLocation areaLocations = new CubeAreaStarLocation();
        IDictionary<string, IList<HabHygRecord>> areaMappings = areaLocations.GetAreaMappings(ly, stars);

        if (File.Exists(outfile))
            File.Delete(outfile);

        using StreamWriter sw = new StreamWriter(outfile);
        foreach (var area in areaMappings)
        {
            sw.Write("Area ");
            sw.Write(area.Key);
            sw.WriteLine(":");

            foreach (HabHygRecord rec in area.Value)
            {
                sw.Write(rec.DisplayName);
                sw.Write(" -- (");
                sw.Write(rec.Xg);
                sw.Write(",");
                sw.Write(rec.Yg);
                sw.Write(",");
                sw.Write(rec.Zg);
                sw.WriteLine(")");
            }
            sw.WriteLine("-------");
        }
    }
}

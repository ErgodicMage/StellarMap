using System.Collections;
using System.Runtime.Serialization;
using System.IO;

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
        dataDir = config["DataPath"];

        //foreach (var kvp in config.AsEnumerable())
        //    Console.WriteLine(kvp.Key + ":" + kvp.Value);

        //dataDir = config["DataPath"];

        //LocateStarsInCube(20);

        //JsonGenerateLocalSector();
        //JsonRetrieveLocalSector();

        //ZipGenerateLocalSector();
        //ZipRetrieveLocalSector();

        //StoreSolarSystem();
        //RetrieveSolarSystem();

        MapReflection();

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
        var stars = records.ToList<HabHygRecord>();

        CubeAreaStarLocation areaLocations = new CubeAreaStarLocation();
        IDictionary<string, List<HabHygRecord>> areaMappings = areaLocations.GetAreaMappings(ly, stars);

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

    public static void MapReflection()
    {
        ProgressionMap map = new("Progression");
        PY100DEMap py100demap = new(map);
        py100demap.CreateSolCluster();

        string filename = Path.Combine(dataDir, "Reflection.txt");

        if (File.Exists(filename))
            File.Delete(filename);

        using StreamWriter writer = new StreamWriter(filename);

        foreach (var propInfo in map.GetType().GetProperties())
        {
            //if (propInfo.Name == "Name" || propInfo.Name == "MetaData")
            //    continue;

            writer.WriteLine($"Name: {propInfo.Name}");
            writer.WriteLine($"Type: {propInfo.PropertyType.Name}");
            writer.WriteLine($"Declaring Type: {propInfo.DeclaringType}");
            writer.WriteLine($"Is Generic: {propInfo.PropertyType.IsGenericType}");

            if (propInfo.CustomAttributes.Any(pt => pt.AttributeType == typeof(IgnoreDataMemberAttribute)))
            {
                writer.WriteLine("Has IgnoreDataMemberAttribute");
                continue;
            }

            object obj = propInfo.GetValue(map);

            if (obj == null)
                continue;

            if (obj is StellarBody body)
            {
                DisplayStellarBody(body, writer);
                continue;
            }

            Type type = obj.GetType();
            if (type.IsGenericType)
            {
                switch (type.Name)
                {
                    case "Dictionary`2":
                    case "IDictionary`2":
                        HandleDictionary(obj, type, writer);
                        break;
                }
            }
        }
    }

    public static void DisplayStellarBody(StellarBody body, StreamWriter writer)
    {
        if (body is null)
        {
            writer.WriteLine("Not a StellarBody");
            return;
        }
        writer.WriteLine($"Identifier: {body.Identifier}");
        writer.WriteLine($"StellarBody Name: {body.Name}");
        writer.WriteLine($"StellarBody Type: {body.GetType().Name}");
        writer.WriteLine($"Map Name: {body.Map.Name}");
    }

    public static void HandleDictionary(object obj, Type type, StreamWriter writer)
    {
        Type[] genericTypes = type.GetGenericArguments();
        for (int i = 0; i < genericTypes.Count(); i++)
        {
            bool isStellarBody = IsStellarBody(genericTypes[i]);
            writer.WriteLine($"Generic Type Name: {genericTypes[i].Name} Is StelarBody: {isStellarBody}");
            HandleDictionaryStellarBody(obj, i, writer);
        }
    }

    public static bool IsStellarBody(Type type)
    {
        Type baseType = type.BaseType;

        while (baseType != null)
        { 
            if (baseType.Name == "StellarBody" || baseType.Name == "StellarParentBody")
                return true;
            baseType = baseType.BaseType; 
        }

        return false;
    }

    public static void HandleDictionaryStellarBody(object obj, int position, StreamWriter writer)
    {
        string property = position == 0 ? "Keys" : "Values";

        Type iDictType = obj.GetType().GetInterface("IDictionary`2");

        IEnumerable enumerator = (IEnumerable)iDictType.GetProperty(property).GetValue(obj, null);

        foreach (object o in enumerator)
        {
            if (o is StellarBody body)
                body.Map = BaseStellarMap.DefaultMap;
        }

        enumerator = (IEnumerable)iDictType.GetProperty(property).GetValue(obj, null);

        foreach (object o in enumerator)
        {
            if (o is StellarBody body)
                DisplayStellarBody(body, writer);
        }
    }
}

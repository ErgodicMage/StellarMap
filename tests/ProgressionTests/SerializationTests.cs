using Newtonsoft.Json;

namespace ProgressionTests;

[TestClass]
[TestCategory(TestCategories.FunctionalTest)]
public class SerializationTests
{
    [TestMethod]
    public void SerializeFileEarth()
    {
        ProgressionMap map = new ProgressionMap("Earth");
        LocalSectorMap c = new LocalSectorMap(map);
        c.CreateEarth();

        string filename = Path.Combine(TestingUtilities.Config["DataPath"], "Earth.json");
        if (File.Exists(filename))
            File.Delete(filename);

        using StreamWriter writer = new StreamWriter(filename);
        SerializeMap(map, writer);
    }

    [TestMethod]
    [TestCategory(TestCategories.FunctionalTest)]
    public void DeSerializeFileEarth()
    {
        string filename = Path.Combine(TestingUtilities.Config["DataPath"], "Earth.json");

        if (!File.Exists(filename))
            SerializeFileEarth();

        ProgressionMap map;

        using StreamReader reader = new StreamReader(filename);
        map = DeSerializeMap(reader);
    }

    [TestMethod]
    [TestCategory(TestCategories.FunctionalTest)]
    public void SerializeFileSolSystem()
    {
        ProgressionMap map = new ProgressionMap("Sol System");
        LocalSectorMap c = new LocalSectorMap(map);
        c.CreateSolSystem();

        string filename = Path.Combine(TestingUtilities.Config["DataPath"], "SolSystem.json");
        if (File.Exists(filename))
            File.Delete(filename);

        using StreamWriter writer = new StreamWriter(filename);
        SerializeMap(map, writer);
    }

    [TestMethod]
    [TestCategory(TestCategories.FunctionalTest)]
    public void DeSerializeFileSolSystem()
    {
        string filename = Path.Combine(TestingUtilities.Config["DataPath"], "SolSystem.json");

        if (!File.Exists(filename))
            SerializeFileSolSystem();

        ProgressionMap map;

        using (StreamReader reader = new StreamReader(filename))
        map = DeSerializeMap(reader);
    }

    [TestMethod]
    [TestCategory(TestCategories.FunctionalTest)]
    public void SerializeFileSolCluster()
    {
        ProgressionMap map = new ProgressionMap("Sol Cluster");
        LocalSectorMap c = new LocalSectorMap(map);
        c.CreateSolCluster();

        string filename = Path.Combine(TestingUtilities.Config["DataPath"], "SolCluster.json");
        if (File.Exists(filename))
            File.Delete(filename);

        using StreamWriter writer = new StreamWriter(filename);
        SerializeMap(map, writer);
    }

    [TestMethod]
    [TestCategory(TestCategories.FunctionalTest)]
    public void DeSerializeFileSolCluster()
    {
        string filename = Path.Combine(TestingUtilities.Config["DataPath"], "SolCluster.json");

        if (!File.Exists(filename))
            SerializeFileSolCluster();

        ProgressionMap map;

        using StreamReader reader = new StreamReader(filename);
        map = DeSerializeMap(reader);
    }

    [TestMethod]
    [TestCategory(TestCategories.FunctionalTest)]
    public void SerializeFileLocalSector()
    {
        ProgressionMap map = new ProgressionMap("Local Sector");
        LocalSectorMap c = new LocalSectorMap(map);
        c.CreateLocalSector();

        string filename = Path.Combine(TestingUtilities.Config["DataPath"], "LocalSector.json");
        if (File.Exists(filename))
            File.Delete(filename);

        using StreamWriter writer = new StreamWriter(filename);
        SerializeMap(map, writer);
    }

    [TestMethod]
    [TestCategory(TestCategories.FunctionalTest)]
    public void DeSerializeFileLocalSector()
    {
        string filename = Path.Combine(TestingUtilities.Config["DataPath"], "LocalSector.json");

        if (!File.Exists(filename))
            SerializeFileLocalSector();

        ProgressionMap map;

        using StreamReader reader = new StreamReader(filename);
        map = DeSerializeMap(reader);
    }

    public void SerializeMap(ProgressionMap map, StreamWriter writer)
    {
        string json = JsonConvert.SerializeObject(map, Newtonsoft.Json.Formatting.Indented);

        writer.Write(json);
    }

    public ProgressionMap DeSerializeMap(StreamReader reader)
    {
        string json = string.Empty;

        json = reader.ReadToEnd();

        ProgressionMap map = JsonConvert.DeserializeObject<ProgressionMap>(json);
        map.SetMap();

        return map;
    }
}

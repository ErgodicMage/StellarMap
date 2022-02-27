using Newtonsoft.Json;

namespace CoreTests;

[TestClass]
public class SerializationTests
{
    [TestMethod]
    [TestCategory(TestCategories.FunctionalTest)]
    public void SerializeFileEarth()
    {
        TestStellarMap map = new TestStellarMap("Earth");
        CreateEarth(map);

        string filename = Path.Combine(TestingUtilities.Config["DataPath"], "BaseEarth.json");
        if (File.Exists(filename))
            File.Delete(filename);

        using StreamWriter writer = new StreamWriter(filename);
        SerializeMap(map, writer);
    }

    [TestMethod]
    [TestCategory(TestCategories.FunctionalTest)]
    public void DeSerializeFileEarth()
    {
        string filename = Path.Combine(TestingUtilities.Config["DataPath"], "BaseEarth.json");

        if (!File.Exists(filename))
            SerializeFileEarth();

        TestStellarMap map;

        using StreamReader reader = new StreamReader(filename);
        map = DeSerializeMap(reader);
    }

    [TestMethod]
    [TestCategory(TestCategories.UnitTest)]
    public void CompareEarthTest()
    {
        TestStellarMap originalMap = new TestStellarMap("Earth");
        CreateEarth(originalMap);
        TestStellarMap finalMap;

        using MemoryStream memory = new MemoryStream();
        using StreamWriter writer = new StreamWriter(memory, Encoding.Unicode, 1024, true);
        SerializeMap(originalMap, writer);
        writer.Flush();

        memory.Position = 0;

        using StreamReader reader = new StreamReader(memory);
        finalMap = DeSerializeMap(reader);

        Assert.IsTrue(BaseStellarMapEqualityComparer.Comparer.Equals(originalMap, finalMap));
    }

    [TestMethod]
    [TestCategory(TestCategories.FunctionalTest)]
    public void SerializeFileSol()
    {
        TestStellarMap map = new TestStellarMap("Sol");
        CreateSol(map);

        string filename = Path.Combine(TestingUtilities.Config["DataPath"], "BaseSol.json");
        if (File.Exists(filename))
            File.Delete(filename);

        using StreamWriter writer = new StreamWriter(filename);
        SerializeMap(map, writer);
    }

    [TestMethod]
    [TestCategory(TestCategories.FunctionalTest)]
    public void DeSerializeFileSol()
    {
        string filename = Path.Combine(TestingUtilities.Config["DataPath"], "BaseSol.json");

        if (!File.Exists(filename))
            SerializeFileSol();

        TestStellarMap map;

        using StreamReader reader = new StreamReader(filename);
        map = DeSerializeMap(reader);
    }

    [TestMethod]
    [TestCategory(TestCategories.UnitTest)]
    public void CompareSolTest()
    {
        TestStellarMap originalMap = new TestStellarMap("Sol");
        CreateSol(originalMap);
        TestStellarMap finalMap;

        using MemoryStream memory = new MemoryStream();
        using StreamWriter writer = new StreamWriter(memory, Encoding.Unicode, 1024, true);
        SerializeMap(originalMap, writer);
        writer.Flush();

        memory.Position = 0;

        using StreamReader reader = new StreamReader(memory);
        finalMap = DeSerializeMap(reader);

        Assert.IsTrue(BaseStellarMapEqualityComparer.Comparer.Equals(originalMap, finalMap));
    }

    public void SerializeMap(TestStellarMap map, StreamWriter writer)
    {
        string json = JsonConvert.SerializeObject(map, Newtonsoft.Json.Formatting.Indented);

        writer.Write(json);
    }

    public TestStellarMap DeSerializeMap(StreamReader reader)
    {
        string json = string.Empty;

        json = reader.ReadToEnd();

        TestStellarMap map = JsonConvert.DeserializeObject<TestStellarMap>(json);
        (map as IStellarMap).SetMap();

        return map;
    }

    public void CreateEarth(TestStellarMap map)
    {
        Planet earth = new Planet("Earth");

        map.Add<Planet>(earth);

        Satellite moon = new Satellite("Moon");
        earth.Add(moon);
    }

    public void CreateSol(TestStellarMap map)
    {
        Star sol = new Star("Sol");
        map.Add<Star>(sol);
        sol.Add(new Planet("Mercury"));
        sol.Add(new Planet("Venus"));

        Planet earth = new Planet("Earth");

        map.Add<Planet>(earth);

        Satellite moon = new Satellite("Moon");
        earth.Add(moon);

        sol.Add(new Planet("Mars"));
        sol.Add(new Planet("Jupiter"));
        sol.Add(new Planet("Saturn"));
        sol.Add(new Planet("Uranus"));
        sol.Add(new Planet("Neptune"));

        sol.Add(new DwarfPlanet("Ceres"));
        sol.Add(new DwarfPlanet("Pluto"));

        sol.Add(new Asteroid("Pallas"));
        sol.Add(new Asteroid("Juno"));

        sol.Add(new Comet("Haley's"));
        sol.Add(new Comet("Caeser's"));
    }
}

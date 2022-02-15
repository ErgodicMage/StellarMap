namespace ProgressionTests;

[TestClass]
public class CreateTests
{
    [TestMethod]
    [TestCategory(TestCategories.UnitTest)]
    public void CreateEarthTest()
    {
        ProgressionMap map = new ProgressionMap("Earth");

        LocalSectorMap csol = new LocalSectorMap(map);
        Planet earth = csol.CreateEarth();
    }

    [TestMethod]
    [TestCategory(TestCategories.UnitTest)]
    public void CreateSolTest()
    {
        ProgressionMap map = new ProgressionMap("Sol");

        LocalSectorMap csol = new LocalSectorMap(map);
        StarSystem sol = csol.CreateSolSystem();

    }

    [TestMethod]
    [TestCategory(TestCategories.UnitTest)]
    public void CreateSolClusterTest()
    {
        ProgressionMap map = new ProgressionMap("Sol Cluster");

        LocalSectorMap csol = new LocalSectorMap(map);
        Cluster solCluster = csol.CreateSolCluster();

    }

    [TestMethod]
    [TestCategory(TestCategories.UnitTest)]
    public void CreateLocalSector()
    {
        ProgressionMap map = new ProgressionMap("Local Sector");

        LocalSectorMap c = new LocalSectorMap(map);
        Sector sector = c.CreateLocalSector();
    }

    // wrote this test method because I started catching myself reusing real stars.
    // sometimes it's hard to work with the designations, so I can now search the text file.
    [TestMethod]
    [TestCategory(TestCategories.FunctionalTest)]
    public void ListStarSystemDesignations()
    {
        ProgressionMap map = new ProgressionMap("Local");

        LocalSectorMap c = new LocalSectorMap(map);
        Sector sector = c.CreateLocalSector();

        string outfile = Path.Combine(TestingUtilities.Config["DataPath"], "Designations.txt");
        if (File.Exists(outfile))
            File.Delete(outfile);

        using (StreamWriter writer = new StreamWriter(outfile))
        {
            foreach (var system in map.StarSystems)
            {
                writer.Write(system.Key);
                writer.Write(" : ");
                writer.Write(system.Value.Name);
                writer.Write(" : ");
                if (system.Value.BasicProperties.ContainsKey(Constants.PropertyNames.Designation))
                    writer.Write(system.Value.BasicProperties[Constants.PropertyNames.Designation]);
                else
                    writer.Write("None");
                writer.WriteLine();
            }
        }
    }
}

namespace ProgressionTests;

[TestClass]
public class NavigatorTests
{
    [TestMethod]
    [TestCategory(TestCategories.UnitTest)]
    public void DirectlyConnectedSystems()
    {
        ProgressionMap map = new ProgressionMap("Sol Cluster 100DE");
        PY100DEMap p = new PY100DEMap(map);
        p.CreateSolCluster();

        Navigator navigator = new Navigator(map);

        IList<ERBridgesPath> path = navigator.GetPaths("Sol", "Epsilon Eridani");

        Assert.IsNotNull(path);
        Assert.IsTrue(path.Count == 1);
        Assert.IsNotNull(path[0].Bridges);
        Assert.IsTrue(path[0].Bridges.Count == 1);
        Assert.AreEqual("Bridge: Sol-Epsilon Eridani", path[0].Bridges[0].Name);

        // test another one but backwards from how it was added
        path = navigator.GetPaths("Ross", "Bernard");

        Assert.IsNotNull(path);
        Assert.IsNotNull(path[0].Bridges);
        Assert.IsTrue(path[0].Bridges.Count == 1);
        Assert.IsTrue(path.Count == 1);
        Assert.AreEqual("Bridge: Bernard-Ross", path[0].Bridges[0].Name);
    }
}


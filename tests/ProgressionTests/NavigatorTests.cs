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

        var pathResult = navigator.GetPaths("Sol", "Epsilon Eridani");

        Assert.IsTrue(pathResult.Success);

        var path = pathResult.Value;

        Assert.IsTrue(path.Count == 1);
        Assert.IsNotNull(path[0].Bridges);
        Assert.IsTrue(path[0].Bridges.Count == 1);
        Assert.AreEqual(1, path[0].TollCosts());
        Assert.AreEqual("Bridge: Sol-Epsilon Eridani", path[0].Bridges[0].Name);

        // test another one but backwards from how it was added
        pathResult = navigator.GetPaths("Ross", "Bernard");

        Assert.IsTrue(pathResult.Success);

        path = pathResult.Value;

        Assert.IsNotNull(path);
        Assert.IsNotNull(path[0].Bridges);
        Assert.IsTrue(path[0].Bridges.Count == 1);
        Assert.IsTrue(path.Count == 1);
        Assert.AreEqual(1, path[0].TollCosts());
        Assert.AreEqual("Bridge: Bernard-Ross", path[0].Bridges[0].Name);
    }

    [TestMethod]
    [TestCategory(TestCategories.UnitTest)]
    public void BernardProcyonPath()
    {
        ProgressionMap map = new ProgressionMap("Sol Cluster 100DE");
        PY100DEMap p = new PY100DEMap(map);
        p.CreateSolCluster();

        Navigator navigator = new Navigator(map);

        var pathResult = navigator.GetPaths("Bernard", "Procyon");
        Assert.IsTrue(pathResult.Success);

        var path = pathResult.Value;

        Assert.IsNotNull(path);
        Assert.IsTrue(path.Count == 2);
        Assert.IsNotNull(path[0].Bridges);
        Assert.IsTrue(path[0].Bridges.Count == 2);
        Assert.AreEqual(2, path[0].TollCosts());
        Assert.AreEqual("Bridge: Bernard-Sol", path[0].Bridges[0].Name);
        Assert.AreEqual("Bridge: Sol-Procyon", path[0].Bridges[1].Name);
        Assert.IsNotNull(path[1].Bridges);
        Assert.IsTrue(path[1].Bridges.Count == 3);
        Assert.AreEqual(3, path[0].TollCosts());
        Assert.AreEqual("Bridge: Bernard-Ross", path[1].Bridges[0].Name);
        Assert.AreEqual("Bridge: Ross-Ceta", path[1].Bridges[1].Name);
        Assert.AreEqual("Bridge: Procyon-Ceti", path[1].Bridges[2].Name);
    }
}


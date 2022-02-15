namespace ProgressionTests;

[TestClass]
public class EqualityComparerTests
{
    [TestMethod]
    [TestCategory(TestCategories.UnitTest)]
    public void ERBridgeEqualTest()
    {
        ProgressionMap map = new ProgressionMap("Local Sector");
        LocalSectorMap c = new LocalSectorMap(map);
        c.CreateLocalSector();

        ERBridge bridge1 = map.Bridges.Values.First();

        StarSystem system1 = map.Get<StarSystem>(bridge1.Portals[0].StarIdentifier);
        StarSystem system2 = map.Get<StarSystem>(bridge1.Portals[1].StarIdentifier);
        ERBridge bridge2 = new ERBridge(bridge1.BridgeType, system1, system2);
            
        // ok force them equal because of identifiers lol
        bridge2.ParentIdentifier = bridge1.ParentIdentifier;
        bridge2.Identifier = bridge1.Identifier;
        bridge2.Portals[0].ERBridgeIdentifier = bridge1.Portals[0].ERBridgeIdentifier;
        bridge2.Portals[1].ERBridgeIdentifier = bridge1.Portals[1].ERBridgeIdentifier;

        if (bridge1.BasicProperties.ContainsKey(Constants.PropertyNames.Distance))
            bridge2.BasicProperties.Add(Constants.PropertyNames.Distance, bridge1.BasicProperties[Constants.PropertyNames.Distance]);

        Assert.IsTrue(ERBridgeEqualityComparer.Comparer.Equals(bridge1, bridge2));
    }

    [TestMethod]
    [TestCategory(TestCategories.UnitTest)]
    public void ERBridgeNotEqualTest()
    {
        ProgressionMap map = new ProgressionMap("Local Sector");
        LocalSectorMap c = new LocalSectorMap(map);
        c.CreateLocalSector();

        ERBridge bridge1 = map.Bridges.Values.First();

        StarSystem system1 = map.Get<StarSystem>(bridge1.Portals[0].StarIdentifier);
        StarSystem system2 = map.Get<StarSystem>(bridge1.Portals[1].StarIdentifier);
        ERBridge bridge2 = new ERBridge(bridge1.BridgeType, system1, system2);

        // ok force them equal because of identifiers lol
        bridge2.ParentIdentifier = bridge1.ParentIdentifier;
        bridge2.Identifier = bridge1.Identifier;
        bridge2.Portals[0].ERBridgeIdentifier = bridge1.Portals[0].ERBridgeIdentifier;
        // except Portal 2 bridge identifier
        //bridge2.Portals[1].ERBridgeIdentifier = bridge1.Portals[1].ERBridgeIdentifier;

        Assert.IsFalse(ERBridgeEqualityComparer.Comparer.Equals(bridge1, bridge2));
    }

    [TestMethod]
    [TestCategory(TestCategories.UnitTest)]
    public void HabitatEqualTest()
    {
        ProgressionMap map = new ProgressionMap("Local Sector");
        LocalSectorMap c = new LocalSectorMap(map);
        c.CreateLocalSector();

        Habitat habitat1 = map.Habitats.Values.First();

        Habitat habitat2 = new Habitat();
        habitat2.BodyType = habitat1.BodyType;
        habitat2.Identifier = habitat1.Identifier;
        habitat2.Name = habitat1.Name;
        habitat2.ParentIdentifier = habitat1.ParentIdentifier;
        habitat2.Properties = habitat1.Properties;

        Assert.IsTrue(StellarBodyEqualityComparer.Comparer.Equals(habitat1, habitat2));
    }

    [TestMethod]
    [TestCategory(TestCategories.UnitTest)]
    public void HabitatNotEqualTest()
    {
        ProgressionMap map = new ProgressionMap("Local Sector");
        LocalSectorMap c = new LocalSectorMap(map);
        c.CreateLocalSector();

        Habitat habitat1 = map.Habitats.Values.First();

        Habitat habitat2 = new Habitat();
        habitat2.BodyType = ProgressionConstants.BodyType.Cluster;
        habitat2.Identifier = habitat1.Identifier;
        habitat2.Name = habitat1.Name;
        habitat2.ParentIdentifier = habitat1.ParentIdentifier;
        //habitat2.Properties = habitat1.Properties;

        Assert.IsFalse(StellarBodyEqualityComparer.Comparer.Equals(habitat1, habitat2));
    }

    [TestMethod]
    [TestCategory(TestCategories.UnitTest)]
    public void StarSystemEqualTest()
    {
        StarSystem system1 = new StarSystem("System 1");
        system1.Identifier = "System 1";
        ProgressionStar star = new ProgressionStar("Star 1");
        star.Identifier = "Star 1";
        system1.Add(star);

        StarSystem system2 = new StarSystem("System 1");
        system2.Identifier = "System 1";
        system2.Add(star);

        Assert.IsTrue(StarSystemEqualityComparer.Comparer.Equals(system1, system2));
    }

    [TestMethod]
    [TestCategory(TestCategories.UnitTest)]
    public void StarSystemNotEqualTest()
    {
        StarSystem system1 = new StarSystem("System 1");
        system1.Identifier = "System 1";
        ProgressionStar star = new ProgressionStar("Star 1");
        star.Identifier = "Star 1";
        system1.Add(star);

        StarSystem system2 = new StarSystem("System 1");
        system2.Identifier = "System 1";
        star = new ProgressionStar("Star 1");
        star.Identifier = "Star 2";
        system2.Add(star);

        Assert.IsFalse(StarSystemEqualityComparer.Comparer.Equals(system1, system2));
    }

    [TestMethod]
    [TestCategory(TestCategories.UnitTest)]
    public void StarSystemNotEqualUnequalStarsTest()
    {
        StarSystem system1 = new StarSystem("System 1");
        system1.Identifier = "System 1";
        ProgressionStar star1 = new ProgressionStar("Star 1");
        star1.Identifier = "Star 1";
        ProgressionStar star2 = new ProgressionStar("Star 2");
        star2.Identifier = "Star 2";
        system1.Add(star1);
        system1.Add(star2);

        StarSystem system2 = new StarSystem("System 1");
        system2.Identifier = "System 1";
        system2.Add(star1);

        Assert.IsFalse(StarSystemEqualityComparer.Comparer.Equals(system1, system2));
    }

    [TestMethod]
    [TestCategory(TestCategories.UnitTest)]
    public void PortalTestEquals()
    {
        Portal portal1 = new Portal();
        portal1.StarIdentifier = "Star 1";
        portal1.ERBridgeIdentifier = "Bridge 1";
        portal1.Position = new Point3d(0.0, 0.0, 0.0);

        Portal portal2 = new Portal();
        portal2.StarIdentifier = "Star 1";
        portal2.ERBridgeIdentifier = "Bridge 1";
        portal2.Position = new Point3d(0.0, 0.0, 0.0);

        Assert.IsTrue(portal1.Equals(portal2));
    }

    [TestMethod]
    [TestCategory(TestCategories.UnitTest)]
    public void PortalNotTestEquals()
    {
        Portal portal1 = new Portal();
        portal1.StarIdentifier = "Star 1";
        portal1.ERBridgeIdentifier = "Bridge 1";
        portal1.Position = new Point3d(0.0, 0.0, 0.0);

        Portal portal2 = new Portal();
        portal2.StarIdentifier = "Star 1";
        portal2.ERBridgeIdentifier = "Bridge 1";
        portal2.Position = new Point3d(1.0, 1.0, 1.0);

        Assert.IsFalse(portal1.Equals(portal2));
    }

    [TestMethod]
    [TestCategory(TestCategories.UnitTest)]
    public void LocalSectorEqualTest()
    {
        ProgressionMap map = new ProgressionMap("Local Sector");
        LocalSectorMap c = new LocalSectorMap(map);
        c.CreateLocalSector();

        ProgressionMap map2 = new ProgressionMap("Local Sector");
        LocalSectorMap c2 = new LocalSectorMap(map2);
        c2.CreateLocalSector();

        Assert.IsTrue(ProgressionMapEqualityComparer.Comparer.Equals(map, map2));
    }

    [TestMethod]
    [TestCategory(TestCategories.UnitTest)]
    public void LocalSectorNotEqualTest()
    {
        ProgressionMap map = new ProgressionMap("Local Sector");
        LocalSectorMap c = new LocalSectorMap(map);
        c.CreateLocalSector();

        ProgressionMap map2 = new ProgressionMap("Local Sector");
        LocalSectorMap c2 = new LocalSectorMap(map2);
        c2.CreateLocalSector();
        var solCluster = map2.Clusters.Values.Where(x => x.Name == "Sol Cluster").First();
        Assert.IsNotNull(solCluster);
        solCluster.Name = "New Sol";
            
        Assert.IsFalse(ProgressionMapEqualityComparer.Comparer.Equals(map, map2));
    }
}

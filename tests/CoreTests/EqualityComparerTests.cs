﻿namespace CoreTests;

[TestClass]
public class EqualityComparerTests
{
    [TestMethod]
    [TestCategory(TestCategories.UnitTest)]
    public void PlanetReferenceEqualTest()
    {
        TestStellarMap map = new TestStellarMap();

        Planet earth = new Planet("Earth");

        map.Add<Planet>(earth);

        Satellite moon = new Satellite("Moon");
        earth.Add(moon);

        Assert.IsTrue(PlanetEqualityComparer.Comparer.Equals(earth, earth));
    }

    [TestMethod]
    [TestCategory(TestCategories.UnitTest)]
    public void PlanetEqualTest()
    {
        TestStellarMap map = new TestStellarMap();
        Planet earth = new Planet("Earth");
        map.Add<Planet>(earth);
        Satellite moon = new Satellite("Moon");
        earth.Add(moon);

        TestStellarMap map2 = new TestStellarMap();
        Planet earth2 = new Planet("Earth");
        map2.Add<Planet>(earth2);
        Satellite moon2 = new Satellite("Moon");
        earth2.Add(moon2);

        Assert.IsTrue(PlanetEqualityComparer.Comparer.Equals(earth, earth2));
    }

    [TestMethod]
    [TestCategory(TestCategories.UnitTest)]
    public void PlanetNotEqualNameTest()
    {
        TestStellarMap map = new TestStellarMap();
        Planet earth = new Planet("Earth");
        map.Add<Planet>(earth);
        Satellite moon = new Satellite("Moon");
        earth.Add(moon);

        TestStellarMap map2 = new TestStellarMap();
        Planet earth2 = new Planet("NotEarth");
        map2.Add<Planet>(earth2);
        Satellite moon2 = new Satellite("Moon");
        earth2.Add(moon2);

        Assert.IsFalse(PlanetEqualityComparer.Comparer.Equals(earth, earth2));
    }

    [TestMethod]
    [TestCategory(TestCategories.UnitTest)]
    public void PlanetNotEqualMoonNameTest()
    {
        TestStellarMap map = new TestStellarMap();
        Planet earth = new Planet("Earth");
        map.Add<Planet>(earth);
        Satellite moon = new Satellite("Moon");
        earth.Add(moon);

        BaseStellarMap map2 = new BaseStellarMap();
        Planet earth2 = new Planet("Earth");
        map2.Add<Planet>(earth2);
        Satellite moon2 = new Satellite("NotMoon");
        earth2.Add(moon2);

        Assert.IsFalse(PlanetEqualityComparer.Comparer.Equals(earth, earth2));
    }

    [TestMethod]
    [TestCategory(TestCategories.UnitTest)]
    public void PlanetNotEqualMoonMissingTest()
    {
        TestStellarMap map = new TestStellarMap();
        Planet earth = new Planet("Earth");
        map.Add<Planet>(earth);

        TestStellarMap map2 = new TestStellarMap();
        Planet earth2 = new Planet("Earth");
        map2.Add<Planet>(earth2);
        Satellite moon2 = new Satellite("Moon");
        earth2.Add(moon2);

        Assert.IsFalse(PlanetEqualityComparer.Comparer.Equals(earth, earth2));
    }

    [TestMethod]
    [TestCategory(TestCategories.UnitTest)]
    public void PlanetNotEqualPropertyDescriptionTest()
    {
        TestStellarMap map = new TestStellarMap();
        Planet earth = new Planet("Earth");
        map.Add<Planet>(earth);
        earth.BasicProperties.Add("Description", "Our home world");
        Satellite moon = new Satellite("Moon");
        earth.Add(moon);

        TestStellarMap map2 = new TestStellarMap();
        Planet earth2 = new Planet("Earth");
        map2.Add<Planet>(earth2);
        earth2.BasicProperties.Add("Description", "No signs of intelligent life");
        Satellite moon2 = new Satellite("Moon");
        earth2.Add(moon2);

        Assert.IsFalse(PlanetEqualityComparer.Comparer.Equals(earth, earth2));
    }

    [TestMethod]
    [TestCategory(TestCategories.UnitTest)]
    public void PlanetNotEqualMissingPropertyTest()
    {
        TestStellarMap map = new TestStellarMap();
        Planet earth = new Planet("Earth");
        map.Add<Planet>(earth);
        Satellite moon = new Satellite("Moon");
        earth.Add(moon);

        BaseStellarMap map2 = new BaseStellarMap();
        Planet earth2 = new Planet("Earth");
        map2.Add<Planet>(earth2);
        earth2.BasicProperties.Add("Description", "No signs of intelligent life");
        Satellite moon2 = new Satellite("Moon");
        earth2.Add(moon2);

        Assert.IsFalse(PlanetEqualityComparer.Comparer.Equals(earth, earth2));
    }

    [TestMethod]
    [TestCategory(TestCategories.UnitTest)]
    public void StarEqualTest()
    {
        TestStellarMap map = new TestStellarMap();
        Star sol = new Star("Sol");
        map.Add<Star>(sol);
        sol.BasicProperties.Add(Constants.PropertyNames.Designation, "Sol");
        sol.BasicProperties.Add(Constants.PropertyNames.StellarClass, "G2V");
        var catalogue = new Dictionary<string, string>();
        catalogue.Add("HabHyg", "0");
        catalogue.Add("Hip", "0");
        sol.Properties.AddGroup("Catalogue", catalogue);
        sol.Add(new Planet("Earth"));

        TestStellarMap map2 = new TestStellarMap();
        Star sol2 = new Star("Sol");
        map2.Add<Star>(sol2);
        sol2.BasicProperties.Add(Constants.PropertyNames.Designation, "Sol");
        sol2.BasicProperties.Add(Constants.PropertyNames.StellarClass, "G2V");
        catalogue = new Dictionary<string, string>();
        catalogue.Add("HabHyg", "0");
        catalogue.Add("Hip", "0");
        sol2.Properties.AddGroup("Catalogue", catalogue);
        sol2.Add(new Planet("Earth"));

        Assert.IsTrue(StarEqualityComparer.Comparer.Equals(sol, sol2));
    }

    [TestMethod]
    [TestCategory(TestCategories.UnitTest)]
    public void StarNotEqualCatalogueTest()
    {
        TestStellarMap map = new TestStellarMap();
        Star sol = new Star("Sol");
        map.Add<Star>(sol);
        sol.BasicProperties.Add(Constants.PropertyNames.Designation, "Sol");
        sol.BasicProperties.Add(Constants.PropertyNames.StellarClass, "G2V");
        var catalogue = new Dictionary<string, string>();
        catalogue.Add("HabHyg", "0");
        catalogue.Add("Hip", "0");
        sol.Properties.AddGroup("Catalogue", catalogue);

        TestStellarMap map2 = new TestStellarMap();
        Star sol2 = new Star("Sol");
        map2.Add<Star>(sol2);
        sol2.BasicProperties.Add(Constants.PropertyNames.Designation, "Sol");
        sol2.BasicProperties.Add(Constants.PropertyNames.StellarClass, "G2V");
        catalogue = new Dictionary<string, string>();
        catalogue.Add("HabHyg", "1");
        catalogue.Add("Hip", "1");
        sol2.Properties.AddGroup("Catalogue", catalogue);

        Assert.IsFalse(StarEqualityComparer.Comparer.Equals(sol, sol2));
    }

    [TestMethod]
    [TestCategory(TestCategories.UnitTest)]
    public void StarEqualSolarSystemTest()
    {
        TestStellarMap map = new TestStellarMap();
        Star sol = new Star("Sol");
        map.Add<Star>(sol);
        sol.Add(new Planet("Mercury"));
        sol.Add(new Planet("Venus"));
        sol.Add(new Planet("Earth"));
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

        TestStellarMap map2 = new TestStellarMap();
        Star sol2 = new Star("Sol");
        map2.Add<Star>(sol2);
        sol2.Add(new Planet("Mercury"));
        sol2.Add(new Planet("Venus"));
        sol2.Add(new Planet("Earth"));
        sol2.Add(new Planet("Mars"));
        sol2.Add(new Planet("Jupiter"));
        sol2.Add(new Planet("Saturn"));
        sol2.Add(new Planet("Uranus"));
        sol2.Add(new Planet("Neptune"));

        sol2.Add(new DwarfPlanet("Ceres"));
        sol2.Add(new DwarfPlanet("Pluto"));

        sol2.Add(new Asteroid("Pallas"));
        sol2.Add(new Asteroid("Juno"));

        sol2.Add(new Comet("Haley's"));
        sol2.Add(new Comet("Caeser's"));

        Assert.IsTrue(StarEqualityComparer.Comparer.Equals(sol, sol2));
    }

    [TestMethod]
    [TestCategory(TestCategories.UnitTest)]
    public void StarNotEqualSolarSystemMissingPlutoTest()
    {
        TestStellarMap map = new TestStellarMap();
        Star sol = new Star("Sol");
        map.Add<Star>(sol);
        sol.Add(new Planet("Mercury"));
        sol.Add(new Planet("Venus"));
        sol.Add(new Planet("Earth"));
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

        TestStellarMap map2 = new TestStellarMap();
        Star sol2 = new Star("Sol");
        //sol2.Identifier = sol.Identifier; // have to do this since BaseStellarMap's default identifier generation is GUIDs which can't be compared
        map2.Add<Star>(sol2);
        sol2.Add(new Planet("Mercury"));
        sol2.Add(new Planet("Venus"));
        sol2.Add(new Planet("Earth"));
        sol2.Add(new Planet("Mars"));
        sol2.Add(new Planet("Jupiter"));
        sol2.Add(new Planet("Saturn"));
        sol2.Add(new Planet("Uranus"));
        sol2.Add(new Planet("Neptune"));

        // Pluto is a planet once again
        sol2.Add(new Planet("Pluto"));

        // Ceres is back to an asteroid
        sol2.Add(new Asteroid("Ceres"));
        sol2.Add(new Asteroid("Pallas"));
        sol2.Add(new Asteroid("Juno"));

        sol2.Add(new Comet("Haley's"));
        sol2.Add(new Comet("Caeser's"));

        Assert.IsFalse(StarEqualityComparer.Comparer.Equals(sol, sol2));
    }

    [TestMethod]
    [TestCategory(TestCategories.UnitTest)]
    public void StellarMapEqualTest()
    {
        TestStellarMap map = new TestStellarMap("TestMap");
        Star sol = new Star("Sol");
        map.Add<Star>(sol);
        sol.Add(new Planet("Mercury"));
        sol.Add(new Planet("Venus"));
        sol.Add(new Planet("Earth"));
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

        Star proximaCent = new Star("Proxima Centauri");
        map.Add<Star>(proximaCent);
        proximaCent.Add(new Planet("b"));

        TestStellarMap map2 = new TestStellarMap("TestMap");
        Star sol2 = new Star("Sol");
        map2.Add<Star>(sol2);
        sol2.Add(new Planet("Mercury"));
        sol2.Add(new Planet("Venus"));
        sol2.Add(new Planet("Earth"));
        sol2.Add(new Planet("Mars"));
        sol2.Add(new Planet("Jupiter"));
        sol2.Add(new Planet("Saturn"));
        sol2.Add(new Planet("Uranus"));
        sol2.Add(new Planet("Neptune"));

        sol2.Add(new DwarfPlanet("Ceres"));
        sol2.Add(new DwarfPlanet("Pluto"));

        sol2.Add(new Asteroid("Pallas"));
        sol2.Add(new Asteroid("Juno"));

        sol2.Add(new Comet("Haley's"));
        sol2.Add(new Comet("Caeser's"));

        proximaCent = new Star("Proxima Centauri");
        map2.Add<Star>(proximaCent);
        proximaCent.Add(new Planet("b"));

        Assert.IsTrue(BaseStellarMapEqualityComparer.Comparer.Equals(map, map2));
    }

    [TestMethod]
    [TestCategory(TestCategories.UnitTest)]
    public void StellarMapNotEqualProxCentCTest()
    {
        TestStellarMap map = new TestStellarMap("TestMap");
        Star sol = new Star("Sol");
        map.Add<Star>(sol);
        sol.Add(new Planet("Mercury"));
        sol.Add(new Planet("Venus"));
        sol.Add(new Planet("Earth"));
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

        Star proximaCent = new Star("Proxima Centauri");
        map.Add<Star>(proximaCent);
        proximaCent.Add(new Planet("b"));

        TestStellarMap map2 = new TestStellarMap("TestMap");
        Star sol2 = new Star("Sol");
        map2.Add<Star>(sol2);
        sol2.Add(new Planet("Mercury"));
        sol2.Add(new Planet("Venus"));
        sol2.Add(new Planet("Earth"));
        sol2.Add(new Planet("Mars"));
        sol2.Add(new Planet("Jupiter"));
        sol2.Add(new Planet("Saturn"));
        sol2.Add(new Planet("Uranus"));
        sol2.Add(new Planet("Neptune"));

        sol2.Add(new DwarfPlanet("Ceres"));
        sol2.Add(new DwarfPlanet("Pluto"));

        sol2.Add(new Asteroid("Pallas"));
        sol2.Add(new Asteroid("Juno"));

        sol2.Add(new Comet("Haley's"));
        sol2.Add(new Comet("Caeser's"));

        proximaCent = new Star("Proxima Centauri");
        map2.Add<Star>(proximaCent);
        proximaCent.Add(new Planet("b"));
        proximaCent.Add(new Planet("c"));

        Assert.IsFalse(BaseStellarMapEqualityComparer.Comparer.Equals(map, map2));
    }
}

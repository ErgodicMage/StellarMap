namespace StellarMap.Progression.DefaultSettingMaps;

public class PY100DEMap
{
    public PY100DEMap(ProgressionMap map)
    {
        Map = map;

        map.MetaData.Add("Basic", "Author", "Ergodic Mage");
        map.MetaData.Add("Basic", "Version", "0.1");
        map.MetaData.Add("Basic", "Date", "11/29/2019");

        using (StringWriter writer = new StringWriter())
        {
            writer.WriteLine("This is the map of the default Progression system at year 100DE.");
            writer.WriteLine("At this point in time, humans have started spreading through the Sol Cluster.");
            writer.WriteLine("The Diaspora Era is documented at https://github.com/ErgodicMage/StellarMap/blob/master/src/StellarSystems/Progression/DefaultSettingMaps/Diaspora%20Era.md");
            writer.Flush();

            map.MetaData.Add("Basic", "Description", writer.ToString());
        }
        map.MetaData.Add("Basic", "Diagram", "https://drive.google.com/file/d/1oQhyRB6X-ckqjOw_A0sMtgq8gt3He8_U/view?usp=sharing");
        map.MetaData.Add("Basic", "Rules", "https://github.com/ErgodicMage/StellarMap/blob/master/src/StellarSystems/Progression/Rules.md");
        map.MetaData.Add("Basic", "ProgressionDate", "100DE");
    }

    ProgressionMap Map { get; set; }

    public Planet CreateEarth()
    {
        ProgressionPlanet earth = new ProgressionPlanet("Earth");

        Map.Add<Planet>(earth);

        Satellite moon = new Satellite("Moon");
        earth.Add(moon);

        earth.Add(new Habitat("Space Station V"));
        earth.Add(new Habitat("Moon Base 1"));

        return earth;
    }

    public StarSystem CreateSolSystem()
    {
        ProgressionStar sol = new ProgressionStar("Sun");

        Map.Add<Star>(sol);
        sol.BasicProperties.Add(Constants.PropertyNames.Designation, "Sol");
        sol.BasicProperties.Add(Constants.PropertyNames.StellarClass, "G2V");
        var catalogue = new Dictionary<string, string>();
        catalogue.Add("HabHyg", "0");
        catalogue.Add("Hip", "0");
        sol.Properties.AddGroup("Catalogue", catalogue);

        sol.Add(new Planet("Mercury"));
        sol.Add(new Planet("Venus"));

        Planet earth = CreateEarth();
        sol.Add(earth);

        sol.Add(new Planet("Mars"));
        sol.Add(new Planet("Jupiter"));
        sol.Add(new Planet("Saturn"));
        sol.Add(new Planet("Uranus"));
        sol.Add(new Planet("Neptune"));
        sol.Add(new Planet("Pluto"));

        sol.Add(new Asteroid("Ceres"));
        sol.Add(new Asteroid("Pallas"));
        sol.Add(new Asteroid("Juno"));

        sol.Add(new Comet("Haley's"));
        sol.Add(new Comet("Caeser's"));

        sol.Add(new Habitat("Ceres Station"));

        StarSystem solSystem = new StarSystem("Sol");
        solSystem.BasicProperties.Add(Constants.PropertyNames.Position, new Point3d(0, 0, 0).ToString());
        Map.Add(solSystem);
        solSystem.Add(sol);

        return solSystem;
    }

    public Cluster CreateSolCluster()
    {
        Cluster solCluster = new Cluster("Sol Cluster");
        Map.Add(solCluster);

        StarSystem system = null;
        ProgressionStar star = null;
        IDictionary<string, string> catalogue = null;

        system = CreateSolSystem();
        solCluster.Add(system);

        #region Bernard's Star
        system = new StarSystem("Bernard");
        system.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 699");
        system.BasicProperties.Add(Constants.PropertyNames.Position, new Point3d(1.5, 0.9, 0.4).ToString());
        Map.Add(system);
        star = new ProgressionStar("Bernard's Star");
        star.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 699");
        star.BasicProperties.Add(Constants.PropertyNames.StellarClass, "sdM4");
        catalogue = new Dictionary<string, string>();
        catalogue.Add("HabHyg", "4");
        catalogue.Add("Hip", "87937");
        star.Properties.AddGroup("Catalogue", catalogue);
        system.Add(star);
        solCluster.Add(system);
        #endregion

        #region Epsilon Eridani
        system = new StarSystem("Epsilon Eridani");
        system.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 144");
        system.BasicProperties.Add(Constants.PropertyNames.Position, new Point3d(-2.1, -0.6, -2.4).ToString());
        Map.Add(system);
        star = new ProgressionStar("Epsilon Eridani");
        star.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 144");
        star.BasicProperties.Add(Constants.PropertyNames.StellarClass, "K2V");
        catalogue = new Dictionary<string, string>();
        catalogue.Add("HabHyg", "13");
        catalogue.Add("Hip", "16537");
        star.Properties.AddGroup("Catalogue", catalogue);
        system.Add(star);
        solCluster.Add(system);
        #endregion

        #region Procyon
        system = new StarSystem("Procyon");
        system.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 280");
        system.BasicProperties.Add(Constants.PropertyNames.Position, new Point3d(-2.8, -1.9, 0.8).ToString());
        Map.Add(system);
        star = new ProgressionStar("Procyon");
        star.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 280");
        star.BasicProperties.Add(Constants.PropertyNames.StellarClass, "F5IV-V");
        catalogue = new Dictionary<string, string>();
        catalogue.Add("HabHyg", "18");
        catalogue.Add("Hip", "37279");
        star.Properties.AddGroup("Catalogue", catalogue);
        system.Add(star);
        solCluster.Add(system);
        #endregion

        #region Ross
        system = new StarSystem("Ross");
        system.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 447");
        system.BasicProperties.Add(Constants.PropertyNames.Position, new Point3d(0, -1.7, 2.9).ToString());
        Map.Add(system);
        star = new ProgressionStar("Pocks");
        star.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 447/Ross 128");
        star.BasicProperties.Add(Constants.PropertyNames.StellarClass, "M4.5V");
        catalogue = new Dictionary<string, string>();
        catalogue.Add("HabHyg", "14");
        catalogue.Add("Hip", "57548");
        star.Properties.AddGroup("Catalogue", catalogue);
        system.Add(star);
        solCluster.Add(system);
        #endregion

        #region Ceti
        system = new StarSystem("Ceti");
        system.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 65");
        system.BasicProperties.Add(Constants.PropertyNames.Position, new Point3d(-0.6, 0.1, -2.5).ToString());
        Map.Add(system);
        star = new ProgressionStar("BL");
        star.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 65 A");
        star.BasicProperties.Add(Constants.PropertyNames.StellarClass, "dM5.5V");
        catalogue = new Dictionary<string, string>();
        catalogue.Add("HabHyg", "7");
        catalogue.Add("Hip", "-1");
        star.Properties.AddGroup("Catalogue", catalogue);
        system.Add(star);
        star = new ProgressionStar("UV");
        star.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 65 B");
        star.BasicProperties.Add(Constants.PropertyNames.StellarClass, "dM5.5V");
        catalogue = new Dictionary<string, string>();
        catalogue.Add("HabHyg", "7");
        catalogue.Add("Hip", "-1");
        star.Properties.AddGroup("Catalogue", catalogue);
        system.Add(star);
        solCluster.Add(system);
        #endregion

        #region Bridges
        // Add Bridges
        ERBridge bridge = ERBridgeHelper.CreateStarSystemBridge(Map, solCluster, "Sol", "Bernard");
        solCluster.Add(bridge);
        bridge = ERBridgeHelper.CreateStarSystemBridge(Map, solCluster, "Sol", "Epsilon Eridani");
        solCluster.Add(bridge);
        bridge = ERBridgeHelper.CreateStarSystemBridge(Map, solCluster, "Sol", "Procyon");
        solCluster.Add(bridge);
        bridge = ERBridgeHelper.CreateStarSystemBridge(Map, solCluster, "Bernard", "Ross");
        solCluster.Add(bridge);
        bridge = ERBridgeHelper.CreateStarSystemBridge(Map, solCluster, "Procyon", "Ceti");
        solCluster.Add(bridge);
        bridge = ERBridgeHelper.CreateStarSystemBridge(Map, solCluster, "Ross", "Ceti");
        solCluster.Add(bridge);
        #endregion

        return solCluster;
    }
}

namespace StellarMap.Progression.DefaultSettingMaps;

public class PreDiasporaMap
{
    public PreDiasporaMap(ProgressionMap map)
    {
        Map = map;

        map.MetaData.Add("Basic", "Author", "Ergodic Mage");
        map.MetaData.Add("Basic", "Version", "0.1");
        map.MetaData.Add("Basic", "Date", "11/29/2019");

        using (StringWriter writer = new StringWriter())
        {
            writer.WriteLine("This is the map of the default Progression system at year 100DE.");
            writer.WriteLine("At this point in time, humans have started spreading through the Sol Cluster.");
            writer.WriteLine("The Pre-Diaspora Era is documented at https://github.com/ErgodicMage/StellarMap/blob/master/src/StellarSystems/Progression/DefaultSettingMaps/PreDiaspora.md");
            writer.Flush();

            map.MetaData.Add("Basic", "Description", writer.ToString());
        }
        map.MetaData.Add("Basic", "Diagram", "https://drive.google.com/file/d/1oQhyRB6X-ckqjOw_A0sMtgq8gt3He8_U/view?usp=sharing");
        map.MetaData.Add("Basic", "Rules", "https://github.com/ErgodicMage/StellarMap/blob/master/src/StellarSystems/Progression/Rules.md");

    }

    ProgressionMap Map { get; set; }
    ProgressionPlanet Earth { get; set; } = new ProgressionPlanet("Earth");
    StarSystem Sol { get; set; } = new StarSystem("Sol");

    public Planet CreateEarth()
    {
        Map.Add<Planet>(Earth);

        Satellite moon = new Satellite("Moon");
        Earth.Add(moon);

        return Earth;
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

        sol.Add(new Asteroid("Vesta"));
        sol.Add(new Asteroid("Ceres"));
        sol.Add(new Asteroid("Pallas"));
        sol.Add(new Asteroid("Juno"));
        sol.Add(new Asteroid("Hygiea"));
        sol.Add(new Asteroid("Euphrosyne"));

        sol.Add(new Comet("Haley's"));
        sol.Add(new Comet("Caeser's"));

        Sol.BasicProperties.Add(Constants.PropertyNames.Position, new Point3d(0, 0, 0).ToString());
        Map.Add(Sol);
        Sol.Add(sol);

        return Sol;
    }

    public ProgressionMap Create2100AD()
    {
        Map.MetaData.Add("Basic", "ProgressionDate", "2100AD");

        CreateSolSystem();

        Earth.Add(new Habitat("SpaceX Station"));
        Earth.Add(new Habitat("Moon Base Alpha"));

        return Map;
    }

    public ProgressionMap Create2200AD()
    {
        Map.MetaData.Add("Basic", "ProgressionDate", "2100AD");

        CreateSolSystem();

        Earth.Add(new Habitat("SpaceX Station"));
        Earth.Add(new Habitat("Moon Base Gamma"));
        Earth.Add(new Habitat(""));

        return Map;
    }
}

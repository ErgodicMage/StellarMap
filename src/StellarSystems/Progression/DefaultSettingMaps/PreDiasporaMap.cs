using System.Collections.Generic;

using StellarMap.Core.Bodies;
using StellarMap.Core.Types;
using StellarMap.Math.Types;

namespace StellarMap.Progression.DefaultSettingMaps
{
    public class PreDiasporaMap
    {
        public PreDiasporaMap(ProgressionMap map)
        {
            Map = map;
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
    }
}

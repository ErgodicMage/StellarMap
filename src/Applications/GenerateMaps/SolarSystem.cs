namespace StellarMap.GenerateMaps;

public static class SolarSystem
{
    public static IStellarMap CreateSolSystem()
    {
        var map = new BaseStellarMap("SolarSystem");

        Star sol = new Star("Sol");
        sol.BasicProperties.Add(Constants.PropertyNames.StellarClass, "G2V");
        map.Add(sol);

        sol.Add(new Planet("Mercury"));
        sol.Add(new Planet("Venus"));

        Planet earth = new Planet("Earth");
        earth.Map = map;
        Satellite moon = new Satellite("Moon");
        earth.Add(moon);
        sol.Add(earth);

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

        sol.GetPlanets();

        return map;
    }

}

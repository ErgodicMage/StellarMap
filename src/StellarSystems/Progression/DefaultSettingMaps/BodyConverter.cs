namespace StellarMap.Progression.DefaultSettingMaps;

public static class BodyConverter
{
    public static ProgressionStar ConvertStarOnly(ProgressionMap map, Star oldStar)
    {
        ProgressionStar newStar = new ProgressionStar(oldStar.Name);
        newStar.Properties = oldStar.Properties;
        map.Add(newStar);
        return newStar;
    }

    public static ProgressionStar ConvertStar(ProgressionMap map, Star oldStar)
    {
        ProgressionStar newStar = ConvertStarOnly(map, oldStar);

        IDictionary<string, Planet>? oldPlanets = oldStar.GetPlanets();
        if (oldPlanets is not null)
        {
            foreach (Planet oldPlanet in oldPlanets.Values)
            {
                ProgressionPlanet newPlanet = ConvertPlanet(map, oldPlanet);
                newStar.Add(newPlanet);
            }
        }

        IDictionary<string, DwarfPlanet>? oldDwarfs = oldStar.GetDwarfPlanets();
        if (oldDwarfs is not null)
        {
            foreach (DwarfPlanet oldDwarf in oldDwarfs.Values)
            {
                // Default handling is that if the DwarfPlanet has satellites convert
                // it to an Planet otherwise convert to an Asteroid
                if (oldDwarf.Satellites != null)
                {
                    ProgressionPlanet newPlanet = ConvertDwarfPlanetasPlanet(map, oldDwarf);
                    newStar.Add(newPlanet);
                }
                else
                {
                    Asteroid newAsteroid = ConvertDwarfPlanetasAsteroid(map, oldDwarf);
                    newStar.Add(newAsteroid);
                }
            }
        }

        IDictionary<string, Asteroid>? oldAsteroids = oldStar.GetAsteroids();
        if (oldAsteroids is not null)
        {
            foreach (Asteroid oldAsteroid in oldAsteroids.Values)
            {
                Asteroid newAsteroid = ConvertAsteroid(map, oldAsteroid);
                newStar.Add(newAsteroid);
            }
        }

        IDictionary<string, Comet>? oldComets = oldStar.GetComets();
        if (oldComets is not null)
        {
            foreach (Comet oldComet in oldComets.Values)
            {
                Comet newComet = ConvertComet(map, oldComet);
                newStar.Add(newComet);
            }
        }

        return newStar;
    }

    public static ProgressionPlanet ConvertPlanetOnly(ProgressionMap map, Planet oldPlanet)
    {
        ProgressionPlanet newPlanet = new ProgressionPlanet(oldPlanet.Name);
        newPlanet.Properties = oldPlanet.Properties;
        map.Add(newPlanet);
        return newPlanet;
    }

    public static ProgressionPlanet ConvertPlanet(ProgressionMap map, Planet oldPlanet)
    {
        ProgressionPlanet newPlanet = ConvertPlanetOnly(map, oldPlanet);

        IDictionary<string, Satellite>? oldSatellites = oldPlanet.GetSatellites();
        if (oldSatellites is not null)
        {
            foreach(Satellite oldSatellite in oldSatellites.Values)
            {
                Satellite newSatellite = ConvertSatellite(map, oldSatellite);
                newPlanet.Add(newSatellite);
            }
        }

        return newPlanet;
    }

    public static ProgressionPlanet ConvertDwarfPlanetasPlanetOnly(ProgressionMap map, DwarfPlanet oldDwarfPlanet)
    {
        ProgressionPlanet newPlanet = new ProgressionPlanet(oldDwarfPlanet.Name);
        newPlanet.Properties = oldDwarfPlanet.Properties;
        map.Add(newPlanet);
        return newPlanet;
    }

    public static ProgressionPlanet ConvertDwarfPlanetasPlanet(ProgressionMap map, DwarfPlanet oldDwarfPlanet)
    {
        ProgressionPlanet newPlanet = ConvertDwarfPlanetasPlanetOnly(map, oldDwarfPlanet);

        IDictionary<string, Satellite>? oldSatellites = oldDwarfPlanet.GetSatellites();
        if (oldSatellites is not null)
        {
            foreach(Satellite oldSatellite in oldSatellites.Values)
            {
                Satellite newSatellite = ConvertSatellite(map, oldSatellite);
                newPlanet.Add(newSatellite);
            }
        }

        return newPlanet;
    }

    public static Asteroid ConvertDwarfPlanetasAsteroid(ProgressionMap map, DwarfPlanet oldDwarfPlanet)
    {
        Asteroid newAsteroid = new Asteroid(oldDwarfPlanet.Name);
        newAsteroid.Properties = oldDwarfPlanet.Properties;
        map.Add(newAsteroid);
        return newAsteroid;
    }

    public static Satellite ConvertSatellite(ProgressionMap map, Satellite oldSatellite)
    {
        Satellite newSatellite = new Satellite(oldSatellite.Name);
        newSatellite.Properties = oldSatellite.Properties;
        map.Add(newSatellite);
        return newSatellite;
    }

    public static Asteroid ConvertAsteroid(ProgressionMap map, Asteroid oldAsteroid)
    {
        Asteroid newAsteroid = new Asteroid(oldAsteroid.Name);
        newAsteroid.Properties = oldAsteroid.Properties;
        map.Add(newAsteroid);
        return newAsteroid;
    }

    public static Comet ConvertComet(ProgressionMap map, Comet oldComet)
    {
        Comet newComet = new Comet(oldComet.Name);
        newComet.Properties = oldComet.Properties;
        map.Add(newComet);
        return newComet;
    }
}

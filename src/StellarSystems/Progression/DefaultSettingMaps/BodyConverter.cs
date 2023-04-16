namespace StellarMap.Progression.DefaultSettingMaps;

public static class BodyConverter
{
    public static Result<ProgressionStar> ConvertStarOnly(ProgressionMap map, Star oldStar)
    {
        Result guardResult = GuardClause.Null(map).Null(oldStar);
        if (!guardResult.Success) return guardResult;

        var newStar = new ProgressionStar(oldStar.Name);
        newStar.Properties = oldStar.Properties;
        map.Add(newStar);
        return newStar;
    }

    public static Result<ProgressionStar> ConvertStar(ProgressionMap map, Star oldStar)
    {
        Result guardResult = GuardClause.Null(map).Null(oldStar);
        if (!guardResult.Success) return guardResult;

        var newStarResult = ConvertStarOnly(map, oldStar);
        if (!newStarResult.Success) return newStarResult;

        var newStar = newStarResult.Value;

        var oldPlanets = oldStar.GetPlanets();
        if (oldPlanets.Success && oldPlanets.Value is not null)
        {
            foreach (var oldPlanet in oldPlanets.Value.Values)
            {
                var newPlanet = ConvertPlanet(map, oldPlanet);
                if (!newPlanet.Success) continue;
                newStar.Add(newPlanet);
            }
        }

        var oldDwarfs = oldStar.GetDwarfPlanets();
        if (oldDwarfs.Success)
        {
            foreach (var oldDwarf in oldDwarfs.Value.Values)
            {
                // Default handling is that if the DwarfPlanet has satellites convert
                // it to an Planet otherwise convert to an Asteroid
                if (oldDwarf.Satellites is not null)
                {
                    var newPlanet = ConvertDwarfPlanetasPlanet(map, oldDwarf);
                    if (!newPlanet.Success) continue;
                    newStar.Add(newPlanet);
                }
                else
                {
                    var newAsteroid = ConvertDwarfPlanetasAsteroid(map, oldDwarf);
                    if (!newAsteroid.Success) continue;
                    newStar.Add(newAsteroid);
                }
            }
        }

        var oldAsteroids = oldStar.GetAsteroids();
        if (oldAsteroids.Success)
        {
            foreach (var oldAsteroid in oldAsteroids.Value.Values)
            {
                var newAsteroid = ConvertAsteroid(map, oldAsteroid);
                if (!newAsteroid.Success) continue;
                newStar.Add(newAsteroid);
            }
        }

        var oldComets = oldStar.GetComets();
        if (oldComets.Success)
        {
            foreach (var oldComet in oldComets.Value.Values)
            {
                var newComet = ConvertComet(map, oldComet);
                if (!newComet.Success) continue;
                newStar.Add(newComet);
            }
        }

        return newStar;
    }

    public static Result<ProgressionPlanet> ConvertPlanetOnly(ProgressionMap map, Planet oldPlanet)
    {
        Result guardResult = GuardClause.Null(map).Null(oldPlanet);
        if (!guardResult.Success) return guardResult;

        var newPlanet = new ProgressionPlanet(oldPlanet.Name);
        newPlanet.Properties = oldPlanet.Properties;
        map.Add(newPlanet);
        return newPlanet;
    }

    public static Result<ProgressionPlanet> ConvertPlanet(ProgressionMap map, Planet oldPlanet)
    {
        Result guardResult = GuardClause.Null(map).Null(oldPlanet);
        if (!guardResult.Success) return guardResult;

        var newPlanetResult = ConvertPlanetOnly(map, oldPlanet);
        if (!newPlanetResult.Success) return newPlanetResult;

        var newPlanet = newPlanetResult.Value;

        var oldSatellites = oldPlanet.GetSatellites();
        if (oldSatellites.Success)
        {
            foreach(Satellite oldSatellite in oldSatellites.Value.Values)
            {
                var newSatellite = ConvertSatellite(map, oldSatellite);
                if (!newSatellite.Success) continue;
                newPlanet.Add(newSatellite);
            }
        }

        return newPlanet;
    }

    public static Result<ProgressionPlanet> ConvertDwarfPlanetasPlanetOnly(ProgressionMap map, DwarfPlanet oldDwarfPlanet)
    {
        Result guardResult = GuardClause.Null(map).Null(oldDwarfPlanet);
        if (!guardResult.Success) return guardResult;

        var newPlanet = new ProgressionPlanet(oldDwarfPlanet.Name);
        newPlanet.Properties = oldDwarfPlanet.Properties;
        map.Add(newPlanet);
        return newPlanet;
    }

    public static Result<ProgressionPlanet> ConvertDwarfPlanetasPlanet(ProgressionMap map, DwarfPlanet oldDwarfPlanet)
    {
        var newPlanet = ConvertDwarfPlanetasPlanetOnly(map, oldDwarfPlanet);
        if (!newPlanet.Success) return newPlanet;

        var oldSatellites = oldDwarfPlanet.GetSatellites();
        if (oldSatellites.Success)
        {
            foreach(Satellite oldSatellite in oldSatellites.Value.Values)
            {
                var newSatellite = ConvertSatellite(map, oldSatellite);
                if (!newSatellite.Success) continue;
                newPlanet.Value.Add(newSatellite);
            }
        }

        return newPlanet;
    }

    public static Result<Asteroid> ConvertDwarfPlanetasAsteroid(ProgressionMap map, DwarfPlanet oldDwarfPlanet)
    {
        Result guardResult = GuardClause.Null(map).Null(oldDwarfPlanet);
        if (!guardResult.Success) return guardResult;

        var newAsteroid = new Asteroid(oldDwarfPlanet.Name);
        newAsteroid.Properties = oldDwarfPlanet.Properties;
        map.Add(newAsteroid);
        return newAsteroid;
    }

    public static Result<Satellite> ConvertSatellite(ProgressionMap map, Satellite oldSatellite)
    {
        Result guardResult = GuardClause.Null(map).Null(oldSatellite);
        if (!guardResult.Success) return guardResult;

        var newSatellite = new Satellite(oldSatellite.Name);
        newSatellite.Properties = oldSatellite.Properties;
        map.Add(newSatellite);
        return newSatellite;
    }

    public static Result<Asteroid> ConvertAsteroid(ProgressionMap map, Asteroid oldAsteroid)
    {
        Result guardResult = GuardClause.Null(map).Null(oldAsteroid);
        if (!guardResult.Success) return guardResult;

        var newAsteroid = new Asteroid(oldAsteroid.Name);
        newAsteroid.Properties = oldAsteroid.Properties;
        map.Add(newAsteroid);
        return newAsteroid;
    }

    public static Result<Comet> ConvertComet(ProgressionMap map, Comet oldComet)
    {
        Result guardResult = GuardClause.Null(map).Null(oldComet);
        if (!guardResult.Success) return guardResult;

        var newComet = new Comet(oldComet.Name);
        newComet.Properties = oldComet.Properties;
        map.Add(newComet);
        return newComet;
    }
}

using System;
using System.Collections.Generic;
using System.Text;

using StellarMap.Core.Bodies;

namespace StellarMap.Core.Types
{
    public interface IStellarMap
    {
        // Properties
        string Name { get; set; }

        IDictionary<string, Star> Stars { get; set; }

        IDictionary<string, Planet> Planets { get; set; }

        IDictionary<string, Satellite> Satellites { get; set; }

        IDictionary<string, Asteroid> Asteroids { get; set; }

        IDictionary<string, Comet> Comets { get; set; }

        // Get Methods
        T Get<T>(string id) where T : StellarBody;

        void Get<T>(ICollection<string> identifiers, IDictionary<string, T> output) where T : StellarBody;

        Star GetStar(string id);

        IDictionary<string, Star> GetStars(ICollection<string> identifiers);

        Planet GetPlanet(string id);

        IDictionary<string, Planet> GetPlanets(ICollection<string> identifiers);

        Satellite GetSatellite(string id);

        IDictionary<string, Satellite> GetSatellites(ICollection<string> identifiers);

        Asteroid GetAsteroid(string id);

        IDictionary<string, Asteroid> GetAsteroids(ICollection<string> identifiers);

        Comet GetComet(string id);

        IDictionary<string, Comet> GetComets(ICollection<string> identifiers);

        // Add Methods
        void Add<T>(T t) where T : StellarBody;

        void Add<T>(ICollection<T> ts) where T : StellarBody;

        void Add(Star star);

        void Add(ICollection<Star> stars);

        void Add(Planet planet);

        void Add(ICollection<Planet> planets);

        void Add(Satellite satelite);

        void Add(ICollection<Satellite> satelites);

        void Add(Asteroid asteroid);

        void Add(ICollection<Asteroid> asteroids);

        void Add(Comet comet);

        void Add(ICollection<Comet> comets);

        string GenerateIdentifier<T>() where T : StellarBody;

        IList<string> GetBodyTypes();

        object GetBody(string bodytype);

        Type GetTypeOfBody(string bodytype);

        bool SetBody(string bodytype, object data);

    }
}

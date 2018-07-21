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

        // Add Methods
        void Add<T>(T t) where T : StellarBody;

        void Add<T>(ICollection<T> ts) where T : StellarBody;

        string GenerateIdentifier<T>() where T : StellarBody;

        IList<string> GetBodyTypes();

        object GetBody(string bodytype);

        Type GetTypeOfBody(string bodytype);

        bool SetBody(string bodytype, object data);

    }
}

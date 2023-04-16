namespace StellarMap.Core.Types;

public interface IStellarMap
{
    // Properties
    string Name { get; set; }

    GroupedProperties MetaData { get; set; }

    IDictionary<string, Star>? Stars { get; set; }

    IDictionary<string, Planet>? Planets { get; set; }

    IDictionary<string, Satellite>? Satellites { get; set; }

    IDictionary<string, DwarfPlanet>? DwarfPlanets { get; set; }

    IDictionary<string, Asteroid>? Asteroids { get; set; }

    IDictionary<string, Comet>? Comets { get; set; }

    // Get Methods
    Result<T> Get<T>(string? id) where T : IStellarBody;

    Result Get<T>(ICollection<string>? identifiers, IDictionary<string, T> output) where T : IStellarBody;

    // Add Methods
    Result Add<T>(T t) where T : IStellarBody;

    Result Add<T>(ICollection<T> ts) where T : IStellarBody;

    string GenerateIdentifier<T>() where T : IStellarBody;

    IList<string> GetBodyTypes();

    Result<object> GetBody(string bodytype);

    Result<Type> GetTypeOfBody(string bodytype);

    Result SetBody(string bodytype, object data);

    Result SetMap() => MapSetter.SetMap(this, this);

}

namespace StellarMap.Core.Types;

public interface IStellarBody
{
    string? Identifier { get; set; }
    string? ParentIdentifier { get; set; }
    string Name { get; set; }
    string BodyType { get; set; }
    GroupedProperties Properties { get; set; }

    IStellarMap Map { get; set; }
}

public interface IStellarParentBody : IStellarBody
{
    Result<T> Get<T> (string name, GroupNamedIdentifiers groupIdentifiers, string groupName) where T : IStellarBody;

    Result<IDictionary<string, T>> GetAll<T>(GroupNamedIdentifiers groupIdentifiers, string groupName) where T : IStellarBody;

    Result Add<T>(T t, GroupNamedIdentifiers groupIdentifiers, string groupName)  where T : IStellarBody;
}

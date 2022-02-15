namespace StellarMap.Core.Types;

public interface ICatalogue
{
    #region Properties
    string Name { get; }

    string Description { get; }

    string Source { get; }

    string Location { get; set; }

    bool AddDataAsProperties { get; set; }
    #endregion

    #region Functions
    IList<Type> GetTypes();

    bool HasType(Type t);

    IStellarMap Get();

    void Get(IStellarMap map);
    #endregion
}

public interface IRealisticCatalogue : ICatalogue
{
    IStellarMap GetWithin(double ly);

    void GetWithin(IStellarMap map, double ly);

    IStellarMap GetWithin(double ly, double magnitude);

    void GetWithin(IStellarMap map, double ly, double magnitude);

    IStellarMap GetMagnitude(double magnitude);

    void GetMagnitude(IStellarMap map, double magnitude);

}

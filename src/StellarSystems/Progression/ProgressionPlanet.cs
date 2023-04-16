namespace StellarMap.Progression;

[DataContract(Name = ProgressionConstants.BodyType.ProgressionPlanet)]
public class ProgressionPlanet : Planet
{
    #region Constructors
    public ProgressionPlanet()
    {            
    }
        
    public ProgressionPlanet(string name) : base(name)
    {
        PlanetGroupIdentifiers.Name = "GroupIdentifiers-ProgressionPlanet";
    }
    #endregion

    #region Public Properties
    [IgnoreDataMember]
    public IDictionary<string, string>? Habitats 
        { get => PlanetGroupIdentifiers.GroupIdentifiers.Get(ProgressionConstants.NamedIdentifiers.Habitats).Value; }
    #endregion

    #region Get Methods
    public virtual Result<Habitat> GeHabitat(string name) => 
        Get<Habitat>(name, PlanetGroupIdentifiers, ProgressionConstants.NamedIdentifiers.Habitats);

    public virtual Result<IDictionary<string, Habitat>> GetHabitats() => 
        GetAll<Habitat>(PlanetGroupIdentifiers, ProgressionConstants.NamedIdentifiers.Habitats);
    #endregion

    #region Add Methods
    public Result Add(Habitat habitat) => 
        Add<Habitat>(habitat, PlanetGroupIdentifiers, ProgressionConstants.NamedIdentifiers.Habitats);
    #endregion

}

namespace StellarMap.Progression;

[DataContract(Name = ProgressionConstants.BodyType.ProgressionStar)]
public class ProgressionStar : Star
{
    #region Consturctors
    public ProgressionStar()
    {            
    }
        
    public ProgressionStar(string name) : base(name)
    {
        StarGroupIdentifiers.Name = "GroupIdentifiers-ProgressionStar";
    }
    #endregion

    #region Public Properties
    [IgnoreDataMember]
    public IDictionary<string, string>? Habitats 
        { get => StarGroupIdentifiers.GroupIdentifiers.Get(ProgressionConstants.NamedIdentifiers.Habitats).Value; }
    #endregion

    #region Get Methods
    public virtual Result<Habitat> GeHabitat(string name) => 
        Get<Habitat>(name, StarGroupIdentifiers, ProgressionConstants.NamedIdentifiers.Habitats);

    public virtual Result<IDictionary<string, Habitat>> GetHabitats() => 
        GetAll<Habitat>(StarGroupIdentifiers, ProgressionConstants.NamedIdentifiers.Habitats);

    #endregion

    #region Add Methods
    public Result Add(Habitat habitat) => 
        Add<Habitat>(habitat, StarGroupIdentifiers, ProgressionConstants.NamedIdentifiers.Habitats);
    #endregion
}

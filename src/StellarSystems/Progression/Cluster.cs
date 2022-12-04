namespace StellarMap.Progression;

[DataContract (Name = ProgressionConstants.BodyType.Cluster)]
public class Cluster : ProgressionContainer
{
    #region Constructors
    public Cluster()
    {
        ContainerType = ProgressionConstants.ContainerTypes.Cluster;
    }

    public Cluster(string name) : base(name, ProgressionConstants.ContainerTypes.Cluster)
    {
        ContainerType = ProgressionConstants.ContainerTypes.Cluster;
    }
    #endregion

    #region Public Properties
    [IgnoreDataMember]
    public IDictionary<string, string>? StarSystems 
        { get => ContainerGroupIdentifiers.GroupIdentifiers.Get(ProgressionConstants.NamedIdentifiers.StarSystems); }
    #endregion

    #region Get Methods
    public virtual StarSystem GetStarSystem(string name) => 
        Get<StarSystem>(name, ContainerGroupIdentifiers, ProgressionConstants.NamedIdentifiers.StarSystems);

    public virtual IDictionary<string, StarSystem> GetStarSystems() => 
        GetAll<StarSystem>(ContainerGroupIdentifiers, ProgressionConstants.NamedIdentifiers.StarSystems);
    #endregion

    #region Add Methods
    public void Add(StarSystem system) => 
        Add<StarSystem>(system, ContainerGroupIdentifiers, ProgressionConstants.NamedIdentifiers.StarSystems);
    #endregion

}

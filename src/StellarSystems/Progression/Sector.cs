namespace StellarMap.Progression;

[DataContract (Name = ProgressionConstants.BodyType.Sector)]
public class Sector : ProgressionContainer
{
    #region Constructors
    public Sector()
    {            
    }
        
    public Sector(string name) : base(name, ProgressionConstants.ContainerTypes.Sector)
    {
        ContainerType = ProgressionConstants.ContainerTypes.Sector;
    }
    #endregion

    #region Public Properties
    [IgnoreDataMember]
    public IDictionary<string, string>? Clusters 
        { get => ContainerGroupIdentifiers.GroupIdentifiers.Get(ProgressionConstants.NamedIdentifiers.Clusters).Value; }
    #endregion

    #region Get Methods
    public virtual Result<Cluster> GetCluster(string name) => 
        Get<Cluster>(name, ContainerGroupIdentifiers, ProgressionConstants.NamedIdentifiers.Clusters);

    public virtual Result<IDictionary<string, Cluster>> GetClusters() => 
        GetAll<Cluster>(ContainerGroupIdentifiers, ProgressionConstants.NamedIdentifiers.Clusters);
    #endregion

    #region Add Methods
    public Result Add(Cluster cluster) => 
        Add<Cluster>(cluster, ContainerGroupIdentifiers, ProgressionConstants.NamedIdentifiers.Clusters);
    #endregion
}

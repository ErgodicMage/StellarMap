namespace StellarMap.Progression;

[DataContract(Name = ProgressionConstants.BodyType.StarSystem)]
public class StarSystem : ProgressionContainer, IEqualityComparer<StarSystem>
{
    #region Constructors
    public StarSystem()
    {            
    }
        
    public StarSystem(string name) : base(name, ProgressionConstants.ContainerTypes.StarSystem)
    {
        ContainerType = ProgressionConstants.ContainerTypes.StarSystem;
    }
    #endregion

    #region Public Properties
    [IgnoreDataMember]
    public IDictionary<string, string>? Stars 
        { get => ContainerGroupIdentifiers.GroupIdentifiers.Get(Constants.NamedIdentifiers.Stars).Value; }

    [DataMember(Order = 21)]
    public IList<Portal>? Portals { get; set; }
    #endregion

    #region Get Methods
    public virtual Result<ProgressionStar> GetStar(string name)
    {        
        Result<Star> result = Get<Star>(name, ContainerGroupIdentifiers, Constants.NamedIdentifiers.Stars);
        if (!result.Success) return Result<ProgressionStar>.Error(result.ErrorMessage, result.Exception);
        var star = result.Value as ProgressionStar;
        return star is not null ? Result<ProgressionStar>.Ok(star) : Result<ProgressionStar>.Error($"StarSystem.GetStar {name} is not a ProgressionStar");
    }

    public virtual Result<IDictionary<string, Star>> GetStars() => 
        GetAll<Star>(ContainerGroupIdentifiers, Constants.NamedIdentifiers.Stars);
    #endregion

    #region Add Methods
    public Result Add(ProgressionStar star) => 
        Add<Star>(star, ContainerGroupIdentifiers, Constants.NamedIdentifiers.Stars);

    public Result Add(Portal portal)
    {
        Portals ??= new List<Portal>();
        Portals.Add(portal);
        return Result.Ok();
    }
    #endregion

    #region IEqualityComparer
    public bool Equals(StarSystem? x, StarSystem? y) => StarSystemEqualityComparer.Comparer.Equals(x, y);

    public override bool Equals(object? obj) => StarSystemEqualityComparer.Comparer.Equals(this, obj as StarSystem);

    public int GetHashCode(StarSystem? obj) => StarSystemEqualityComparer.Comparer.GetHashCode(obj);

    public override int GetHashCode() => StarSystemEqualityComparer.Comparer.GetHashCode(this);
    #endregion
}

public sealed class StarSystemEqualityComparer : IEqualityComparer<StarSystem>
{
    #region IEqualityComparer
    public bool Equals(StarSystem? x, StarSystem? y)
    {
        if (x is null || y is null) return false;

        bool bRet = ProgressionContainerEqualityComparer.Comparer.Equals(x, y);

        if (bRet)
        {
            if (x.Portals == null && y.Portals == null)
                bRet = true;
            else if (x.Portals == null || y.Portals == null)
                bRet = false;
            else if (x.Portals.Count == y.Portals.Count)
            {
                var thisPortals = x.Portals.GetEnumerator();
                var otherPortals = y.Portals.GetEnumerator();

                while (thisPortals.MoveNext() && otherPortals.MoveNext())
                {
                    if (!thisPortals.Current.Equals(otherPortals.Current))
                    {
                        bRet = false;
                        break;
                    }
                }
            }
            else
                bRet = false;
        }

        return bRet;
    }

    public int GetHashCode(StarSystem? obj)
    {
        if (obj is null) return 0;

        int hash = ProgressionContainerEqualityComparer.Comparer.GetHashCode();
        if (obj.Portals is not null)
        {
            foreach (Portal p in obj.Portals)
                hash ^= p.GetHashCode();
        }

        return hash;
    }
    #endregion

    public static StarSystemEqualityComparer Comparer { get; } = new StarSystemEqualityComparer();
}

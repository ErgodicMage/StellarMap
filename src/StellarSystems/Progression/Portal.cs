namespace StellarMap.Progression;

[DataContract (Name = "Portal")]
public struct Portal : IEquatable<Portal>
{
    [DataMember (Order = 1)]
    public string StarIdentifier { get; set; }

    [DataMember (Order = 2)]
    public string ERBridgeIdentifier { get; set; }

    [DataMember(Order = 3)]
    public Point3d Position;

    #region IEquatable
    public bool Equals(Portal other) => 
        StarIdentifier.Equals(other.StarIdentifier) && ERBridgeIdentifier.Equals(other.ERBridgeIdentifier) && 
        Position.Equals(other.Position);

    public override bool Equals(object obj) => obj is Portal p && Equals(p);

    public override int GetHashCode()
    {
        int hash = base.GetHashCode();
        if (StarIdentifier != null)
            hash ^= StarIdentifier.GetHashCode();
        if (!string.IsNullOrEmpty(ERBridgeIdentifier))
            hash ^= ERBridgeIdentifier.GetHashCode();

        hash ^= Position.GetHashCode();

        return hash;
    }
    #endregion
}

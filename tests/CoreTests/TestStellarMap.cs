namespace CoreTests;

// class created just to standardize on identifiers simply so that they don't have to be copied from one map to the other
public class TestStellarMap : BaseStellarMap
{
    public TestStellarMap() : base() { }
    public TestStellarMap(string name) : base(name) { }

    int count = 0;

    public override string GenerateIdentifier<T>()
    {
        count++;
        return count.ToString();
    }

}

namespace StellarMap.Progression;

[DataContract (Name = ProgressionConstants.BodyType.Habitat)]
public class Habitat : StellarBody
{
    #region Constructors
    public Habitat()
    {            
    }
        
    public Habitat(string name) : base (name, ProgressionConstants.BodyType.Habitat)
    {
    }
    #endregion

    #region Protected Methods
    #endregion
}

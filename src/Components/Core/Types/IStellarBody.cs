using System;
using System.Collections.Generic;

namespace StellarMap.Core.Types
{
    public interface IStellarBody
    {
        string Identifier { get; set; }
        string ParentIdentifier { get; set; }
        string Name { get; set; }
        string BodyType { get; set; }
        IDictionary<string, GroupProperties> AllGroupProperties { get; set; }

        IStellarMap Map { get; set; }
    }

    public interface IStallarBodyWithBodies
    {
        T Get<T> (string name) where T : IStellarBody;

        IDictionary<string, T> GetAll<T>() where T : IStellarBody;
    }
}

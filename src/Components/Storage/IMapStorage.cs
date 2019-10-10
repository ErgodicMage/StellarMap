using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using StellarMap.Core.Bodies;
using StellarMap.Core.Types;

namespace StellarMap.Storage
{
    public interface IMapStorage
    {
        void Store(IStellarMap map, StreamWriter writer);

        T Retreive<T>(StreamReader reader) where T : IStellarMap, new();
    }
}

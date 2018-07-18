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

        bool Retreive(StreamReader reader, IStellarMap map);
    }
}

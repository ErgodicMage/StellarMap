using System;
using System.Collections.Generic;
using System.Text;

using StellarMap.Core.Bodies;
using StellarMap.Core.Types;
using StellarMap.Math.Types;

namespace StellarMap.Traversal
{
    public interface IWormhole : ITraversal
    {
        string[] ConnectedStars { get; set; }

        Point3d[] Positions { get; set; }

        Star GetStar(int end);

        Star GetStar(string name);

        IDictionary<string, Star> GetStars();

    }
}

using System;
using System.Collections.Generic;
using System.Text;

using StellarMap.Catalogues;
using StellarMap.Math.Types;

namespace StellarMap.GenerateMaps
{
    public class CubeAreaStarLocation
    {
        IDictionary<string, Func<double, Point3d, bool>> AreaMappings = new Dictionary<string, Func<double, Point3d, bool>>();

        public CubeAreaStarLocation()
        {
            {
                AreaMappings.Add("A(0,0,0)", (l, p) => ((p.x < l / 2 && p.x > -l / 2 && p.y < l / 2 && p.y > -l / 2 && p.z < l / 2 && p.z > -l / 2)));
                AreaMappings.Add("A(-1,0,0)", (l, p) => ((p.x < -l / 2 && p.x > -3 * l / 2 && p.y < l / 2 && p.y > -l / 2 && p.z < l / 2 && p.z > -l / 2)));
                AreaMappings.Add("A(1,0,0)", (l, p) => ((p.x < 3 * l / 2 && p.x > l / 2 && p.y < l / 2 && p.y > -l / 2 && p.z < l / 2 && p.z > -l / 2)));

                AreaMappings.Add("A(0,1,0)", (l, p) => ((p.x < l / 2 && p.x > -l / 2 && p.y < 3 * l / 2 && p.y > l / 2 && p.z < l / 2 && p.z > -l / 2)));
                AreaMappings.Add("A(-1,1,0)", (l, p) => ((p.x < -l / 2 && p.x > -3 * l / 2 && p.y < 3 * l / 2 && p.y > l / 2 && p.z < l / 2 && p.z > -l / 2)));
                AreaMappings.Add("A(1,1,0)", (l, p) => ((p.x < 3 * l / 2 && p.x > l / 2 && p.y < 3 * l / 2 && p.y > l / 2 && p.z < l / 2 && p.z > -l / 2)));

                AreaMappings.Add("A(0,-1,0)", (l, p) => ((p.x < l / 2 && p.x > -l / 2 && p.y < -l / 2 && p.y > -3 * l / 2 && p.z < l / 2 && p.z > -l / 2)));
                AreaMappings.Add("A(-1,-1,0)", (l, p) => ((p.x < -l / 2 && p.x > -3 * l / 2 && p.y < -l / 2 && p.y > -3 * l / 2 && p.z < l / 2 && p.z > -l / 2)));
                AreaMappings.Add("A(1,-1,0)", (l, p) => ((p.x < 3 * l / 2 && p.x > l / 2 && p.y < -l / 2 && p.y > -3 * l / 2 && p.z < l / 2 && p.z > -l / 2)));


                AreaMappings.Add("A(0,0,1)", (l, p) => ((p.x < l / 2 && p.x > -l / 2 && p.y < l / 2 && p.y > -l / 2 && p.z < 3 * l / 2 && p.z > l / 2)));
                AreaMappings.Add("A(-1,0,1)", (l, p) => ((p.x < -l / 2 && p.x > -3 * l / 2 && p.y < l / 2 && p.y > -l / 2 && p.z < 3 * l / 2 && p.z > l / 2)));
                AreaMappings.Add("A(1,0,1)", (l, p) => ((p.x < 3 * l / 2 && p.x > l / 2 && p.y < l / 2 && p.y > -l / 2 && p.z < 3 * l / 2 && p.z > l / 2)));

                AreaMappings.Add("A(0,1,1)", (l, p) => ((p.x < l / 2 && p.x > -l / 2 && p.y < 3 * l / 2 && p.y > l / 2 && p.z < 3 * l / 2 && p.z > l / 2)));
                AreaMappings.Add("A(-1,1,1)", (l, p) => ((p.x < -l / 2 && p.x > -3 * l / 2 && p.y < 3 * l / 2 && p.y > l / 2 && p.z < 3 * l / 2 && p.z > l / 2)));
                AreaMappings.Add("A(1,1,1)", (l, p) => ((p.x < 3 * l / 2 && p.x > l / 2 && p.y < 3 * l / 2 && p.y > l / 2 && p.z < 3 * l / 2 && p.z > l / 2)));

                AreaMappings.Add("A(0,-1,1)", (l, p) => ((p.x < l / 2 && p.x > -l / 2 && p.y < -l / 2 && p.y > -3 * l / 2 && p.z < 3 * l / 2 && p.z > l / 2)));
                AreaMappings.Add("A(-1,-1,1)", (l, p) => ((p.x < -l / 2 && p.x > -3 * l / 2 && p.y < -l / 2 && p.y > -3 * l / 2 && p.z < 3 * l / 2 && p.z > l / 2)));
                AreaMappings.Add("A(1,-1,1)", (l, p) => ((p.x < 3 * l / 2 && p.x > l / 2 && p.y < -l / 2 && p.y > -3 * l / 2 && p.z < 3 * l / 2 && p.z > l / 2)));


                AreaMappings.Add("A(0,0,-1)", (l, p) => ((p.x < l / 2 && p.x > -l / 2 && p.y < l / 2 && p.y > -l / 2 && p.z < -l / 2 && p.z > -3 * l / 2)));
                AreaMappings.Add("A(-1,0,-1)", (l, p) => ((p.x < -l / 2 && p.x > -3 * l / 2 && p.y < l / 2 && p.y > -l / 2 && p.z < -l / 2 && p.z > -3 * l / 2)));
                AreaMappings.Add("A(1,0,-1)", (l, p) => ((p.x < 3 * l / 2 && p.x > l / 2 && p.y < l / 2 && p.y > -l / 2 && p.z < -l / 2 && p.z > -3 * l / 2)));

                AreaMappings.Add("A(0,1,-1)", (l, p) => ((p.x < l / 2 && p.x > -l / 2 && p.y < 3 * l / 2 && p.y > l / 2 && p.z < -l / 2 && p.z > -3 * l / 2)));
                AreaMappings.Add("A(-1,1,-1)", (l, p) => ((p.x < -l / 2 && p.x > -3 * l / 2 && p.y < 3 * l / 2 && p.y > l / 2 && p.z < -l / 2 && p.z > -3 * l / 2)));
                AreaMappings.Add("A(1,1,-1)", (l, p) => ((p.x < 3 * l / 2 && p.x > l / 2 && p.y < 3 * l / 2 && p.y > l / 2 && p.z < -l / 2 && p.z > -3 * l / 2)));

                AreaMappings.Add("A(0,-1,-1)", (l, p) => ((p.x < l / 2 && p.x > -l / 2 && p.y < -l / 2 && p.y > -3 * l / 2 && p.z < -l / 2 && p.z > -3 * l / 2)));
                AreaMappings.Add("A(-1,-1,-1)", (l, p) => ((p.x < -l / 2 && p.x > -3 * l / 2 && p.y < -l / 2 && p.y > -3 * l / 2 && p.z < -l / 2 && p.z > -3 * l / 2)));
                AreaMappings.Add("A(1,-1,-1)", (l, p) => ((p.x < 3 * l / 2 && p.x > l / 2 && p.y < -l / 2 && p.y > -3 * l / 2 && p.z < -l / 2 && p.z > -3 * l / 2)));
            }
        }

        public string GetArea(double l, Point3d p)
        {
            string area = string.Empty;

            foreach (var kvp in AreaMappings)
            {
                if (kvp.Value(l, p))
                {
                    area = kvp.Key;
                    break;
                }
            }

            return area;
        }

        public IDictionary<string, IList<HabHygRecord>> GetAreaMappings(double l, IList<HabHygRecord> stars)
        {
            IDictionary<string, IList<HabHygRecord>> mappings = new Dictionary<string, IList<HabHygRecord>>();
            {
                mappings.Add("A(0,0,0)", new List<HabHygRecord>());
                mappings.Add("A(-1,0,0)", new List<HabHygRecord>());
                mappings.Add("A(1,0,0)", new List<HabHygRecord>());

                mappings.Add("A(0,1,0)", new List<HabHygRecord>());
                mappings.Add("A(-1,1,0)", new List<HabHygRecord>());
                mappings.Add("A(1,1,0)", new List<HabHygRecord>());

                mappings.Add("A(0,-1,0)", new List<HabHygRecord>());
                mappings.Add("A(-1,-1,0)", new List<HabHygRecord>());
                mappings.Add("A(1,-1,0)", new List<HabHygRecord>());


                mappings.Add("A(0,0,1)", new List<HabHygRecord>());
                mappings.Add("A(-1,0,1)", new List<HabHygRecord>());
                mappings.Add("A(1,0,1)", new List<HabHygRecord>());

                mappings.Add("A(0,1,1)", new List<HabHygRecord>());
                mappings.Add("A(-1,1,1)", new List<HabHygRecord>());
                mappings.Add("A(1,1,1)", new List<HabHygRecord>());

                mappings.Add("A(0,-1,1)", new List<HabHygRecord>());
                mappings.Add("A(-1,-1,1)", new List<HabHygRecord>());
                mappings.Add("A(1,-1,1)", new List<HabHygRecord>());


                mappings.Add("A(0,0,-1)", new List<HabHygRecord>());
                mappings.Add("A(-1,0,-1)", new List<HabHygRecord>());
                mappings.Add("A(1,0,-1)", new List<HabHygRecord>());

                mappings.Add("A(0,1,-1)", new List<HabHygRecord>());
                mappings.Add("A(-1,1,-1)", new List<HabHygRecord>());
                mappings.Add("A(1,1,-1)", new List<HabHygRecord>());

                mappings.Add("A(0,-1,-1)", new List<HabHygRecord>());
                mappings.Add("A(-1,-1,-1)", new List<HabHygRecord>());
                mappings.Add("A(1,-1,-1)", new List<HabHygRecord>());
            }

            double parsec = l / 3.261633;

            foreach (HabHygRecord rec in stars)
            {
                Point3d p = new Point3d(double.Parse(rec.Xg), double.Parse(rec.Yg), double.Parse(rec.Zg));

                string area = GetArea(parsec, p);
                if (!string.IsNullOrEmpty(area))
                {
                    mappings[area].Add(rec);
                }
            }

            return mappings;
        }
    }
}

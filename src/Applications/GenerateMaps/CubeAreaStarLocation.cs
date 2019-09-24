using System;
using System.Collections.Generic;
using System.Text;

using StellarMap.Catalogues;

namespace StellarMap.GenerateMaps
{
    public class CubeAreaStarLocation
    {
        IDictionary<string, Func<double, double, double, double, bool>> AreaMappings = new Dictionary<string, Func<double, double, double, double, bool>>();

        public CubeAreaStarLocation()
        {
            {
                AreaMappings.Add("A(0,0,0)", (l, x, y, z) => ((x < l / 2 && x > -l / 2 && y < l / 2 && y > -l / 2 && z < l / 2 && z > -l / 2)));
                AreaMappings.Add("A(-1,0,0)", (l, x, y, z) => ((x < -l / 2 && x > -3 * l / 2 && y < l / 2 && y > -l / 2 && z < l / 2 && z > -l / 2)));
                AreaMappings.Add("A(1,0,0)", (l, x, y, z) => ((x < 3 * l / 2 && x > l / 2 && y < l / 2 && y > -l / 2 && z < l / 2 && z > -l / 2)));

                AreaMappings.Add("A(0,1,0)", (l, x, y, z) => ((x < l / 2 && x > -l / 2 && y < 3 * l / 2 && y > l / 2 && z < l / 2 && z > -l / 2)));
                AreaMappings.Add("A(-1,1,0)", (l, x, y, z) => ((x < -l / 2 && x > -3 * l / 2 && y < 3 * l / 2 && y > l / 2 && z < l / 2 && z > -l / 2)));
                AreaMappings.Add("A(1,1,0)", (l, x, y, z) => ((x < 3 * l / 2 && x > l / 2 && y < 3 * l / 2 && y > l / 2 && z < l / 2 && z > -l / 2)));

                AreaMappings.Add("A(0,-1,0)", (l, x, y, z) => ((x < l / 2 && x > -l / 2 && y < -l / 2 && y > -3 * l / 2 && z < l / 2 && z > -l / 2)));
                AreaMappings.Add("A(-1,-1,0)", (l, x, y, z) => ((x < -l / 2 && x > -3 * l / 2 && y < -l / 2 && y > -3 * l / 2 && z < l / 2 && z > -l / 2)));
                AreaMappings.Add("A(1,-1,0)", (l, x, y, z) => ((x < 3 * l / 2 && x > l / 2 && y < -l / 2 && y > -3 * l / 2 && z < l / 2 && z > -l / 2)));


                AreaMappings.Add("A(0,0,1)", (l, x, y, z) => ((x < l / 2 && x > -l / 2 && y < l / 2 && y > -l / 2 && z < 3 * l / 2 && z > l / 2)));
                AreaMappings.Add("A(-1,0,1)", (l, x, y, z) => ((x < -l / 2 && x > -3 * l / 2 && y < l / 2 && y > -l / 2 && z < 3 * l / 2 && z > l / 2)));
                AreaMappings.Add("A(1,0,1)", (l, x, y, z) => ((x < 3 * l / 2 && x > l / 2 && y < l / 2 && y > -l / 2 && z < 3 * l / 2 && z > l / 2)));

                AreaMappings.Add("A(0,1,1)", (l, x, y, z) => ((x < l / 2 && x > -l / 2 && y < 3 * l / 2 && y > l / 2 && z < 3 * l / 2 && z > l / 2)));
                AreaMappings.Add("A(-1,1,1)", (l, x, y, z) => ((x < -l / 2 && x > -3 * l / 2 && y < 3 * l / 2 && y > l / 2 && z < 3 * l / 2 && z > l / 2)));
                AreaMappings.Add("A(1,1,1)", (l, x, y, z) => ((x < 3 * l / 2 && x > l / 2 && y < 3 * l / 2 && y > l / 2 && z < 3 * l / 2 && z > l / 2)));

                AreaMappings.Add("A(0,-1,1)", (l, x, y, z) => ((x < l / 2 && x > -l / 2 && y < -l / 2 && y > -3 * l / 2 && z < 3 * l / 2 && z > l / 2)));
                AreaMappings.Add("A(-1,-1,1)", (l, x, y, z) => ((x < -l / 2 && x > -3 * l / 2 && y < -l / 2 && y > -3 * l / 2 && z < 3 * l / 2 && z > l / 2)));
                AreaMappings.Add("A(1,-1,1)", (l, x, y, z) => ((x < 3 * l / 2 && x > l / 2 && y < -l / 2 && y > -3 * l / 2 && z < 3 * l / 2 && z > l / 2)));


                AreaMappings.Add("A(0,0,-1)", (l, x, y, z) => ((x < l / 2 && x > -l / 2 && y < l / 2 && y > -l / 2 && z < -l / 2 && z > -3 * l / 2)));
                AreaMappings.Add("A(-1,0,-1)", (l, x, y, z) => ((x < -l / 2 && x > -3 * l / 2 && y < l / 2 && y > -l / 2 && z < -l / 2 && z > -3 * l / 2)));
                AreaMappings.Add("A(1,0,-1)", (l, x, y, z) => ((x < 3 * l / 2 && x > l / 2 && y < l / 2 && y > -l / 2 && z < -l / 2 && z > -3 * l / 2)));

                AreaMappings.Add("A(0,1,-1)", (l, x, y, z) => ((x < l / 2 && x > -l / 2 && y < 3 * l / 2 && y > l / 2 && z < -l / 2 && z > -3 * l / 2)));
                AreaMappings.Add("A(-1,1,-1)", (l, x, y, z) => ((x < -l / 2 && x > -3 * l / 2 && y < 3 * l / 2 && y > l / 2 && z < -l / 2 && z > -3 * l / 2)));
                AreaMappings.Add("A(1,1,-1)", (l, x, y, z) => ((x < 3 * l / 2 && x > l / 2 && y < 3 * l / 2 && y > l / 2 && z < -l / 2 && z > -3 * l / 2)));

                AreaMappings.Add("A(0,-1,-1)", (l, x, y, z) => ((x < l / 2 && x > -l / 2 && y < -l / 2 && y > -3 * l / 2 && z < -l / 2 && z > -3 * l / 2)));
                AreaMappings.Add("A(-1,-1,-1)", (l, x, y, z) => ((x < -l / 2 && x > -3 * l / 2 && y < -l / 2 && y > -3 * l / 2 && z < -l / 2 && z > -3 * l / 2)));
                AreaMappings.Add("A(1,-1,-1)", (l, x, y, z) => ((x < 3 * l / 2 && x > l / 2 && y < -l / 2 && y > -3 * l / 2 && z < -l / 2 && z > -3 * l / 2)));
            }
        }

        public string GetArea(double l, double x, double y, double z)
        {
            string area = string.Empty;

            foreach (var kvp in AreaMappings)
            {
                if (kvp.Value(l, x, y, z))
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
                double x = double.Parse(rec.Xg);
                double y = double.Parse(rec.Yg);
                double z = double.Parse(rec.Zg);

                string area = GetArea(parsec, x, y, z);
                if (!string.IsNullOrEmpty(area))
                {
                    mappings[area].Add(rec);
                }
            }

            return mappings;
        }
    }
}

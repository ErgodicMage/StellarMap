namespace TestCatalogues;

public class CubeAreaStarLocation
{
    readonly IDictionary<string, Func<double, Point3d, bool>> AreaMappings = new Dictionary<string, Func<double, Point3d, bool>>();

    public CubeAreaStarLocation()
    {
        {
            AreaMappings.Add("A(0,0,0)", (l, p) => (p.X < l / 2 && p.X > -l / 2 && p.Y < l / 2 && p.Y > -l / 2 && p.Z < l / 2 && p.Z > -l / 2));
            AreaMappings.Add("A(-1,0,0)", (l, p) => (p.X < -l / 2 && p.X > -3 * l / 2 && p.Y < l / 2 && p.Y > -l / 2 && p.Z < l / 2 && p.Z > -l / 2));
            AreaMappings.Add("A(1,0,0)", (l, p) => (p.X < 3 * l / 2 && p.X > l / 2 && p.Y < l / 2 && p.Y > -l / 2 && p.Z < l / 2 && p.Z > -l / 2));

            AreaMappings.Add("A(0,1,0)", (l, p) => (p.X < l / 2 && p.X > -l / 2 && p.Y < 3 * l / 2 && p.Y > l / 2 && p.Z < l / 2 && p.Z > -l / 2));
            AreaMappings.Add("A(-1,1,0)", (l, p) => (p.X < -l / 2 && p.X > -3 * l / 2 && p.Y < 3 * l / 2 && p.Y > l / 2 && p.Z < l / 2 && p.Z > -l / 2));
            AreaMappings.Add("A(1,1,0)", (l, p) => (p.X < 3 * l / 2 && p.X > l / 2 && p.Y < 3 * l / 2 && p.Y > l / 2 && p.Z < l / 2 && p.Z > -l / 2));

            AreaMappings.Add("A(0,-1,0)", (l, p) => (p.X < l / 2 && p.X > -l / 2 && p.Y < -l / 2 && p.Y > -3 * l / 2 && p.Z < l / 2 && p.Z > -l / 2));
            AreaMappings.Add("A(-1,-1,0)", (l, p) => (p.X < -l / 2 && p.X > -3 * l / 2 && p.Y < -l / 2 && p.Y > -3 * l / 2 && p.Z < l / 2 && p.Z > -l / 2));
            AreaMappings.Add("A(1,-1,0)", (l, p) => (p.X < 3 * l / 2 && p.X > l / 2 && p.Y < -l / 2 && p.Y > -3 * l / 2 && p.Z < l / 2 && p.Z > -l / 2));


            AreaMappings.Add("A(0,0,1)", (l, p) => (p.X < l / 2 && p.X > -l / 2 && p.Y < l / 2 && p.Y > -l / 2 && p.Z < 3 * l / 2 && p.Z > l / 2));
            AreaMappings.Add("A(-1,0,1)", (l, p) => (p.X < -l / 2 && p.X > -3 * l / 2 && p.Y < l / 2 && p.Y > -l / 2 && p.Z < 3 * l / 2 && p.Z > l / 2));
            AreaMappings.Add("A(1,0,1)", (l, p) => (p.X < 3 * l / 2 && p.X > l / 2 && p.Y < l / 2 && p.Y > -l / 2 && p.Z < 3 * l / 2 && p.Z > l / 2));

            AreaMappings.Add("A(0,1,1)", (l, p) => (p.X < l / 2 && p.X > -l / 2 && p.Y < 3 * l / 2 && p.Y > l / 2 && p.Z < 3 * l / 2 && p.Z > l / 2));
            AreaMappings.Add("A(-1,1,1)", (l, p) => (p.X < -l / 2 && p.X > -3 * l / 2 && p.Y < 3 * l / 2 && p.Y > l / 2 && p.Z < 3 * l / 2 && p.Z > l / 2));
            AreaMappings.Add("A(1,1,1)", (l, p) => (p.X < 3 * l / 2 && p.X > l / 2 && p.Y < 3 * l / 2 && p.Y > l / 2 && p.Z < 3 * l / 2 && p.Z > l / 2));

            AreaMappings.Add("A(0,-1,1)", (l, p) => (p.X < l / 2 && p.X > -l / 2 && p.Y < -l / 2 && p.Y > -3 * l / 2 && p.Z < 3 * l / 2 && p.Z > l / 2));
            AreaMappings.Add("A(-1,-1,1)", (l, p) => (p.X < -l / 2 && p.X > -3 * l / 2 && p.Y < -l / 2 && p.Y > -3 * l / 2 && p.Z < 3 * l / 2 && p.Z > l / 2));
            AreaMappings.Add("A(1,-1,1)", (l, p) => (p.X < 3 * l / 2 && p.X > l / 2 && p.Y < -l / 2 && p.Y > -3 * l / 2 && p.Z < 3 * l / 2 && p.Z > l / 2));


            AreaMappings.Add("A(0,0,-1)", (l, p) => (p.X < l / 2 && p.X > -l / 2 && p.Y < l / 2 && p.Y > -l / 2 && p.Z < -l / 2 && p.Z > -3 * l / 2));
            AreaMappings.Add("A(-1,0,-1)", (l, p) => (p.X < -l / 2 && p.X > -3 * l / 2 && p.Y < l / 2 && p.Y > -l / 2 && p.Z < -l / 2 && p.Z > -3 * l / 2));
            AreaMappings.Add("A(1,0,-1)", (l, p) => (p.X < 3 * l / 2 && p.X > l / 2 && p.Y < l / 2 && p.Y > -l / 2 && p.Z < -l / 2 && p.Z > -3 * l / 2));

            AreaMappings.Add("A(0,1,-1)", (l, p) => (p.X < l / 2 && p.X > -l / 2 && p.Y < 3 * l / 2 && p.Y > l / 2 && p.Z < -l / 2 && p.Z > -3 * l / 2));
            AreaMappings.Add("A(-1,1,-1)", (l, p) => (p.X < -l / 2 && p.X > -3 * l / 2 && p.Y < 3 * l / 2 && p.Y > l / 2 && p.Z < -l / 2 && p.Z > -3 * l / 2));
            AreaMappings.Add("A(1,1,-1)", (l, p) => (p.X < 3 * l / 2 && p.X > l / 2 && p.Y < 3 * l / 2 && p.Y > l / 2 && p.Z < -l / 2 && p.Z > -3 * l / 2));

            AreaMappings.Add("A(0,-1,-1)", (l, p) => (p.X < l / 2 && p.X > -l / 2 && p.Y < -l / 2 && p.Y > -3 * l / 2 && p.Z < -l / 2 && p.Z > -3 * l / 2));
            AreaMappings.Add("A(-1,-1,-1)", (l, p) => (p.X < -l / 2 && p.X > -3 * l / 2 && p.Y < -l / 2 && p.Y > -3 * l / 2 && p.Z < -l / 2 && p.Z > -3 * l / 2));
            AreaMappings.Add("A(1,-1,-1)", (l, p) => (p.X < 3 * l / 2 && p.X > l / 2 && p.Y < -l / 2 && p.Y > -3 * l / 2 && p.Z < -l / 2 && p.Z > -3 * l / 2));
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

    public IDictionary<string, List<HabHygRecord>> GetAreaMappings(double l, List<HabHygRecord> stars)
    {
        IDictionary<string, List<HabHygRecord>> mappings = new Dictionary<string, List<HabHygRecord>>();
        {
            mappings.Add("A(0,0,0)", new());
            mappings.Add("A(-1,0,0)", new());
            mappings.Add("A(1,0,0)", new());

            mappings.Add("A(0,1,0)", new());
            mappings.Add("A(-1,1,0)", new());
            mappings.Add("A(1,1,0)", new());

            mappings.Add("A(0,-1,0)", new());
            mappings.Add("A(-1,-1,0)", new());
            mappings.Add("A(1,-1,0)", new());


            mappings.Add("A(0,0,1)", new());
            mappings.Add("A(-1,0,1)", new());
            mappings.Add("A(1,0,1)", new());

            mappings.Add("A(0,1,1)", new());
            mappings.Add("A(-1,1,1)", new());
            mappings.Add("A(1,1,1)", new());

            mappings.Add("A(0,-1,1)", new());
            mappings.Add("A(-1,-1,1)", new());
            mappings.Add("A(1,-1,1)", new());


            mappings.Add("A(0,0,-1)", new());
            mappings.Add("A(-1,0,-1)", new());
            mappings.Add("A(1,0,-1)", new());

            mappings.Add("A(0,1,-1)", new());
            mappings.Add("A(-1,1,-1)", new());
            mappings.Add("A(1,1,-1)", new());

            mappings.Add("A(0,-1,-1)", new());
            mappings.Add("A(-1,-1,-1)", new());
            mappings.Add("A(1,-1,-1)", new());
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

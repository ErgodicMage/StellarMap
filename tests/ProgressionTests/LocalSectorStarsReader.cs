using System.Collections.Generic;
using System.IO;

using CsvHelper;
using CsvHelper.Configuration;

using StellarMap.Math.Types;

namespace ProgressionTests
{
    public class LocalSectorStar
    {
        public LocalSectorStar()
        {
        }

        public string Designation { get; set; }
        public Point3d Position { get; set; }
        public string Name { get; set; }
        public string Cluster { get; set; }
    }

    public class LocalSectorStarsReader
    {
        public IList<LocalSectorStar> Stars { get; set; }

        public void Load(string starsFile)
        {
            Stars = new List<LocalSectorStar>();

            CsvReader reader = new CsvReader(File.OpenText(starsFile));
            
            reader.Read();
            reader.ReadHeader();
            while (reader.Read())
            {
                string designation = reader["Designation"];

                if (string.IsNullOrEmpty(designation) || designation.StartsWith("A("))
                    continue;

                LocalSectorStar star = new LocalSectorStar();
                star.Designation = designation;

                string pos = reader["Position"];
                if (!string.IsNullOrEmpty(pos) && Point3d.TryParse(pos, out var position))
                    star.Position = position;

                star.Name = reader["Name"];
                star.Cluster = reader["Cluster"];

                Stars.Add(star);
            }
        }
    }
}

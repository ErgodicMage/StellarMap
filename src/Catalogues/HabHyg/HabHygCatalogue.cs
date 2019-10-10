using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using StellarMap.Core.Bodies;
using StellarMap.Core.Types;

//using CsvHelper;
//using CsvHelper.Configuration;


namespace StellarMap.Catalogues
{
    public class HabHygCatalogue : IRealisticCatalogue
    {
        #region Properties
        public string Name => "HabHyg";

        public string Description => "Catalogue from Winchell Chung";

        public string Source => "http://www.projectrho.com/public_html/starmaps";

        public string Location { get; set; }

        public bool AddDataAsProperties { get; set; }
        #endregion

        #region Functions
        public IList<Type> GetTypes()
        {
            IList<Type> types = new List<Type>();
            types.Add(typeof(Star));
            return types;
        }

        public bool HasType(Type t) => t == typeof(Star);

        public IStellarMap Get()
        {
            IStellarMap map = BaseStellarMap.DefaultMap;
            Get(map);
            return map;
        }

        public void Get(IStellarMap map)
        {
            HabHYGCsvReader reader = new HabHYGCsvReader();
            reader.Load(Location);

            foreach (HabHygRecord record in reader.Catalogue)
            {
                Star star = Convert(record);
                map.Add<Star>(star);
            }
        }

        public IStellarMap GetWithin(double ly)
        {
            IStellarMap map = BaseStellarMap.DefaultMap;
            GetWithin(map, ly);
            return map;
        }

        public void GetWithin(IStellarMap map, double ly)
        {
            double parsecs = ly / 3.261633;

            HabHYGCsvReader reader = new HabHYGCsvReader();
            reader.Load(Location);

            var records = reader.Catalogue.Where<HabHygRecord>(c => c.Distance < parsecs);

            foreach(HabHygRecord record in records)
            {
                Star star = Convert(record);
                map.Add<Star>(star);
            }

        }

        public IStellarMap GetWithin(double ly, double magnitude)
        {
            IStellarMap map = BaseStellarMap.DefaultMap;
            GetWithin(map, ly, magnitude);
            return map;
        }

        public void GetWithin(IStellarMap map, double ly, double magnitude)
        {
            double parsecs = ly / 3.261633;

            HabHYGCsvReader reader = new HabHYGCsvReader();
            reader.Load(Location);

            var records = reader.Catalogue.Where<HabHygRecord>(c => c.Distance < parsecs && c.AbsMag > magnitude);

            foreach (HabHygRecord record in records)
            {
                Star star = Convert(record);
                map.Add<Star>(star);
            }
        }

        public IStellarMap GetMagnitude(double magnitude)
        {
            IStellarMap map = BaseStellarMap.DefaultMap;
            GetMagnitude(map, magnitude);
            return map;
        }

        public void GetMagnitude(IStellarMap map, double magnitude)
        {
            HabHYGCsvReader reader = new HabHYGCsvReader();
            reader.Load(Location);

            var records = reader.Catalogue.Where<HabHygRecord>(c => c.AbsMag > magnitude);

            foreach (HabHygRecord record in records)
            {
                Star star = Convert(record);
                map.Add<Star>(star);
            }
        }

        public IList<HabHygRecord> GetRaw()
        {
            HabHYGCsvReader reader = new HabHYGCsvReader();
            reader.Load(Location);
            return reader.Catalogue;
        }
        #endregion

        #region Private Functions
        private Star Convert(HabHygRecord record)
        {
            Star star = new Star(record.DisplayName);
            star.Identifier = string.Format("HabHyg-{0:D5}", record.HabHyg);
            star.Name = record.DisplayName;

            if (AddDataAsProperties)
            {
                IDictionary<string, string> prop = new Dictionary<string, string>();
                prop.Add("HabHyg", record.HabHyg.ToString());
                prop.Add("Hip", record.Hip);
                prop.Add("Hab", record.Hab);
                prop.Add("Display Name", record.DisplayName);
                prop.Add("Hyg", record.Hyg);
                prop.Add("BayerFlamsteed", record.BayerFlamsteed);
                prop.Add("Gliese", record.Gliese);
                prop.Add("BD", record.BD);
                prop.Add("HD", record.HD);
                prop.Add("HR", record.HR);
                prop.Add("Proper Name", record.ProperName);
                prop.Add("Spectral Class", record.SpectralClass);
                prop.Add("Distance", record.Distance.ToString());
                prop.Add("Xg", record.Xg);
                prop.Add("Yg", record.Yg);
                prop.Add("Zg", record.Zg);
                prop.Add("AbsMag", record.AbsMag.ToString());
                star.Properties.AddGroup("HabHyg", prop);
            }

            return star;
        }
        #endregion
    }
}

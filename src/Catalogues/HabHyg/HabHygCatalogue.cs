using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using StellarMap.Core.Bodies;
using StellarMap.Core.Types;

using CsvHelper;
using CsvHelper.Configuration;


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
            Load();

            foreach (HabHygRecord record in catalogue)
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

            Load();

            var records = catalogue.Where<HabHygRecord>(c => c.Distance < parsecs);

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

            Load();

            var records = catalogue.Where<HabHygRecord>(c => c.Distance < parsecs && c.AbsMag > magnitude);

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
            Load();

            var records = catalogue.Where<HabHygRecord>(c => c.AbsMag > magnitude);

            foreach (HabHygRecord record in records)
            {
                Star star = Convert(record);
                map.Add<Star>(star);
            }
        }

        public IList<HabHygRecord> GetRaw()
        {
            Load();
            return catalogue;
        }
        #endregion

        #region Private Functions
        public class HabHygRecord
        {
            public long HabHyg { get; set; }
            public string Hip { get; set; }
            public string Hab { get; set; }
            public string DisplayName { get; set; }
            public string Hyg { get; set; }
            public string BayerFlamsteed { get; set; }
            public string Gliese { get; set; }
            public string BD { get; set; }
            public string HD { get; set; }
            public string HR { get; set; }
            public string ProperName { get; set; }
            public string SpectralClass { get; set; }
            public double Distance { get; set; }
            public string Xg { get; set; }
            public string Yg { get; set; }
            public string Zg { get; set; }
            public double AbsMag { get; set; }
        }

        public class HabHYGRecordMap : ClassMap<HabHygRecord>
        {
            public HabHYGRecordMap()
            {
                AutoMap();
                Map(m => m.Hab).Name("Hab?");
                Map(m => m.DisplayName).Name("Display Name");
                Map(m => m.ProperName).Name("Proper Name");
                Map(m => m.SpectralClass).Name("Spectral Class");
            }
        }


        private IList<HabHygRecord> catalogue;

        private void Load()
        {
            if (catalogue == null)
            {
                catalogue = new List<HabHygRecord>();

                CsvReader reader = new CsvReader(File.OpenText(Location));
                reader.Configuration.RegisterClassMap<HabHYGRecordMap>();
                reader.Read();
                reader.ReadHeader();
                while (reader.Read())
                {
                    HabHygRecord record = reader.GetRecord<HabHygRecord>();
                    catalogue.Add(record);
                }
            }
        }

        private Star Convert(HabHygRecord record)
        {
            Star star = new Star(record.DisplayName);
            star.Identifier = string.Format("HabHyg-{0:D5}", record.HabHyg);
            star.Name = record.DisplayName;

            if (AddDataAsProperties)
            {
                GroupProperties prop = new GroupProperties("HabHyg");
                prop.Properties.Add("HabHyg", record.HabHyg.ToString());
                prop.Properties.Add("Hip", record.Hip);
                prop.Properties.Add("Hab", record.Hab);
                prop.Properties.Add("Display Name", record.DisplayName);
                prop.Properties.Add("Hyg", record.Hyg);
                prop.Properties.Add("BayerFlamsteed", record.BayerFlamsteed);
                prop.Properties.Add("Gliese", record.Gliese);
                prop.Properties.Add("BD", record.BD);
                prop.Properties.Add("HD", record.HD);
                prop.Properties.Add("HR", record.HR);
                prop.Properties.Add("Proper Name", record.ProperName);
                prop.Properties.Add("Spectral Class", record.SpectralClass);
                prop.Properties.Add("Distance", record.Distance.ToString());
                prop.Properties.Add("Xg", record.Xg);
                prop.Properties.Add("Yg", record.Yg);
                prop.Properties.Add("Zg", record.Zg);
                prop.Properties.Add("AbsMag", record.AbsMag.ToString());
                star.AllGroupProperties.Add("HabHyg", prop);
            }

            return star;
        }
        #endregion
    }
}

using System.Collections.Generic;
using System.IO;

using CsvHelper;
using CsvHelper.Configuration;

namespace StellarMap.Catalogues
{
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

    public class HabHYGCsvReader
    {
        public IList<HabHygRecord> Catalogue { get; set; }

        public void Load(string catalogueFile)
        {
            Catalogue = new List<HabHygRecord>();

            CsvReader reader = new CsvReader(File.OpenText(catalogueFile));
            reader.Configuration.RegisterClassMap<HabHYGRecordMap>();
            reader.Read();
            reader.ReadHeader();
            while (reader.Read())
            {
                HabHygRecord record = reader.GetRecord<HabHygRecord>();
                Catalogue.Add(record);
            }
        }
    }
}

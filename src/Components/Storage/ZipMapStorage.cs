using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;

namespace StellarMap.Storage;

public class ZipMapStorage : IMapStorage
{
    #region Constructor
    public ZipMapStorage()
    {
    }
    #endregion

    public Result Store(IStellarMap map, StreamWriter writer)
    {
        Result guardResult = GuardClause.Null(map).Null(writer);
        if (!guardResult.Success) return guardResult;

        return Result.Try<IStellarMap, StreamWriter>((map, writer) =>
        {
            ZipStrings.UseUnicode = true;

            using var stream = new ZipOutputStream(writer.BaseStream);
            stream.IsStreamOwner = false;

            string json = JsonConvert.SerializeObject(map.MetaData, Formatting.Indented);
            byte[] bytes = Encoding.Default.GetBytes(json);
            byte[] buffer = new byte[4096];

            var entry = new ZipEntry("MetaData.json");
            stream.PutNextEntry(entry);

            using (var memory = new MemoryStream(bytes))
                StreamUtils.Copy(memory, stream, buffer);

            var bodytypes = map.GetBodyTypes();

            foreach (string bodytype in bodytypes)
            {
                var body = map.GetBody(bodytype);

                if (!body.Success)
                    continue;

                json = JsonConvert.SerializeObject(body, Formatting.Indented);
                bytes = Encoding.Default.GetBytes(json);

                entry = new ZipEntry(bodytype + "s.json");
                stream.PutNextEntry(entry);

                using var memory = new MemoryStream(bytes);
                StreamUtils.Copy(memory, stream, buffer);
            }
        },
        map, writer);
    }

    public Result<T> Retreive<T>(StreamReader reader)  where T : IStellarMap, new()
    {
        Result guardResult = GuardClause.Null(reader);
        if (!guardResult.Success) return guardResult;

        return Result<T>.Try<StreamReader>((reader) =>
            {
                T map = new();

                ZipStrings.UseUnicode = true;
            
                byte[] buffer = new byte[4096];

                using var stream = new ZipInputStream(reader.BaseStream);
                ZipEntry entry;
                while ((entry = stream.GetNextEntry()) != null)
                {
                    string json = string.Empty;

                    var memory = new MemoryStream();
                    StreamUtils.Copy(stream, memory, buffer);
                    memory.Position = 0;

                    using StreamReader sr = new StreamReader(memory);
                    json = sr.ReadToEnd();

                    if (!string.IsNullOrEmpty(json))
                    {
                        if (entry.Name == "MetaData.json")
                        {
                            var metaData = JsonConvert.DeserializeObject<GroupedProperties>(json);
                            map.MetaData = metaData is not null ? metaData : new GroupedProperties();
                        }
                        else
                        {
                            string bodytype = entry.Name.Replace("s.json", "");
                            Type? t = map.GetTypeOfBody(bodytype);
                            if (t is not null)
                            {
                                object? data = JsonConvert.DeserializeObject(json, t);
                                if (data is not null)
                                    map.SetBody(bodytype, data);
                            }
                        }
                    }
                }

                map.SetMap();
                return map;
            },
            reader);
    }
}

﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using StellarMap.Core.Types;

using Newtonsoft.Json;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;


namespace StellarMap.Storage
{
    public class ZipMapStorage : IMapStorage
    {
        #region Constructor
        public ZipMapStorage()
        {
        }
        #endregion

        public void Store(IStellarMap map, StreamWriter writer)
        {
            ZipStrings.UseUnicode = true;

            using (ZipOutputStream stream = new ZipOutputStream(writer.BaseStream))
            {
                stream.IsStreamOwner = false;

                string json = JsonConvert.SerializeObject(map.MetaData, Formatting.Indented);
                byte[] bytes = Encoding.Default.GetBytes(json);
                byte[] buffer = new byte[4096];

                ZipEntry entry = new ZipEntry("MetaData.json");
                stream.PutNextEntry(entry);

                using (MemoryStream memory = new MemoryStream(bytes))
                    StreamUtils.Copy(memory, stream, buffer);

                IList<string> bodytypes = map.GetBodyTypes();

                foreach (string bodytype in bodytypes)
                {
                    object body = map.GetBody(bodytype);

                    if (body != null)
                    {
                        json = JsonConvert.SerializeObject(body, Formatting.Indented);
                        bytes = Encoding.Default.GetBytes(json);

                        entry = new ZipEntry(bodytype + "s.json");
                        stream.PutNextEntry(entry);

                        using MemoryStream memory = new MemoryStream(bytes);
                        StreamUtils.Copy(memory, stream, buffer);
                    }
                }
            }

            //stream.Finish(); // this writes the entries at the end of the zip stream but does not close the stream.
        }

        public T Retreive<T>(StreamReader reader)  where T : IStellarMap, new()
        {
            T map = new T();

            ZipStrings.UseUnicode = true;
            
            byte[] buffer = new byte[4096];            

            using (ZipInputStream stream = new ZipInputStream(reader.BaseStream))
            {
                ZipEntry entry = null;
                while ((entry = stream.GetNextEntry()) != null)
                {
                    string json = string.Empty;

                    MemoryStream memory = new MemoryStream();
                    StreamUtils.Copy(stream, memory, buffer);
                    memory.Position = 0;

                    using StreamReader sr = new StreamReader(memory);
                    json = sr.ReadToEnd();

                    if (!string.IsNullOrEmpty(json))
                    {
                        if (entry.Name == "MetaData.json")
                            map.MetaData = JsonConvert.DeserializeObject<GroupedProperties>(json);
                        else
                        {
                            string bodytype = entry.Name.Replace("s.json", "");
                            Type t = map.GetTypeOfBody(bodytype);
                            object data = JsonConvert.DeserializeObject(json, t);

                            map.SetBody(bodytype, data);
                        }
                    }

                }
            }

            map.SetMap();

            return map;
        }
    }
}

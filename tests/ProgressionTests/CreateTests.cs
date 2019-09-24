using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using StellarMap.Core.Bodies;
using StellarMap.Core.Types;
using StellarMap.Math.Types;

using StellarMap.Progression;
using StellarMap.Progression.DefaultSettingMaps;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ProgressionTests
{
    [TestClass]
    public class CreateTests
    {
        [TestMethod]
        public void CreateEarthTest()
        {
            ProgressionMap map = new ProgressionMap("Earth");

            LocalSectorMap csol = new LocalSectorMap(map);
            Planet earth = csol.CreateEarth();
        }

        [TestMethod]
        public void CreateSolTest()
        {
            ProgressionMap map = new ProgressionMap("Sol");

            LocalSectorMap csol = new LocalSectorMap(map);
            StarSystem sol = csol.CreateSolSystem();

        }

        [TestMethod]
        public void CreateSolClusterTest()
        {
            ProgressionMap map = new ProgressionMap("Sol Cluster");

            LocalSectorMap csol = new LocalSectorMap(map);
            Cluster solCluster = csol.CreateSolCluster();

        }

        [TestMethod]
        public void CreateLocalSector()
        {
            ProgressionMap map = new ProgressionMap("Local Sector");

            LocalSectorMap c = new LocalSectorMap(map);
            Sector sector = c.CreateLocalSector();
        }

        // wrote this test method because I started catching myself reusing real stars.
        // sometimes it's hard to work with the designations, so I can now search the text file.
        [TestMethod]
        public void ListStarSystemDesignations()
        {
            ProgressionMap map = new ProgressionMap("Local");

            LocalSectorMap c = new LocalSectorMap(map);
            Sector sector = c.CreateLocalSector();

            string outfile = @"C:\Development\StellarMap\TestData\Designations.txt";
            if (File.Exists(outfile))
                File.Delete(outfile);

            using (StreamWriter writer = new StreamWriter(outfile))
            {
                foreach (var system in map.StarSystems)
                {
                    writer.Write(system.Key);
                    writer.Write(" : ");
                    writer.Write(system.Value.Name);
                    writer.Write(" : ");
                    if (system.Value.BasicProperties.ContainsKey(Constants.PropertyNames.Designation))
                        writer.Write(system.Value.BasicProperties[Constants.PropertyNames.Designation]);
                    else
                        writer.Write("None");
                    writer.WriteLine();
                }
            }
        }
    }
}

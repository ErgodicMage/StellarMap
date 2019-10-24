using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using StellarMap.Core.Bodies;
using StellarMap.Core.Types;

using StellarMap.Progression;
using StellarMap.Progression.DefaultSettingMaps;

namespace ProgressionTests
{
    [TestClass]
    public class EquatableTests
    {
        [TestMethod]
        //[TestCategory(TestCategories.UnitTest)]
        public void LocalSectorEqualTest()
        {
            ProgressionMap map = new ProgressionMap("Local Sector");
            LocalSectorMap c = new LocalSectorMap(map);
            c.CreateLocalSector();

            ProgressionMap map2 = new ProgressionMap("Local Sector");
            LocalSectorMap c2 = new LocalSectorMap(map2);
            c2.CreateLocalSector();

            Assert.IsTrue(map.Equals(map2));
        }
    }
}

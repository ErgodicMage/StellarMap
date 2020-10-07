using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using StellarMap.Core.Bodies;
using StellarMap.Core.Types;
using StellarMap.Math.Types;

using StellarMap.Traveller;

namespace TravellerTests
{
    [TestClass]
    public class CreateTests
    {
        [TestMethod]
        [TestCategory(TestCategories.UnitTest)]
        public void CreateWorldAramisTest()
        {
            TravellerMap map = new TravellerMap("Aramis World");

            SpinwardMarchesMap spinwardmarches = new SpinwardMarchesMap(map);
            World aramis = spinwardmarches.CreateAramisWorld();
        }

        [TestMethod]
        [TestCategory(TestCategories.UnitTest)]
        public void CreateSubsectorAramis()
        {
            TravellerMap map = new TravellerMap("Aramis Subsector");

            SpinwardMarchesMap spinwardmarches = new SpinwardMarchesMap(map);
            Subsector aramis = spinwardmarches.CreateAramisSubsector();
        }

        [TestMethod]
        [TestCategory(TestCategories.UnitTest)]
        public void CreateSpinwardMarchesTest()
        {
            TravellerMap map = new TravellerMap("Spinward Marches");

            SpinwardMarchesMap spinwardmarches = new SpinwardMarchesMap(map);
            Sector sector = spinwardmarches.CreateSector();
        }
    }
}

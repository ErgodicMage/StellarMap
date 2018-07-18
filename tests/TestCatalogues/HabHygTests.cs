using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using StellarMap.Core.Bodies;
using StellarMap.Core.Types;
using StellarMap.Catalogues;

namespace TestCatalogues
{
    [TestClass]
    public class HabHygTests
    {
        string folder = @"C:\Development\StellarMap\Catalogues\";

        [TestMethod]
        public void LoadCatalogue()
        {
            HabHygCatalogue catalogue = new HabHygCatalogue();
            catalogue.Location = folder + "HabHYG.csv";

            BaseStellarMap map = new BaseStellarMap("HabHyg All");
            catalogue.Get(map);

            map = new BaseStellarMap("HabHyg within 10ly");
            catalogue.GetWithin(map, 10);

            map = new BaseStellarMap("HabHyg higher than magnitude 4");
            catalogue.GetMagnitude(map, 4);

            map = new BaseStellarMap("HabHyg within 10ly higher that magnitude 4");
            catalogue.GetWithin(map, 10, 4);
        }
    }
}

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

            map = new BaseStellarMap("HabHyg within 5ly");
            catalogue.GetWithin(map, 5);

            map = new BaseStellarMap("HabHyg within 10ly");
            catalogue.GetWithin(map, 10);

            map = new BaseStellarMap("HabHyg within 15ly");
            catalogue.GetWithin(map, 15);

            map = new BaseStellarMap("HabHyg within 20ly");
            catalogue.GetWithin(map, 20);

            map = new BaseStellarMap("HabHyg within 30ly");
            catalogue.GetWithin(map, 30);

            map = new BaseStellarMap("HabHyg within 40ly");
            catalogue.GetWithin(map, 40);

            map = new BaseStellarMap("HabHyg within 50ly");
            catalogue.GetWithin(map, 50);

            map = new BaseStellarMap("HabHyg within 75ly");
            catalogue.GetWithin(map, 75);

            map = new BaseStellarMap("HabHyg within 100ly");
            catalogue.GetWithin(map, 100);

            map = new BaseStellarMap("HabHyg higher than magnitude 4");
            catalogue.GetMagnitude(map, 4);

            map = new BaseStellarMap("HabHyg within 10ly higher that magnitude 4");
            catalogue.GetWithin(map, 10, 4);
        }
    }
}

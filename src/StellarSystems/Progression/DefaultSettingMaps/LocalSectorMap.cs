using System;
using System.Collections.Generic;
using System.Text;

using StellarMap.Core.Bodies;
using StellarMap.Core.Types;
using StellarMap.Math.Types;

using StellarMap.Progression;

namespace StellarMap.Progression.DefaultSettingMaps
{
    public class LocalSectorMap
    {
        public LocalSectorMap(ProgressionMap map)
        {
            Map = map;
        }

        ProgressionMap Map { get; set; }

        public Planet CreateEarth()
        {
            ProgressionPlanet earth = new ProgressionPlanet("Earth");

            Map.Add<Planet>(earth);

            Satellite moon = new Satellite("Moon");
            earth.Add(moon);

            earth.Add(new Habitat("Space Station V"));
            earth.Add(new Habitat("Moon Base 1"));

            return earth;
        }

        public StarSystem CreateSolSystem()
        {
            ProgressionStar sol = new ProgressionStar("Sun");

            Map.Add<Star>(sol);
            sol.BasicProperties.Add(Constants.PropertyNames.Designation, "Sol");
            sol.BasicProperties.Add(Constants.PropertyNames.StellarClass, "G2V");
            GroupProperties catalogue = new GroupProperties("Catalogue");
            catalogue.Properties.Add("HabHyg", "0");
            catalogue.Properties.Add("Hip", "0");
            sol.AllGroupProperties.Add("Catalogue", catalogue);

            sol.Add(new Planet("Mercury"));
            sol.Add(new Planet("Venus"));

            Planet earth = CreateEarth();
            sol.Add(earth);

            sol.Add(new Planet("Mars"));
            sol.Add(new Planet("Jupiter"));
            sol.Add(new Planet("Saturn"));
            sol.Add(new Planet("Uranus"));
            sol.Add(new Planet("Neptune"));
            sol.Add(new Planet("Pluto"));

            sol.Add(new Asteroid("Ceres"));
            sol.Add(new Asteroid("Pallas"));
            sol.Add(new Asteroid("Juno"));

            sol.Add(new Comet("Haley's"));
            sol.Add(new Comet("Caeser's"));

            sol.Add(new Habitat("Ceres Station"));

            StarSystem solSystem = new StarSystem("Sol");
            Map.Add(solSystem);
            solSystem.Add(sol);

            return solSystem;
        }

        public Cluster CreateSolCluster()
        {
            Cluster solCluster = new Cluster("Sol Cluster");
            Map.Add(solCluster);

            StarSystem system = null;
            ProgressionStar star = null;
            GroupProperties catalogue = null;

            system = CreateSolSystem();
            solCluster.Add(system);

            #region Bernard's Star
            system = new StarSystem("Bernard");
            system.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 699");
            Map.Add(system);
            star = new ProgressionStar("Bernard's Star");
            star.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 699");
            star.BasicProperties.Add(Constants.PropertyNames.StellarClass, "sdM4");
            catalogue = new GroupProperties("Catalogue");
            catalogue.Properties.Add("HabHyg", "4");
            catalogue.Properties.Add("Hip", "87937");
            star.AllGroupProperties.Add("Catalogue", catalogue);
            system.Add(star);
            solCluster.Add(system);
            #endregion

            #region Sirius
            system = new StarSystem("Sirius");
            system.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 244");
            Map.Add(system);
            star = new ProgressionStar("Sirius");
            star.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 244");
            star.BasicProperties.Add(Constants.PropertyNames.StellarClass, "A0mA1Va");
            catalogue = new GroupProperties("Catalogue");
            catalogue.Properties.Add("HabHyg", "10");
            catalogue.Properties.Add("Hip", "32349");
            star.AllGroupProperties.Add("Catalogue", catalogue);
            system.Add(star);
            solCluster.Add(system);
            #endregion

            #region Luyten's Star
            system = new StarSystem("Luyten");
            system.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 273");
            Map.Add(system);
            star = new ProgressionStar("Luyten's Star");
            star.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 273");
            star.BasicProperties.Add(Constants.PropertyNames.StellarClass, "M5");
            catalogue = new GroupProperties("Catalogue");
            catalogue.Properties.Add("HabHyg", "28");
            catalogue.Properties.Add("Hip", "36208");
            star.AllGroupProperties.Add("Catalogue", catalogue);
            system.Add(star);
            solCluster.Add(system);
            #endregion

            #region Procyon
            system = new StarSystem("Procyon");
            system.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 280");
            Map.Add(system);
            star = new ProgressionStar("Procyon");
            star.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 280");
            star.BasicProperties.Add(Constants.PropertyNames.StellarClass, "F5IV-V");
            catalogue = new GroupProperties("Catalogue");
            catalogue.Properties.Add("HabHyg", "18");
            catalogue.Properties.Add("Hip", "37279");
            star.AllGroupProperties.Add("Catalogue", catalogue);
            system.Add(star);
            solCluster.Add(system);
            #endregion

            #region Lalande
            system = new StarSystem("Lalande");
            system.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 441");
            Map.Add(system);
            star = new ProgressionStar("Lalande");
            star.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 411");
            star.BasicProperties.Add(Constants.PropertyNames.StellarClass, "M2V");
            catalogue = new GroupProperties("Catalogue");
            catalogue.Properties.Add("HabHyg", "6");
            catalogue.Properties.Add("Hip", "54035");
            star.AllGroupProperties.Add("Catalogue", catalogue);
            system.Add(star);
            solCluster.Add(system);
            #endregion

            #region Epsilon Eridani
            system = new StarSystem("Epsilon Eridani");
            system.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 144");
            Map.Add(system);
            star = new ProgressionStar("Epsilon Eridani");
            star.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 144");
            star.BasicProperties.Add(Constants.PropertyNames.StellarClass, "K2V");
            catalogue = new GroupProperties("Catalogue");
            catalogue.Properties.Add("HabHyg", "13");
            catalogue.Properties.Add("Hip", "16537");
            star.AllGroupProperties.Add("Catalogue", catalogue);
            system.Add(star);
            solCluster.Add(system);
            #endregion

            #region Ross
            system = new StarSystem("Ross");
            system.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 447");
            Map.Add(system);
            star = new ProgressionStar("Pocks");
            star.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 447/Ross 128");
            star.BasicProperties.Add(Constants.PropertyNames.StellarClass, "M4.5V");
            catalogue = new GroupProperties("Catalogue");
            catalogue.Properties.Add("HabHyg", "14");
            catalogue.Properties.Add("Hip", "57548");
            star.AllGroupProperties.Add("Catalogue", catalogue);
            system.Add(star);
            solCluster.Add(system);
            #endregion

            #region Aquarii 
            system = new StarSystem("Aquarii");
            system.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 866");
            Map.Add(system);
            star = new ProgressionStar("Alvin");
            star.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 866 A");
            star.BasicProperties.Add(Constants.PropertyNames.StellarClass, "M5VJ");
            catalogue = new GroupProperties("Catalogue");
            catalogue.Properties.Add("HabHyg", "16");
            catalogue.Properties.Add("Hip", "-1");
            star.AllGroupProperties.Add("Catalogue", catalogue);
            system.Add(star);
            star = new ProgressionStar("Bruce");
            star.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 866 B");
            star.BasicProperties.Add(Constants.PropertyNames.StellarClass, "M");
            catalogue = new GroupProperties("Catalogue");
            catalogue.Properties.Add("HabHyg", "16");
            catalogue.Properties.Add("Hip", "-1");
            star.AllGroupProperties.Add("Catalogue", catalogue);
            system.Add(star);
            star = new ProgressionStar("Calvin");
            star.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 866 C");
            star.BasicProperties.Add(Constants.PropertyNames.StellarClass, "M");
            catalogue = new GroupProperties("Catalogue");
            catalogue.Properties.Add("HabHyg", "16");
            catalogue.Properties.Add("Hip", "-1");
            star.AllGroupProperties.Add("Catalogue", catalogue);
            system.Add(star);
            solCluster.Add(system);
            #endregion

            #region Wolf
            system = new StarSystem("Wolf");
            system.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 406");
            Map.Add(system);
            star = new ProgressionStar("Wolf");
            star.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 406");
            star.BasicProperties.Add(Constants.PropertyNames.StellarClass, "M6");
            catalogue = new GroupProperties("Catalogue");
            catalogue.Properties.Add("HabHyg", "5");
            catalogue.Properties.Add("Hip", "-1");
            star.AllGroupProperties.Add("Catalogue", catalogue);
            system.Add(star);
            solCluster.Add(system);
            #endregion

            #region Ceti
            system = new StarSystem("Ceti");
            system.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 65");
            Map.Add(system);
            star = new ProgressionStar("BL");
            star.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 65 A");
            star.BasicProperties.Add(Constants.PropertyNames.StellarClass, "dM5.5V");
            catalogue = new GroupProperties("Catalogue");
            catalogue.Properties.Add("HabHyg", "7");
            catalogue.Properties.Add("Hip", "-1");
            star.AllGroupProperties.Add("Catalogue", catalogue);
            system.Add(star);
            star = new ProgressionStar("UV");
            star.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 65 B");
            star.BasicProperties.Add(Constants.PropertyNames.StellarClass, "dM5.5V");
            catalogue = new GroupProperties("Catalogue");
            catalogue.Properties.Add("HabHyg", "7");
            catalogue.Properties.Add("Hip", "-1");
            star.AllGroupProperties.Add("Catalogue", catalogue);
            system.Add(star);
            solCluster.Add(system);
            #endregion

            #region Bridges
            // Add Bridges
            ERBridge bridge = ERBridgeHelper.CreateStarSystemBridge(Map, solCluster, "Sol", "Bernard");
            solCluster.Add(bridge);
            bridge = ERBridgeHelper.CreateStarSystemBridge(Map, solCluster, "Sol", "Epsilon Eridani");
            solCluster.Add(bridge);
            bridge = ERBridgeHelper.CreateStarSystemBridge(Map, solCluster, "Sol", "Procyon");
            solCluster.Add(bridge);
            bridge = ERBridgeHelper.CreateStarSystemBridge(Map, solCluster, "Bernard", "Sirius");
            solCluster.Add(bridge);
            bridge = ERBridgeHelper.CreateStarSystemBridge(Map, solCluster, "Bernard", "Ross");
            solCluster.Add(bridge);
            bridge = ERBridgeHelper.CreateStarSystemBridge(Map, solCluster, "Procyon", "Ceti");
            solCluster.Add(bridge);
            bridge = ERBridgeHelper.CreateStarSystemBridge(Map, solCluster, "Procyon", "Lalande");
            solCluster.Add(bridge);
            bridge = ERBridgeHelper.CreateStarSystemBridge(Map, solCluster, "Lalande", "Epsilon Eridani");
            solCluster.Add(bridge);
            bridge = ERBridgeHelper.CreateStarSystemBridge(Map, solCluster, "Ross", "Aquarii");
            solCluster.Add(bridge);
            bridge = ERBridgeHelper.CreateStarSystemBridge(Map, solCluster, "Aquarii", "Wolf");
            solCluster.Add(bridge);
            bridge = ERBridgeHelper.CreateStarSystemBridge(Map, solCluster, "Sirius", "Luyten");
            solCluster.Add(bridge);
            #endregion

            return solCluster;
        }

        public Cluster CreateCentauriCluster()
        {
            Cluster cluster = new Cluster("Centauri Cluster");
            Map.Add(cluster);

            StarSystem system = null;
            ProgressionStar star = null;
            GroupProperties catalogue = null;

            #region Centauri
            system = new StarSystem("Centauri");
            system.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 559");
            Map.Add(system);
            star = new ProgressionStar("Alpha");
            star.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 559 A");
            star.BasicProperties.Add(Constants.PropertyNames.StellarClass, "G2V");
            catalogue = new GroupProperties("Catalogue");
            catalogue.Properties.Add("HabHyg", "3");
            catalogue.Properties.Add("Hip", "71863");
            star.AllGroupProperties.Add("Catalogue", catalogue);
            system.Add(star);
            star = new ProgressionStar("Beta");
            star.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 559 B");
            star.BasicProperties.Add(Constants.PropertyNames.StellarClass, "K1V");
            catalogue = new GroupProperties("Catalogue");
            catalogue.Properties.Add("HabHyg", "2");
            catalogue.Properties.Add("Hip", "71861");
            star.AllGroupProperties.Add("Catalogue", catalogue);
            system.Add(star);
            star = new ProgressionStar("Proxima");
            star.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 551");
            star.BasicProperties.Add(Constants.PropertyNames.StellarClass, "M5Ve");
            catalogue = new GroupProperties("Catalogue");
            catalogue.Properties.Add("HabHyg", "1");
            catalogue.Properties.Add("Hip", "70890");
            star.AllGroupProperties.Add("Catalogue", catalogue);
            system.Add(star);
            cluster.Add(system);
            #endregion

            #region Tau Ceti
            system = new StarSystem("Tau Ceti");
            system.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 71");
            Map.Add(system);
            star = new ProgressionStar("Tau Ceti");
            star.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 71");
            star.BasicProperties.Add(Constants.PropertyNames.StellarClass, "G8V");
            catalogue = new GroupProperties("Catalogue");
            catalogue.Properties.Add("HabHyg", "25");
            catalogue.Properties.Add("Hip", "8102");
            star.AllGroupProperties.Add("Catalogue", catalogue);
            system.Add(star);
            cluster.Add(system);
            #endregion

            #region Groombridge
            system = new StarSystem("Groombridge");
            system.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 380");
            Map.Add(system);
            star = new ProgressionStar("Groombridge");
            star.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 380");
            star.BasicProperties.Add(Constants.PropertyNames.StellarClass, "K8V");
            catalogue = new GroupProperties("Catalogue");
            catalogue.Properties.Add("HabHyg", "59");
            catalogue.Properties.Add("Hip", "49908");
            star.AllGroupProperties.Add("Catalogue", catalogue);
            system.Add(star);
            cluster.Add(system);
            #endregion

            #region Kruger
            system = new StarSystem("Kruger");
            system.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 860");
            Map.Add(system);
            star = new ProgressionStar("Kruger");
            star.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 860 A");
            star.BasicProperties.Add(Constants.PropertyNames.StellarClass, "M2V");
            catalogue = new GroupProperties("Catalogue");
            catalogue.Properties.Add("HabHyg", "32");
            catalogue.Properties.Add("Hip", "110893");
            star.AllGroupProperties.Add("Catalogue", catalogue);
            system.Add(star);
            star = new ProgressionStar("Potter");
            star.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 860 B");
            star.BasicProperties.Add(Constants.PropertyNames.StellarClass, "M6V");
            catalogue = new GroupProperties("Catalogue");
            catalogue.Properties.Add("HabHyg", "32");
            catalogue.Properties.Add("Hip", "110893");
            star.AllGroupProperties.Add("Catalogue", catalogue);
            cluster.Add(system);
            #endregion

            #region Lacaille
            system = new StarSystem("Lacaille");
            system.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 825");
            Map.Add(system);
            star = new ProgressionStar("Lacaille");
            star.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 825");
            star.BasicProperties.Add(Constants.PropertyNames.StellarClass, "M1/M2V");
            catalogue = new GroupProperties("Catalogue");
            catalogue.Properties.Add("HabHyg", "30");
            catalogue.Properties.Add("Hip", "105090");
            star.AllGroupProperties.Add("Catalogue", catalogue);
            system.Add(star);
            cluster.Add(system);
            #endregion

            #region Gleine
            system = new StarSystem("Gleine");
            system.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 725");
            Map.Add(system);
            star = new ProgressionStar("Gleine");
            star.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 725");
            star.BasicProperties.Add(Constants.PropertyNames.StellarClass, "M3");
            catalogue = new GroupProperties("Catalogue");
            catalogue.Properties.Add("HabHyg", "26");
            catalogue.Properties.Add("Hip", "91768");
            star.AllGroupProperties.Add("Catalogue", catalogue);
            system.Add(star);
            star = new ProgressionStar("Struve");
            star.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 725 B");
            star.BasicProperties.Add(Constants.PropertyNames.StellarClass, "M3.5");
            catalogue = new GroupProperties("Catalogue");
            catalogue.Properties.Add("HabHyg", "19");
            catalogue.Properties.Add("Hip", "91772");
            star.AllGroupProperties.Add("Catalogue", catalogue);
            system.Add(star); cluster.Add(system);
            #endregion

            #region Kapteyn
            system = new StarSystem("Kapteyn");
            system.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 191");
            Map.Add(system);
            star = new ProgressionStar("Kapteyn's Star");
            star.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 191");
            star.BasicProperties.Add(Constants.PropertyNames.StellarClass, "M0V");
            catalogue = new GroupProperties("Catalogue");
            catalogue.Properties.Add("HabHyg", "29");
            catalogue.Properties.Add("Hip", "24186");
            star.AllGroupProperties.Add("Catalogue", catalogue);
            system.Add(star);
            cluster.Add(system);
            #endregion

            #region Vista
            system = new StarSystem("Vista");
            system.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 473");
            Map.Add(system);
            star = new ProgressionStar("Vista");
            star.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 473 A");
            star.BasicProperties.Add(Constants.PropertyNames.StellarClass, "M5.5eJ");
            catalogue = new GroupProperties("Catalogue");
            catalogue.Properties.Add("HabHyg", "36");
            catalogue.Properties.Add("Hip", "-1");
            star.AllGroupProperties.Add("Catalogue", catalogue);
            system.Add(star);
            star = new ProgressionStar("Point");
            star.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 473 B");
            star.BasicProperties.Add(Constants.PropertyNames.StellarClass, "M7");
            catalogue = new GroupProperties("Catalogue");
            catalogue.Properties.Add("HabHyg", "37");
            catalogue.Properties.Add("Hip", "-1");
            star.AllGroupProperties.Add("Catalogue", catalogue);
            system.Add(star);
            cluster.Add(system);
            #endregion

            #region Bihar
            system = new StarSystem("Bihar");
            system.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 1061");
            Map.Add(system);
            star = new ProgressionStar("Bihar");
            star.BasicProperties.Add(Constants.PropertyNames.Designation, "GJ 1061");
            star.BasicProperties.Add(Constants.PropertyNames.StellarClass, "M4.5");
            catalogue = new GroupProperties("Catalogue");
            catalogue.Properties.Add("HabHyg", "35");
            catalogue.Properties.Add("Hip", "-1");
            star.AllGroupProperties.Add("Catalogue", catalogue);
            system.Add(star);
            cluster.Add(system);
            #endregion

            #region Inne
            system = new StarSystem("Inne");
            system.BasicProperties.Add(Constants.PropertyNames.Designation, "NN 3618");
            Map.Add(system);
            star = new ProgressionStar("Inne's Star");
            star.BasicProperties.Add(Constants.PropertyNames.Designation, "NN 3618");
            star.BasicProperties.Add(Constants.PropertyNames.StellarClass, "M5V");
            catalogue = new GroupProperties("Catalogue");
            catalogue.Properties.Add("HabHyg", "45");
            catalogue.Properties.Add("Hip", "-1");
            star.AllGroupProperties.Add("Catalogue", catalogue);
            system.Add(star);
            cluster.Add(system);
            #endregion

            #region Bridges
            ERBridge bridge = ERBridgeHelper.CreateStarSystemBridge(Map, cluster, "Kruger", "Centauri");
            cluster.Add(bridge);
            bridge = ERBridgeHelper.CreateStarSystemBridge(Map, cluster, "Kruger", "Lacaille");
            cluster.Add(bridge);
            bridge = ERBridgeHelper.CreateStarSystemBridge(Map, cluster, "Centauri", "Lacaille");
            cluster.Add(bridge);
            bridge = ERBridgeHelper.CreateStarSystemBridge(Map, cluster, "Lacaille", "Tau Ceti");
            cluster.Add(bridge);
            bridge = ERBridgeHelper.CreateStarSystemBridge(Map, cluster, "Lacaille", "Gleine");
            cluster.Add(bridge);
            bridge = ERBridgeHelper.CreateStarSystemBridge(Map, cluster, "Gleine", "Kapteyn");
            cluster.Add(bridge);
            bridge = ERBridgeHelper.CreateStarSystemBridge(Map, cluster, "Kruger", "Vista");
            cluster.Add(bridge);
            bridge = ERBridgeHelper.CreateStarSystemBridge(Map, cluster, "Vista", "Bihar");
            cluster.Add(bridge);
            bridge = ERBridgeHelper.CreateStarSystemBridge(Map, cluster, "Gleine", "Inne");
            cluster.Add(bridge);
            bridge = ERBridgeHelper.CreateStarSystemBridge(Map, cluster, "Groombridge", "Bihar");
            cluster.Add(bridge);
            #endregion

            return cluster;
        }

        public Cluster CreateDwarfCluster()
        {
            Cluster cluster = new Cluster("Dwarf Cluster");
            Map.Add(cluster);

            StarSystem system = null;
            ProgressionStar star = null;
            GroupProperties catalogue = null;

            #region Wolf's Den
            system = new StarSystem("Wolf's Den");
            system.BasicProperties.Add(Constants.PropertyNames.Designation, "NN 3622");
            Map.Add(system);
            star = new ProgressionStar("Wolf's Den");
            star.BasicProperties.Add(Constants.PropertyNames.Designation, "NN 3622");
            star.BasicProperties.Add(Constants.PropertyNames.StellarClass, "M6.5");
            catalogue = new GroupProperties("Catalogue");
            catalogue.Properties.Add("HabHyg", "46");
            catalogue.Properties.Add("Hip", "0");
            star.AllGroupProperties.Add("Catalogue", catalogue);
            system.Add(star);
            cluster.Add(system);
            #endregion

            #region Andromeda
            system = new StarSystem("Andromeda");
            system.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 15");
            Map.Add(system);
            star = new ProgressionStar("Andomeda A");
            star.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 15 A");
            star.BasicProperties.Add(Constants.PropertyNames.StellarClass, "M1V");
            catalogue = new GroupProperties("Catalogue");
            catalogue.Properties.Add("HabHyg", "24");
            catalogue.Properties.Add("Hip", "1475");
            star.AllGroupProperties.Add("Catalogue", catalogue);
            system.Add(star);
            star = new ProgressionStar("Andromeda B");
            star.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 15 B");
            star.BasicProperties.Add(Constants.PropertyNames.StellarClass, "M6Ve");
            catalogue = new GroupProperties("Catalogue");
            catalogue.Properties.Add("HabHyg", "22");
            catalogue.Properties.Add("Hip", "-1");
            star.AllGroupProperties.Add("Catalogue", catalogue);
            system.Add(star);
            cluster.Add(system);
            #endregion

            #region Sutter
            system = new StarSystem("Sutter");
            system.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 687");
            Map.Add(system);
            star = new ProgressionStar("Sutter");
            star.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 687");
            star.BasicProperties.Add(Constants.PropertyNames.StellarClass, "M3.5V");
            catalogue = new GroupProperties("Catalogue");
            catalogue.Properties.Add("HabHyg", "49");
            catalogue.Properties.Add("Hip", "86162");
            star.AllGroupProperties.Add("Catalogue", catalogue);
            system.Add(star);
            cluster.Add(system);
            #endregion

            #region Siera
            system = new StarSystem("Siera");
            system.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 234");
            Map.Add(system);
            star = new ProgressionStar("Bright");
            star.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 234 A");
            star.BasicProperties.Add(Constants.PropertyNames.StellarClass, "M4.5Ve");
            catalogue = new GroupProperties("Catalogue");
            catalogue.Properties.Add("HabHyg", "34");
            catalogue.Properties.Add("Hip", "30920");
            star.AllGroupProperties.Add("Catalogue", catalogue);
            system.Add(star);
            star = new ProgressionStar("Dim");
            star.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 234 B");
            star.BasicProperties.Add(Constants.PropertyNames.StellarClass, "M");
            catalogue = new GroupProperties("Catalogue");
            catalogue.Properties.Add("HabHyg", "33");
            catalogue.Properties.Add("Hip", "-1");
            star.AllGroupProperties.Add("Catalogue", catalogue);
            cluster.Add(system);
            #endregion

            #region YZ Ceti
            system = new StarSystem("YZ Ceti");
            system.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 54.1");
            Map.Add(system);
            star = new ProgressionStar("YZ Ceti");
            star.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 54.1");
            star.BasicProperties.Add(Constants.PropertyNames.StellarClass, "M4.5");
            catalogue = new GroupProperties("Catalogue");
            catalogue.Properties.Add("HabHyg", "-1");
            catalogue.Properties.Add("Hip", "-1");
            star.AllGroupProperties.Add("Catalogue", catalogue);
            system.Add(star);
            cluster.Add(system);
            #endregion

            #region Kercur
            system = new StarSystem("Kercur");
            system.BasicProperties.Add(Constants.PropertyNames.Designation, "GJ 3622");
            Map.Add(system);
            star = new ProgressionStar("Kercur");
            star.BasicProperties.Add(Constants.PropertyNames.Designation, "GJ 3622");
            star.BasicProperties.Add(Constants.PropertyNames.StellarClass, "M6.5");
            catalogue = new GroupProperties("Catalogue");
            catalogue.Properties.Add("HabHyg", "-1");
            catalogue.Properties.Add("Hip", "-1");
            star.AllGroupProperties.Add("Catalogue", catalogue);
            system.Add(star);
            cluster.Add(system);
            #endregion

            #region Nuone
            system = new StarSystem("Nuone");
            system.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 1");
            Map.Add(system);
            star = new ProgressionStar("Nuone");
            star.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 1");
            star.BasicProperties.Add(Constants.PropertyNames.StellarClass, "M2V");
            catalogue = new GroupProperties("Catalogue");
            catalogue.Properties.Add("HabHyg", "40");
            catalogue.Properties.Add("Hip", "439");
            star.AllGroupProperties.Add("Catalogue", catalogue);
            system.Add(star);
            cluster.Add(system);
            #endregion

            #region Leonis
            system = new StarSystem("Leonis");
            system.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 388");
            Map.Add(system);
            star = new ProgressionStar("Leonis");
            star.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 388");
            star.BasicProperties.Add(Constants.PropertyNames.StellarClass, "M4.5Ve");
            catalogue = new GroupProperties("Catalogue");
            catalogue.Properties.Add("HabHyg", "58");
            catalogue.Properties.Add("Hip", "-1");
            star.AllGroupProperties.Add("Catalogue", catalogue);
            system.Add(star);
            cluster.Add(system);
            #endregion

            #region Ophir
            system = new StarSystem("Ophir");
            system.BasicProperties.Add(Constants.PropertyNames.Designation, "SCR 1845");
            Map.Add(system);
            star = new ProgressionStar("Red");
            star.BasicProperties.Add(Constants.PropertyNames.Designation, "SCR 1845 A");
            star.BasicProperties.Add(Constants.PropertyNames.StellarClass, "M8.5V");
            catalogue = new GroupProperties("Catalogue");
            catalogue.Properties.Add("HabHyg", "-1");
            catalogue.Properties.Add("Hip", "-1");
            star.AllGroupProperties.Add("Catalogue", catalogue);
            system.Add(star);
            star = new ProgressionStar("Brown");
            star.BasicProperties.Add(Constants.PropertyNames.Designation, "SCR 1845 B");
            star.BasicProperties.Add(Constants.PropertyNames.StellarClass, "T6V");
            catalogue = new GroupProperties("Catalogue");
            catalogue.Properties.Add("HabHyg", "-1");
            catalogue.Properties.Add("Hip", "-1");
            star.AllGroupProperties.Add("Catalogue", catalogue);
            cluster.Add(system);
            #endregion

            #region Cancri
            system = new StarSystem("Cancri");
            system.BasicProperties.Add(Constants.PropertyNames.Designation, "GJ 1111");
            Map.Add(system);
            star = new ProgressionStar("Cancri");
            star.BasicProperties.Add(Constants.PropertyNames.Designation, "GJ 1111");
            star.BasicProperties.Add(Constants.PropertyNames.StellarClass, "M6.5");
            catalogue = new GroupProperties("Catalogue");
            catalogue.Properties.Add("HabHyg", "23");
            catalogue.Properties.Add("Hip", "-1");
            star.AllGroupProperties.Add("Catalogue", catalogue);
            system.Add(star);
            cluster.Add(system);
            #endregion

            #region Eureka
            system = new StarSystem("Eureka");
            system.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 440");
            Map.Add(system);
            star = new ProgressionStar("Eureka");
            star.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 440");
            star.BasicProperties.Add(Constants.PropertyNames.StellarClass, "DC:");
            catalogue = new GroupProperties("Catalogue");
            catalogue.Properties.Add("HabHyg", "50");
            catalogue.Properties.Add("Hip", "57367");
            star.AllGroupProperties.Add("Catalogue", catalogue);
            system.Add(star);
            cluster.Add(system);
            #endregion

            #region TeeGarden
            system = new StarSystem("TeeGarden");
            system.BasicProperties.Add(Constants.PropertyNames.Designation, "SO025300.5+165258");
            Map.Add(system);
            star = new ProgressionStar("TeeGarden");
            star.BasicProperties.Add(Constants.PropertyNames.Designation, "SO025300.5+165258");
            star.BasicProperties.Add(Constants.PropertyNames.StellarClass, "M4.5V");
            catalogue = new GroupProperties("Catalogue");
            catalogue.Properties.Add("HabHyg", "-1");
            catalogue.Properties.Add("Hip", "-1");
            star.AllGroupProperties.Add("Catalogue", catalogue);
            system.Add(star);
            cluster.Add(system);
            #endregion

            #region Van Maanen
            system = new StarSystem("Van Maanen");
            system.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 35");
            Map.Add(system);
            star = new ProgressionStar("Van Maanen");
            star.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 35");
            star.BasicProperties.Add(Constants.PropertyNames.StellarClass, "DG");
            catalogue = new GroupProperties("Catalogue");
            catalogue.Properties.Add("HabHyg", "41");
            catalogue.Properties.Add("Hip", "3829");
            star.AllGroupProperties.Add("Catalogue", catalogue);
            system.Add(star);
            cluster.Add(system);
            #endregion

            #region Knob
            system = new StarSystem("Knob");
            system.BasicProperties.Add(Constants.PropertyNames.Designation, "UGPS 0722-05");
            Map.Add(system);
            star = new ProgressionStar("Ross");
            star.BasicProperties.Add(Constants.PropertyNames.Designation, "UGPS 0722-05");
            star.BasicProperties.Add(Constants.PropertyNames.StellarClass, "T10");
            catalogue = new GroupProperties("Catalogue");
            catalogue.Properties.Add("HabHyg", "-1");
            catalogue.Properties.Add("Hip", "-1");
            star.AllGroupProperties.Add("Catalogue", catalogue);
            system.Add(star);
            cluster.Add(system);
            #endregion

            #region Bridges
            ERBridge bridge = ERBridgeHelper.CreateStarSystemBridge(Map, cluster, "Wolf's Den", "Siera");
            cluster.Add(bridge);
            bridge = ERBridgeHelper.CreateStarSystemBridge(Map, cluster, "Siera", "Leonis");
            cluster.Add(bridge);
            bridge = ERBridgeHelper.CreateStarSystemBridge(Map, cluster, "Leonis", "Ophir");
            cluster.Add(bridge);
            bridge = ERBridgeHelper.CreateStarSystemBridge(Map, cluster, "Leonis", "TeeGarden");
            cluster.Add(bridge);
            bridge = ERBridgeHelper.CreateStarSystemBridge(Map, cluster, "TeeGarden", "Van Maanen");
            cluster.Add(bridge);
            bridge = ERBridgeHelper.CreateStarSystemBridge(Map, cluster, "TeeGarden", "Cancri");
            cluster.Add(bridge);
            bridge = ERBridgeHelper.CreateStarSystemBridge(Map, cluster, "Van Maanen", "YZ Ceti");
            cluster.Add(bridge);
            bridge = ERBridgeHelper.CreateStarSystemBridge(Map, cluster, "Van Maanen", "Eureka");
            cluster.Add(bridge);
            bridge = ERBridgeHelper.CreateStarSystemBridge(Map, cluster, "Cancri", "Nuone");
            cluster.Add(bridge);
            bridge = ERBridgeHelper.CreateStarSystemBridge(Map, cluster, "YZ Ceti", "Andromeda");
            cluster.Add(bridge);
            bridge = ERBridgeHelper.CreateStarSystemBridge(Map, cluster, "Andromeda", "Kercur");
            cluster.Add(bridge);
            bridge = ERBridgeHelper.CreateStarSystemBridge(Map, cluster, "Kercur", "Nuone");
            cluster.Add(bridge);
            bridge = ERBridgeHelper.CreateStarSystemBridge(Map, cluster, "Nuone", "Sutter");
            cluster.Add(bridge);
            bridge = ERBridgeHelper.CreateStarSystemBridge(Map, cluster, "Nuone", "Eureka");
            cluster.Add(bridge);
            bridge = ERBridgeHelper.CreateStarSystemBridge(Map, cluster, "Eureka", "Knob");
            cluster.Add(bridge);
            #endregion

            return cluster;
        }

        public Cluster CreateCygniCluster()
        {
            Cluster cluster = new Cluster("Cygni Cluster");
            Map.Add(cluster);

            StarSystem system = null;
            ProgressionStar star = null;
            GroupProperties catalogue = null;

            #region Cygni
            system = new StarSystem("Cygni");
            system.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 820");
            Map.Add(system);
            star = new ProgressionStar("Bradley");
            star.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 820 A");
            star.BasicProperties.Add(Constants.PropertyNames.StellarClass, "K5V");
            catalogue = new GroupProperties("Catalogue");
            catalogue.Properties.Add("HabHyg", "20");
            catalogue.Properties.Add("Hip", "104214");
            star.AllGroupProperties.Add("Catalogue", catalogue);
            system.Add(star);
            star = new ProgressionStar("Paizzi");
            star.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 820 B");
            star.BasicProperties.Add(Constants.PropertyNames.StellarClass, "K7V");
            catalogue = new GroupProperties("Catalogue");
            catalogue.Properties.Add("HabHyg", "21");
            catalogue.Properties.Add("Hip", "104217");
            star.AllGroupProperties.Add("Catalogue", catalogue);
            system.Add(star);
            cluster.Add(system);
            #endregion

            #region Epsilon Indi
            system = new StarSystem("Epsilon Indi");
            system.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 845");
            Map.Add(system);
            star = new ProgressionStar("Indus");
            star.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 845 A");
            star.BasicProperties.Add(Constants.PropertyNames.StellarClass, "K5V");
            catalogue = new GroupProperties("Catalogue");
            catalogue.Properties.Add("HabHyg", "27");
            catalogue.Properties.Add("Hip", "108870");
            star.AllGroupProperties.Add("Catalogue", catalogue);
            system.Add(star);
            star = new ProgressionStar("Ghaggar");
            star.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 845 B");
            star.BasicProperties.Add(Constants.PropertyNames.StellarClass, "T1V");
            catalogue = new GroupProperties("Catalogue");
            catalogue.Properties.Add("HabHyg", "27");
            catalogue.Properties.Add("Hip", "108870");
            star.AllGroupProperties.Add("Catalogue", catalogue);
            system.Add(star);
            star = new ProgressionStar("Hakra");
            star.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 845 C");
            star.BasicProperties.Add(Constants.PropertyNames.StellarClass, "T6V");
            catalogue = new GroupProperties("Catalogue");
            catalogue.Properties.Add("HabHyg", "27");
            catalogue.Properties.Add("Hip", "108870");
            star.AllGroupProperties.Add("Catalogue", catalogue);
            system.Add(star);
            cluster.Add(system);
            #endregion

            #region Arietis
            system = new StarSystem("Arietis");
            system.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 83");
            Map.Add(system);
            star = new ProgressionStar("Arietis");
            star.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 83.1");
            star.BasicProperties.Add(Constants.PropertyNames.StellarClass, "M8e");
            catalogue = new GroupProperties("Catalogue");
            catalogue.Properties.Add("HabHyg", "43");
            catalogue.Properties.Add("Hip", "-1");
            star.AllGroupProperties.Add("Catalogue", catalogue);
            system.Add(star);
            cluster.Add(system);
            #endregion

            #region Flare
            system = new StarSystem("Flare");
            system.BasicProperties.Add(Constants.PropertyNames.Designation, "DEN 1048-3956");
            Map.Add(system);
            star = new ProgressionStar("Flare");
            star.BasicProperties.Add(Constants.PropertyNames.Designation, "DEN 1048-3956");
            star.BasicProperties.Add(Constants.PropertyNames.StellarClass, "M8.5V");
            catalogue = new GroupProperties("Catalogue");
            catalogue.Properties.Add("HabHyg", "-1");
            catalogue.Properties.Add("Hip", "-1");
            star.AllGroupProperties.Add("Catalogue", catalogue);
            system.Add(star);
            cluster.Add(system);
            #endregion

            #region Demic
            system = new StarSystem("Demic");
            system.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 412");
            Map.Add(system);
            star = new ProgressionStar("Demic");
            star.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 412 A");
            star.BasicProperties.Add(Constants.PropertyNames.StellarClass, "M2V");
            catalogue = new GroupProperties("Catalogue");
            catalogue.Properties.Add("HabHyg", "57");
            catalogue.Properties.Add("Hip", "54211");
            star.AllGroupProperties.Add("Catalogue", catalogue);
            system.Add(star);
            star = new ProgressionStar("Wax");
            star.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 412 B");
            star.BasicProperties.Add(Constants.PropertyNames.StellarClass, "M63");
            catalogue = new GroupProperties("Catalogue");
            catalogue.Properties.Add("HabHyg", "56");
            catalogue.Properties.Add("Hip", "-1");
            star.AllGroupProperties.Add("Catalogue", catalogue);
            cluster.Add(system);
            #endregion

            #region Lapis
            system = new StarSystem("Lapis");
            system.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 628");
            Map.Add(system);
            star = new ProgressionStar("Lapis");
            star.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 628");
            star.BasicProperties.Add(Constants.PropertyNames.StellarClass, "M3.5");
            catalogue = new GroupProperties("Catalogue");
            catalogue.Properties.Add("HabHyg", "39");
            catalogue.Properties.Add("Hip", "80824");
            star.AllGroupProperties.Add("Catalogue", catalogue);
            system.Add(star);
            cluster.Add(system);
            #endregion

            #region Banawali
            system = new StarSystem("Banawali");
            system.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 876");
            Map.Add(system);
            star = new ProgressionStar("Banawali");
            star.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 876");
            star.BasicProperties.Add(Constants.PropertyNames.StellarClass, "M5");
            catalogue = new GroupProperties("Catalogue");
            catalogue.Properties.Add("HabHyg", "55");
            catalogue.Properties.Add("Hip", "113020");
            star.AllGroupProperties.Add("Catalogue", catalogue);
            system.Add(star);
            cluster.Add(system);
            #endregion

            #region Rudra
            system = new StarSystem("Rudra");
            system.BasicProperties.Add(Constants.PropertyNames.Designation, "GJ 1245");
            Map.Add(system);
            star = new ProgressionStar("Rudra");
            star.BasicProperties.Add(Constants.PropertyNames.Designation, "GJ 1245 A");
            star.BasicProperties.Add(Constants.PropertyNames.StellarClass, "M5.5V");
            catalogue = new GroupProperties("Catalogue");
            catalogue.Properties.Add("HabHyg", "53");
            catalogue.Properties.Add("Hip", "-1");
            star.AllGroupProperties.Add("Catalogue", catalogue);
            system.Add(star);
            star = new ProgressionStar("Pashupati");
            star.BasicProperties.Add(Constants.PropertyNames.Designation, "GJ 1245 B");
            star.BasicProperties.Add(Constants.PropertyNames.StellarClass, "M");
            catalogue = new GroupProperties("Catalogue");
            catalogue.Properties.Add("HabHyg", "53");
            catalogue.Properties.Add("Hip", "-1");
            star.AllGroupProperties.Add("Catalogue", catalogue);
            system.Add(star);
            star = new ProgressionStar("Agni");
            star.BasicProperties.Add(Constants.PropertyNames.Designation, "GJ 1245 C");
            star.BasicProperties.Add(Constants.PropertyNames.StellarClass, "M6V");
            catalogue = new GroupProperties("Catalogue");
            catalogue.Properties.Add("HabHyg", "-1");
            catalogue.Properties.Add("Hip", "-1");
            star.AllGroupProperties.Add("Catalogue", catalogue);
            cluster.Add(system);
            #endregion

            #region Bridges
            ERBridge bridge = ERBridgeHelper.CreateStarSystemBridge(Map, cluster, "Cygni", "Epsilon Indi");
            cluster.Add(bridge);
            bridge = ERBridgeHelper.CreateStarSystemBridge(Map, cluster, "Cygni", "Arietis");
            cluster.Add(bridge);
            bridge = ERBridgeHelper.CreateStarSystemBridge(Map, cluster, "Epsilon Indi", "Flare");
            cluster.Add(bridge);
            bridge = ERBridgeHelper.CreateStarSystemBridge(Map, cluster, "Flare", "Lapis");
            cluster.Add(bridge);
            bridge = ERBridgeHelper.CreateStarSystemBridge(Map, cluster, "Arietis", "Demic");
            cluster.Add(bridge);
            bridge = ERBridgeHelper.CreateStarSystemBridge(Map, cluster, "Arietis", "Lapis");
            cluster.Add(bridge);
            bridge = ERBridgeHelper.CreateStarSystemBridge(Map, cluster, "Flare", "Demic");
            cluster.Add(bridge);
            bridge = ERBridgeHelper.CreateStarSystemBridge(Map, cluster, "Demic", "Banawali");
            cluster.Add(bridge);
            bridge = ERBridgeHelper.CreateStarSystemBridge(Map, cluster, "Demic", "Rudra");
            cluster.Add(bridge);
            #endregion

            return cluster;
        }

        public Cluster CreateAltairCluster()
        {
            // My callout to Forbidden Planet
            Cluster cluster = new Cluster("Altair");
            Map.Add(cluster);

            StarSystem system = null;
            ProgressionStar star = null;
            GroupProperties catalogue = null;

            #region Altair
            system = new StarSystem("Altair");
            system.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 768");
            Map.Add(system);
            star = new ProgressionStar("Altaira");
            star.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 768");
            star.BasicProperties.Add(Constants.PropertyNames.StellarClass, "A7Vn");
            catalogue = new GroupProperties("Catalogue");
            catalogue.Properties.Add("HabHyg", "70");
            catalogue.Properties.Add("Hip", "97649");
            star.AllGroupProperties.Add("Catalogue", catalogue);
            system.Add(star);
            cluster.Add(system);
            #endregion

            #region Krell
            system = new StarSystem("Krell");
            system.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 702");
            Map.Add(system);
            star = new ProgressionStar("Krell");
            star.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 702 A");
            star.BasicProperties.Add(Constants.PropertyNames.StellarClass, "K1V");
            catalogue = new GroupProperties("Catalogue");
            catalogue.Properties.Add("HabHyg", "69");
            catalogue.Properties.Add("Hip", "88601");
            star.AllGroupProperties.Add("Catalogue", catalogue);
            system.Add(star);
            star = new ProgressionStar("Robby");
            star.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 702 B");
            star.BasicProperties.Add(Constants.PropertyNames.StellarClass, "K5V");
            catalogue = new GroupProperties("Catalogue");
            catalogue.Properties.Add("HabHyg", "68");
            catalogue.Properties.Add("Hip", "-1");
            star.AllGroupProperties.Add("Catalogue", catalogue);
            system.Add(star);
            cluster.Add(system);
            #endregion

            #region Mobius
            system = new StarSystem("Mobius");
            system.BasicProperties.Add(Constants.PropertyNames.Designation, "GJ 1116");
            Map.Add(system);
            star = new ProgressionStar("Mobius");
            star.BasicProperties.Add(Constants.PropertyNames.Designation, "GJ 1116 A");
            star.BasicProperties.Add(Constants.PropertyNames.StellarClass, "M");
            catalogue = new GroupProperties("Catalogue");
            catalogue.Properties.Add("HabHyg", "71");
            catalogue.Properties.Add("Hip", "-1");
            star.AllGroupProperties.Add("Catalogue", catalogue);
            system.Add(star);
            star = new ProgressionStar("Pidgeon");
            star.BasicProperties.Add(Constants.PropertyNames.Designation, "GJ 1116 B");
            star.BasicProperties.Add(Constants.PropertyNames.StellarClass, "M");
            catalogue = new GroupProperties("Catalogue");
            catalogue.Properties.Add("HabHyg", "72");
            catalogue.Properties.Add("Hip", "-1");
            star.AllGroupProperties.Add("Catalogue", catalogue);
            system.Add(star);
            cluster.Add(system);
            #endregion

            #region Bellerophon
            system = new StarSystem("Bellerophon");
            system.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 205");
            Map.Add(system);
            star = new ProgressionStar("Bellerophon");
            star.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 205");
            star.BasicProperties.Add(Constants.PropertyNames.StellarClass, "M1.5");
            catalogue = new GroupProperties("Catalogue");
            catalogue.Properties.Add("HabHyg", "85");
            catalogue.Properties.Add("Hip", "25878");
            star.AllGroupProperties.Add("Catalogue", catalogue);
            system.Add(star);
            cluster.Add(system);
            #endregion

            #region Id
            system = new StarSystem("Id");
            system.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 445");
            Map.Add(system);
            star = new ProgressionStar("Id");
            star.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 445");
            star.BasicProperties.Add(Constants.PropertyNames.StellarClass, "M");
            catalogue = new GroupProperties("Catalogue");
            catalogue.Properties.Add("HabHyg", "75");
            catalogue.Properties.Add("Hip", "57544");
            star.AllGroupProperties.Add("Catalogue", catalogue);
            system.Add(star);
            cluster.Add(system);
            #endregion

            #region Adams
            system = new StarSystem("Adams");
            system.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 251");
            Map.Add(system);
            star = new ProgressionStar("Adams");
            star.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 251");
            star.BasicProperties.Add(Constants.PropertyNames.StellarClass, "M3");
            catalogue = new GroupProperties("Catalogue");
            catalogue.Properties.Add("HabHyg", "87");
            catalogue.Properties.Add("Hip", "-1");
            star.AllGroupProperties.Add("Catalogue", catalogue);
            system.Add(star);
            cluster.Add(system);
            #endregion

            #region Lacerta
            system = new StarSystem("Lacerta");
            system.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 873");
            Map.Add(system);
            star = new ProgressionStar("Lacerta");
            star.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 873");
            star.BasicProperties.Add(Constants.PropertyNames.StellarClass, "M4.5");
            catalogue = new GroupProperties("Catalogue");
            catalogue.Properties.Add("HabHyg", "67");
            catalogue.Properties.Add("Hip", "112460");
            star.AllGroupProperties.Add("Catalogue", catalogue);
            system.Add(star);
            cluster.Add(system);
            #endregion

            #region Gateway
            system = new StarSystem("Gateway");
            system.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 693");
            Map.Add(system);
            star = new ProgressionStar("Lacerta");
            star.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 693");
            star.BasicProperties.Add(Constants.PropertyNames.StellarClass, "M2");
            catalogue = new GroupProperties("Catalogue");
            catalogue.Properties.Add("HabHyg", "90");
            catalogue.Properties.Add("Hip", "86990");
            star.AllGroupProperties.Add("Catalogue", catalogue);
            system.Add(star);
            cluster.Add(system);
            #endregion

            #region Omicron
            system = new StarSystem("Omicron");
            system.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 166");
            Map.Add(system);
            star = new ProgressionStar("Keid");
            star.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 166 A");
            star.BasicProperties.Add(Constants.PropertyNames.StellarClass, "K1");
            catalogue = new GroupProperties("Catalogue");
            catalogue.Properties.Add("HabHyg", "65");
            catalogue.Properties.Add("Hip", "19849");
            star.AllGroupProperties.Add("Catalogue", catalogue);
            system.Add(star);
            star = new ProgressionStar("Little White");
            star.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 166 B");
            star.BasicProperties.Add(Constants.PropertyNames.StellarClass, "DA4");
            catalogue = new GroupProperties("Catalogue");
            catalogue.Properties.Add("HabHyg", "63");
            catalogue.Properties.Add("Hip", "-1");
            star.AllGroupProperties.Add("Catalogue", catalogue);
            system.Add(star);
            star = new ProgressionStar("Little Red");
            star.BasicProperties.Add(Constants.PropertyNames.Designation, "Gl 166 C");
            star.BasicProperties.Add(Constants.PropertyNames.StellarClass, "M4.5e");
            catalogue = new GroupProperties("Catalogue");
            catalogue.Properties.Add("HabHyg", "64");
            catalogue.Properties.Add("Hip", "-1");
            star.AllGroupProperties.Add("Catalogue", catalogue);
            system.Add(star);
            cluster.Add(system);
            #endregion

            #region Bridges
            ERBridge bridge = ERBridgeHelper.CreateStarSystemBridge(Map, cluster, "Altair", "Krell");
            cluster.Add(bridge);
            bridge = ERBridgeHelper.CreateStarSystemBridge(Map, cluster, "Altair", "Mobius");
            cluster.Add(bridge);
            bridge = ERBridgeHelper.CreateStarSystemBridge(Map, cluster, "Altair", "Bellerophon");
            cluster.Add(bridge);
            bridge = ERBridgeHelper.CreateStarSystemBridge(Map, cluster, "Altair", "Adams");
            cluster.Add(bridge);
            bridge = ERBridgeHelper.CreateStarSystemBridge(Map, cluster, "Krell", "Id");
            cluster.Add(bridge);
            bridge = ERBridgeHelper.CreateStarSystemBridge(Map, cluster, "Krell", "Omicron");
            cluster.Add(bridge);
            bridge = ERBridgeHelper.CreateStarSystemBridge(Map, cluster, "Mobius", "Bellerophon");
            cluster.Add(bridge);
            bridge = ERBridgeHelper.CreateStarSystemBridge(Map, cluster, "Mobius", "Id");
            cluster.Add(bridge);
            bridge = ERBridgeHelper.CreateStarSystemBridge(Map, cluster, "Mobius", "Adams");
            cluster.Add(bridge);
            bridge = ERBridgeHelper.CreateStarSystemBridge(Map, cluster, "Bellerophon", "Lacerta");
            cluster.Add(bridge);
            bridge = ERBridgeHelper.CreateStarSystemBridge(Map, cluster, "Lacerta", "Gateway");
            cluster.Add(bridge);
            #endregion

            return cluster;
        }

        public Sector CreateLocalSector()
        {
            Sector local = new Sector("Local Sector");
            Map.Add(local);

            local.Add(CreateSolCluster());
            local.Add(CreateCentauriCluster());
            local.Add(CreateDwarfCluster());
            local.Add(CreateCygniCluster());
            local.Add(CreateAltairCluster());

            ERBridge bridge = ERBridgeHelper.CreateClusterBridge(Map, local, "Sirius", "Wolf's Den");
            local.Add(bridge);
            bridge = ERBridgeHelper.CreateClusterBridge(Map, local, "Lalande", "Kruger");
            local.Add(bridge);
            bridge = ERBridgeHelper.CreateClusterBridge(Map, local, "Van Maanen", "Lacaille");
            local.Add(bridge);
            bridge = ERBridgeHelper.CreateClusterBridge(Map, local, "Tau Ceti", "Cygni");
            local.Add(bridge);
            bridge = ERBridgeHelper.CreateClusterBridge(Map, local, "Rudra", "Altair");
            local.Add(bridge);

            return local;
        }
    }
}

using System.Collections.Generic;

using StellarMap.Core.Bodies;
using StellarMap.Core.Types;

namespace StellarMap.Progression.DefaultSettingMaps
{
    public static class PhysicalSolarSystemCreator
    {
        public static Star CreateSolarSystem(IStellarMap map = null)
        {
            Star sol = CreateSol(map);

            sol.Add(CreateMercury(map));
            sol.Add(CreateVenus(map));
            sol.Add(CreateEarth(map));
            sol.Add(CreateMars(map));
            sol.Add(CreateJupiter(map));
            sol.Add(CreateSaturn(map));
            sol.Add(CreateUranus(map));
            sol.Add(CreateNeptune(map));

            sol.Add(CreateCeres(map));
            sol.Add(CreatePluto(map));

            var asteroids = CreateAsteroids(map);
            foreach (Asteroid a in asteroids)
                sol.Add(a);

            var comets = CreateComets();
            foreach (Comet c in comets)
                sol.Add(c);

            return sol;

        }

        public static Star CreateSol(IStellarMap map = null)
        {
            Star sol = new Star("Sol");

            map ??= BaseStellarMap.DefaultMap;
            map.Add(sol);

            sol.BasicProperties.Add(Constants.PropertyNames.Designation, "Sol");
            sol.BasicProperties.Add(Constants.PropertyNames.StellarClass, "G2V");
            sol.BasicProperties.Add(Constants.PropertyNames.Description, "The star at the center of the Solar System.");
            sol.BasicProperties.Add(Constants.PropertyNames.Radius, "695700 km");
            sol.BasicProperties.Add(Constants.PropertyNames.Area, "6.09E12 km2");
            sol.BasicProperties.Add(Constants.PropertyNames.Volume, "1.41E18 km3");
            sol.BasicProperties.Add(Constants.PropertyNames.Flattening, "9E-6");
            sol.BasicProperties.Add(Constants.PropertyNames.Mass, "1.9891E30 kg");
            sol.BasicProperties.Add(Constants.PropertyNames.Density, "1.408 g/cm3");
            sol.BasicProperties.Add(Constants.PropertyNames.Gravity, "274 m/s2");
            sol.BasicProperties.Add(Constants.PropertyNames.EscapeVelocity, "617.7 km/s");

            var catalogue = new Dictionary<string, string>();
            catalogue.Add("HabHyg", "0");
            catalogue.Add("Hip", "0");
            sol.Properties.AddGroup("Catalogue", catalogue);

            return sol;
        }

        public static Planet CreateMercury(IStellarMap map = null)
        {
            Planet mercury = new Planet("Mercury");
            
            mercury.BasicProperties.Add(Constants.PropertyNames.Description, "The first planet in the Solar System.");
            mercury.BasicProperties.Add(Constants.PropertyNames.Type, "Rocky Planet");
            mercury.BasicProperties.Add(Constants.PropertyNames.Radius, "2439.7 km");
            mercury.BasicProperties.Add(Constants.PropertyNames.Area, "7.48E7 km2");
            mercury.BasicProperties.Add(Constants.PropertyNames.Volume, "6.083E10 km3");
            mercury.BasicProperties.Add(Constants.PropertyNames.Flattening, "0.0000");
            mercury.BasicProperties.Add(Constants.PropertyNames.Mass, "3.3011E23 kg");
            mercury.BasicProperties.Add(Constants.PropertyNames.Density, "5.427 g/cm3");
            mercury.BasicProperties.Add(Constants.PropertyNames.Gravity, "3.7 m/s2");
            mercury.BasicProperties.Add(Constants.PropertyNames.EscapeVelocity, "4.25 km/s");

            map ??= BaseStellarMap.DefaultMap;
            map.Add(mercury);

            return mercury;
        }

        public static Planet CreateVenus(IStellarMap map = null)
        {
            Planet venus = new Planet("Venus");

            venus.BasicProperties.Add(Constants.PropertyNames.Description, "The 2nd planet in the Solar System.");
            venus.BasicProperties.Add(Constants.PropertyNames.Type, "Rocky Planet");
            venus.BasicProperties.Add(Constants.PropertyNames.Radius, "6051.8 km");
            venus.BasicProperties.Add(Constants.PropertyNames.Area, "4.6023E8 km2");
            venus.BasicProperties.Add(Constants.PropertyNames.Volume, "9.2843E11 km3");
            venus.BasicProperties.Add(Constants.PropertyNames.Flattening, "0");
            venus.BasicProperties.Add(Constants.PropertyNames.Mass, "4.8675E24 kg");
            venus.BasicProperties.Add(Constants.PropertyNames.Density, "5.243 g/cm3");
            venus.BasicProperties.Add(Constants.PropertyNames.Gravity, "8.87 m/s2");
            venus.BasicProperties.Add(Constants.PropertyNames.EscapeVelocity, "10.36 km/s");

            map ??= BaseStellarMap.DefaultMap;
            map.Add(venus);            

            return venus;
        }

        public static Planet CreateEarth(IStellarMap map = null)
        {
            Planet earth = new Planet("Earth");

            map ??= BaseStellarMap.DefaultMap;
            map.Add(earth);              

            earth.BasicProperties.Add(Constants.PropertyNames.Description, "The 3nd planet in the Solar System" +
                " and the only object known to harbor life");
            earth.BasicProperties.Add(Constants.PropertyNames.Type, "Rocky Planet");
            earth.BasicProperties.Add(Constants.PropertyNames.Radius, "6371.0 km");
            earth.BasicProperties.Add(Constants.PropertyNames.Area, "5.10072E8 km2");
            earth.BasicProperties.Add(Constants.PropertyNames.Volume, "1.08321E12 km3");
            earth.BasicProperties.Add(Constants.PropertyNames.Flattening, ".0033528");
            earth.BasicProperties.Add(Constants.PropertyNames.Mass, "5.97237E24 kg");
            earth.BasicProperties.Add(Constants.PropertyNames.Density, "5.514 g/cm3");
            earth.BasicProperties.Add(Constants.PropertyNames.Gravity, "9.80665 m/s2");
            earth.BasicProperties.Add(Constants.PropertyNames.EscapeVelocity, "11.186 km/s");

            #region Moon
            Satellite moon = new Satellite("Moon");
            earth.Add(moon);

            moon.BasicProperties.Add(Constants.PropertyNames.Description, "The only natural satellite of the planet Earth");
            moon.BasicProperties.Add(Constants.PropertyNames.Radius, "1737.4 km");
            moon.BasicProperties.Add(Constants.PropertyNames.Area, "3.793E7 km2");
            moon.BasicProperties.Add(Constants.PropertyNames.Volume, "2.1958E10 km3");
            moon.BasicProperties.Add(Constants.PropertyNames.Flattening, "0.0012");
            moon.BasicProperties.Add(Constants.PropertyNames.Mass, "7.342E22 kg");
            moon.BasicProperties.Add(Constants.PropertyNames.Density, "3.344 g/cm3");
            moon.BasicProperties.Add(Constants.PropertyNames.Gravity, "1.62 m/s2");
            moon.BasicProperties.Add(Constants.PropertyNames.EscapeVelocity, "2.38 km/s");
            #endregion

            return earth;
        }

        public static Planet CreateMars(IStellarMap map = null)
        {
            Planet mars = new Planet("Mars");

            map ??= BaseStellarMap.DefaultMap;
            map.Add(mars);  

            mars.BasicProperties.Add(Constants.PropertyNames.Description, "The 4th planet in the Solar System.");
            mars.BasicProperties.Add(Constants.PropertyNames.Type, "Rocky Planet");
            mars.BasicProperties.Add(Constants.PropertyNames.Radius, "3389.5 km");
            mars.BasicProperties.Add(Constants.PropertyNames.Area, "1.448E8 km2");
            mars.BasicProperties.Add(Constants.PropertyNames.Volume, "1.6318E11 km3");
            mars.BasicProperties.Add(Constants.PropertyNames.Flattening, "0.00589");
            mars.BasicProperties.Add(Constants.PropertyNames.Mass, "6.4171E23 kg");
            mars.BasicProperties.Add(Constants.PropertyNames.Density, "3.9335 g/cm3");
            mars.BasicProperties.Add(Constants.PropertyNames.Gravity, "3.721 m/s2");
            mars.BasicProperties.Add(Constants.PropertyNames.EscapeVelocity, "5.027 km/s");

            #region Phobos
            Satellite phobos = new Satellite("Phobos");
            mars.Add(phobos);

            phobos.BasicProperties.Add(Constants.PropertyNames.Description, "First and largest natural satellite of Mars.");
            phobos.BasicProperties.Add(Constants.PropertyNames.Dimensions, "27x22X18 km");
            phobos.BasicProperties.Add(Constants.PropertyNames.Radius, "11.2667 km");
            phobos.BasicProperties.Add(Constants.PropertyNames.Area, "1548.3 km2");
            phobos.BasicProperties.Add(Constants.PropertyNames.Volume, "5783.6 km3");
            phobos.BasicProperties.Add(Constants.PropertyNames.Mass, "1.0659E16 kg");
            phobos.BasicProperties.Add(Constants.PropertyNames.Density, "1.876 g/cm3");
            phobos.BasicProperties.Add(Constants.PropertyNames.Gravity, "0.0057 m/s2");
            phobos.BasicProperties.Add(Constants.PropertyNames.EscapeVelocity, "11.39 m/s");
            #endregion

            #region Deimos
            Satellite deimos = new Satellite("Deimos");
            mars.Add(deimos);

            deimos.BasicProperties.Add(Constants.PropertyNames.Description, "Second natural satellite of Mars.");
            deimos.BasicProperties.Add(Constants.PropertyNames.Dimensions, "15x12.2x11 km");
            deimos.BasicProperties.Add(Constants.PropertyNames.Radius, "6.2 km");
            deimos.BasicProperties.Add(Constants.PropertyNames.Area, "495.155 km2");
            deimos.BasicProperties.Add(Constants.PropertyNames.Volume, "999.78 km3");
            deimos.BasicProperties.Add(Constants.PropertyNames.Mass, "1.4762E15 kg");
            deimos.BasicProperties.Add(Constants.PropertyNames.Density, "1.471 g/cm3");
            deimos.BasicProperties.Add(Constants.PropertyNames.Gravity, "0.003 m/s2");
            deimos.BasicProperties.Add(Constants.PropertyNames.EscapeVelocity, "5.556 m/s");
            #endregion

            return mars;
        }

        public static Planet CreateJupiter(IStellarMap map = null)
        {
            Planet jupiter = new Planet("Jupiter");

            map ??= BaseStellarMap.DefaultMap;
            map.Add(jupiter);

            jupiter.BasicProperties.Add(Constants.PropertyNames.Description, "The fifth and largets planet in the Solar System.");
            jupiter.BasicProperties.Add(Constants.PropertyNames.Type, "Gas Giant Planet");
            jupiter.BasicProperties.Add(Constants.PropertyNames.Radius, "69911 km");
            jupiter.BasicProperties.Add(Constants.PropertyNames.Area, "6.1419E10 km2");
            jupiter.BasicProperties.Add(Constants.PropertyNames.Volume, "1.4313E15 km3");
            jupiter.BasicProperties.Add(Constants.PropertyNames.Flattening, "0.06487");
            jupiter.BasicProperties.Add(Constants.PropertyNames.Mass, "1.9882E27 kg");
            jupiter.BasicProperties.Add(Constants.PropertyNames.Density, "1.326 g/cm3");
            jupiter.BasicProperties.Add(Constants.PropertyNames.Gravity, "24.79 m/s2");
            jupiter.BasicProperties.Add(Constants.PropertyNames.EscapeVelocity, "59.5 km/s");

            // There are currently 79 moons of Jupiter and of course I'm not going to enter them all in
            // instead I'll do the inner moons, the Galilean moons and finish off with the top 10 largest (if not already added).
            // Inner Moons
            #region Metis
            Satellite moon = new Satellite("Metis");
            jupiter.Add(moon);

            moon.BasicProperties.Add(Constants.PropertyNames.Description, "First inner natural satellite of Jupiter.");
            moon.BasicProperties.Add(Constants.PropertyNames.Dimensions, "60x40x34 km");
            moon.BasicProperties.Add(Constants.PropertyNames.Radius, "21.5 km");
            moon.BasicProperties.Add(Constants.PropertyNames.Area, "5800 km2");
            moon.BasicProperties.Add(Constants.PropertyNames.Volume, "42700 km3");
            moon.BasicProperties.Add(Constants.PropertyNames.Mass, "unknown kg");
            moon.BasicProperties.Add(Constants.PropertyNames.Density, "unknown g/cm3");
            moon.BasicProperties.Add(Constants.PropertyNames.Gravity, "unknown m/s2");
            moon.BasicProperties.Add(Constants.PropertyNames.EscapeVelocity, "unknown m/s");
            #endregion

            #region Adrastea
            moon = new Satellite("Adrastea");
            jupiter.Add(moon);

            moon.BasicProperties.Add(Constants.PropertyNames.Description, "Second inner natural satellite of Jupiter.");
            moon.BasicProperties.Add(Constants.PropertyNames.Dimensions, "20x16x14 km");
            moon.BasicProperties.Add(Constants.PropertyNames.Radius, "18.2 km");
            moon.BasicProperties.Add(Constants.PropertyNames.Area, "unknown km2");
            moon.BasicProperties.Add(Constants.PropertyNames.Volume, "2345 km3");
            moon.BasicProperties.Add(Constants.PropertyNames.Mass, "unknown kg");
            moon.BasicProperties.Add(Constants.PropertyNames.Density, "unknown g/cm3");
            moon.BasicProperties.Add(Constants.PropertyNames.Gravity, "unknown m/s2");
            moon.BasicProperties.Add(Constants.PropertyNames.EscapeVelocity, "unknown m/s");
            #endregion

            #region Amalthea
            moon = new Satellite("Amalthea");
            jupiter.Add(moon);

            moon.BasicProperties.Add(Constants.PropertyNames.Description, "Third inner natural satellite and the 5th largest of Jupiter.");
            moon.BasicProperties.Add(Constants.PropertyNames.Dimensions, "250x146x128 km");
            moon.BasicProperties.Add(Constants.PropertyNames.Radius, "83.5 km");
            moon.BasicProperties.Add(Constants.PropertyNames.Area, "unknown km2");
            moon.BasicProperties.Add(Constants.PropertyNames.Volume, "2.43E6 km3");
            moon.BasicProperties.Add(Constants.PropertyNames.Mass, "2.08E18 kg");
            moon.BasicProperties.Add(Constants.PropertyNames.Density, "0.857 g/cm3");
            moon.BasicProperties.Add(Constants.PropertyNames.Gravity, "0.02 m/s2");
            moon.BasicProperties.Add(Constants.PropertyNames.EscapeVelocity, "0.058 m/s");
            #endregion

            #region Thebe
            moon = new Satellite("Thebe");
            jupiter.Add(moon);

            moon.BasicProperties.Add(Constants.PropertyNames.Description, "Fourth inner natural satellite and the 7th largest of Jupiter.");
            moon.BasicProperties.Add(Constants.PropertyNames.Dimensions, "116x98x84 km");
            moon.BasicProperties.Add(Constants.PropertyNames.Radius, "49.3 km");
            moon.BasicProperties.Add(Constants.PropertyNames.Area, "unknown km2");
            moon.BasicProperties.Add(Constants.PropertyNames.Volume, "5.0E5 km3");
            moon.BasicProperties.Add(Constants.PropertyNames.Mass, "unknown kg");
            moon.BasicProperties.Add(Constants.PropertyNames.Density, "unknown g/cm3");
            moon.BasicProperties.Add(Constants.PropertyNames.Gravity, "0.04 m/s2");
            moon.BasicProperties.Add(Constants.PropertyNames.EscapeVelocity, "20-30 m/s");
            #endregion

            //Galilean Moons
            #region Io
            moon = new Satellite("Io");
            jupiter.Add(moon);

            moon.BasicProperties.Add(Constants.PropertyNames.Description, "First Galilean and the 3rd largest natural satellite of Jupiter.");
            moon.BasicProperties.Add(Constants.PropertyNames.Dimensions, "3660x3637.4x3630.6 km");
            moon.BasicProperties.Add(Constants.PropertyNames.Radius, "1821.6 km");
            moon.BasicProperties.Add(Constants.PropertyNames.Area, "4.191E7 km2");
            moon.BasicProperties.Add(Constants.PropertyNames.Volume, "2.53E10 km3");
            moon.BasicProperties.Add(Constants.PropertyNames.Mass, "8.932E22 kg");
            moon.BasicProperties.Add(Constants.PropertyNames.Density, "3.528 g/cm3");
            moon.BasicProperties.Add(Constants.PropertyNames.Gravity, "1.796 m/s2");
            moon.BasicProperties.Add(Constants.PropertyNames.EscapeVelocity, "2.558 km/s");
            #endregion

            #region Europa
            moon = new Satellite("Europa");
            jupiter.Add(moon);

            moon.BasicProperties.Add(Constants.PropertyNames.Description, "Second Galilean and 4th largest natural satellite of Jupiter.");
            moon.BasicProperties.Add(Constants.PropertyNames.Radius, "1560.8 km");
            moon.BasicProperties.Add(Constants.PropertyNames.Area, "3.09E7 km2");
            moon.BasicProperties.Add(Constants.PropertyNames.Volume, "1.593E10 km3");
            moon.BasicProperties.Add(Constants.PropertyNames.Mass, "4.798E22 kg");
            moon.BasicProperties.Add(Constants.PropertyNames.Density, "3.013 g/cm3");
            moon.BasicProperties.Add(Constants.PropertyNames.Gravity, "1.314 m/s2");
            moon.BasicProperties.Add(Constants.PropertyNames.EscapeVelocity, "2.025 km/s");
            #endregion

            #region Ganymede
            moon = new Satellite("Ganymede");
            jupiter.Add(moon);

            moon.BasicProperties.Add(Constants.PropertyNames.Description, "Third Galilean and largest natural satellite of Jupiter.");
            moon.BasicProperties.Add(Constants.PropertyNames.Radius, "2634.1 km");
            moon.BasicProperties.Add(Constants.PropertyNames.Area, "8.72E7 km2");
            moon.BasicProperties.Add(Constants.PropertyNames.Volume, "7.66E10 km3");
            moon.BasicProperties.Add(Constants.PropertyNames.Mass, "1.4819E23 kg");
            moon.BasicProperties.Add(Constants.PropertyNames.Density, "1.936 g/cm3");
            moon.BasicProperties.Add(Constants.PropertyNames.Gravity, "1.428 m/s2");
            moon.BasicProperties.Add(Constants.PropertyNames.EscapeVelocity, "2.741 km/s");
            #endregion

            #region Callisto
            moon = new Satellite("Callisto");
            jupiter.Add(moon);

            moon.BasicProperties.Add(Constants.PropertyNames.Description, "Fourth Galilean and 2nd largest natural satellite of Jupiter.");
            moon.BasicProperties.Add(Constants.PropertyNames.Radius, "2410.3 km");
            moon.BasicProperties.Add(Constants.PropertyNames.Area, "7.30E7 km2");
            moon.BasicProperties.Add(Constants.PropertyNames.Volume, "5.9E10 km3");
            moon.BasicProperties.Add(Constants.PropertyNames.Mass, "1.076E23 kg");
            moon.BasicProperties.Add(Constants.PropertyNames.Density, "1.8344 g/cm3");
            moon.BasicProperties.Add(Constants.PropertyNames.Gravity, "1.235 m/s2");
            moon.BasicProperties.Add(Constants.PropertyNames.EscapeVelocity, "2.440 km/s");
            #endregion

            #region Himalia
            moon = new Satellite("Himalia");
            jupiter.Add(moon);

            moon.BasicProperties.Add(Constants.PropertyNames.Description, "11th natural and 6th largest satellite of Jupiter.");
            moon.BasicProperties.Add(Constants.PropertyNames.Dimensions, "205.6x141.4x? km"); // strange only 2 dimensions given
            moon.BasicProperties.Add(Constants.PropertyNames.Radius, "85 km");
            moon.BasicProperties.Add(Constants.PropertyNames.Area, "9.1E4 km2");
            moon.BasicProperties.Add(Constants.PropertyNames.Volume, "2.6E6 km3");
            moon.BasicProperties.Add(Constants.PropertyNames.Mass, "4.2E18 kg");
            moon.BasicProperties.Add(Constants.PropertyNames.Density, "2.6 g/cm3");
            moon.BasicProperties.Add(Constants.PropertyNames.Gravity, "0.062 m/s2");
            moon.BasicProperties.Add(Constants.PropertyNames.EscapeVelocity, "0.1 km/s");
            #endregion

            #region Elara
            moon = new Satellite("Elara");
            jupiter.Add(moon);

            moon.BasicProperties.Add(Constants.PropertyNames.Description, "Fourth inner natural satellite of Jupiter.");
            moon.BasicProperties.Add(Constants.PropertyNames.Dimensions, "unknown km");
            moon.BasicProperties.Add(Constants.PropertyNames.Radius, "40 km");
            moon.BasicProperties.Add(Constants.PropertyNames.Area, "unknown km2");
            moon.BasicProperties.Add(Constants.PropertyNames.Volume, "unknown km3");
            moon.BasicProperties.Add(Constants.PropertyNames.Mass, "8.7E17 kg");
            moon.BasicProperties.Add(Constants.PropertyNames.Density, "2.6 g/cm3");
            moon.BasicProperties.Add(Constants.PropertyNames.Gravity, "unknown m/s2");
            moon.BasicProperties.Add(Constants.PropertyNames.EscapeVelocity, "unknown m/s");
            #endregion

            #region Pasiphae
            moon = new Satellite("Pasiphae");
            jupiter.Add(moon);

            moon.BasicProperties.Add(Constants.PropertyNames.Description, "60th and 9th largest inner natural satellite of Jupiter.");
            moon.BasicProperties.Add(Constants.PropertyNames.Dimensions, "unknown km");
            moon.BasicProperties.Add(Constants.PropertyNames.Radius, "28.9 km");
            moon.BasicProperties.Add(Constants.PropertyNames.Area, "unknown km2");
            moon.BasicProperties.Add(Constants.PropertyNames.Volume, "unknown km3");
            moon.BasicProperties.Add(Constants.PropertyNames.Mass, "3E17 kg");
            moon.BasicProperties.Add(Constants.PropertyNames.Density, "2.6 g/cm3");
            moon.BasicProperties.Add(Constants.PropertyNames.Gravity, "0.022 m/s2");
            moon.BasicProperties.Add(Constants.PropertyNames.EscapeVelocity, "0.036 km/s");
            #endregion

            #region Carme
            moon = new Satellite("Carme");
            jupiter.Add(moon);

            moon.BasicProperties.Add(Constants.PropertyNames.Description, "74th and 10th largest natural satellite of Jupiter.");
            moon.BasicProperties.Add(Constants.PropertyNames.Dimensions, "unknown km");
            moon.BasicProperties.Add(Constants.PropertyNames.Radius, "23.35 km");
            moon.BasicProperties.Add(Constants.PropertyNames.Area, "unknown km2");
            moon.BasicProperties.Add(Constants.PropertyNames.Volume, "unknown km3");
            moon.BasicProperties.Add(Constants.PropertyNames.Mass, "unknown kg");
            moon.BasicProperties.Add(Constants.PropertyNames.Density, "2.6 g/cm3");
            moon.BasicProperties.Add(Constants.PropertyNames.Gravity, "unknown m/s2");
            moon.BasicProperties.Add(Constants.PropertyNames.EscapeVelocity, "unknown m/s");
            #endregion

            return jupiter;
        }

        public static Planet CreateSaturn(IStellarMap map = null)
        {
            Planet saturn = new Planet("Saturn");

            map ??= BaseStellarMap.DefaultMap;
            map.Add(saturn);

            saturn.BasicProperties.Add(Constants.PropertyNames.Description, "The 6th planet in the Solar System.");
            saturn.BasicProperties.Add(Constants.PropertyNames.Type, "Gas Giant Planet");
            saturn.BasicProperties.Add(Constants.PropertyNames.Radius, "58232 km");
            saturn.BasicProperties.Add(Constants.PropertyNames.Area, "4.27E10 km2");
            saturn.BasicProperties.Add(Constants.PropertyNames.Volume, "8.2713E14 km3");
            saturn.BasicProperties.Add(Constants.PropertyNames.Flattening, "0.09796");
            saturn.BasicProperties.Add(Constants.PropertyNames.Mass, "5.6834E26 kg");
            saturn.BasicProperties.Add(Constants.PropertyNames.Density, "0.867 g/cm3");
            saturn.BasicProperties.Add(Constants.PropertyNames.Gravity, "10.44 m/s2");
            saturn.BasicProperties.Add(Constants.PropertyNames.EscapeVelocity, "35.5 km/s");

            // For Saturn I'll just be adding top 10-20 moons by size
            #region Titan
            Satellite moon = new Satellite("Titan");
            saturn.Add(moon);

            moon.BasicProperties.Add(Constants.PropertyNames.Description, "Largest natural satellite of Saturn and second largest in the Solarsystem.");
            //moon.BasicProperties.Add(Constants.PropertyNames.Dimensions, "unknown km");
            moon.BasicProperties.Add(Constants.PropertyNames.Radius, "2574.73 km");
            moon.BasicProperties.Add(Constants.PropertyNames.Area, "8.3E7 km2");
            moon.BasicProperties.Add(Constants.PropertyNames.Volume, "7.16E10 km3");
            moon.BasicProperties.Add(Constants.PropertyNames.Mass, "1.3452E23 kg");
            moon.BasicProperties.Add(Constants.PropertyNames.Density, "1.8798 g/cm3");
            moon.BasicProperties.Add(Constants.PropertyNames.Gravity, "1.352 m/s2");
            moon.BasicProperties.Add(Constants.PropertyNames.EscapeVelocity, "2.639 m/s");
            #endregion

            #region Rhea
            moon = new Satellite("Rhea");
            saturn.Add(moon);

            moon.BasicProperties.Add(Constants.PropertyNames.Description, "2nd largest natural satellite of Saturn.");
            moon.BasicProperties.Add(Constants.PropertyNames.Dimensions, "1532.4x1525.6x1524.4 km");
            moon.BasicProperties.Add(Constants.PropertyNames.Radius, "763.8 km");
            moon.BasicProperties.Add(Constants.PropertyNames.Area, "7.337E6 km2");
            moon.BasicProperties.Add(Constants.PropertyNames.Volume, "unknown km3");
            moon.BasicProperties.Add(Constants.PropertyNames.Mass, "2.3E21 kg");
            moon.BasicProperties.Add(Constants.PropertyNames.Density, "1.236 g/cm3");
            moon.BasicProperties.Add(Constants.PropertyNames.Gravity, "0.264 m/s2");
            moon.BasicProperties.Add(Constants.PropertyNames.EscapeVelocity, "0.635 m/s");
            #endregion

            #region Iapetus
            moon = new Satellite("Iapetus");
            saturn.Add(moon);

            moon.BasicProperties.Add(Constants.PropertyNames.Description, "3rd largest natural satellite of Saturn.");
            moon.BasicProperties.Add(Constants.PropertyNames.Dimensions, "1492.0x1492.0x1424.0 km");
            moon.BasicProperties.Add(Constants.PropertyNames.Radius, "734.5 km");
            moon.BasicProperties.Add(Constants.PropertyNames.Area, "6.7E6 km2");
            moon.BasicProperties.Add(Constants.PropertyNames.Volume, "unknown km3");
            moon.BasicProperties.Add(Constants.PropertyNames.Mass, "1.806E21 kg");
            moon.BasicProperties.Add(Constants.PropertyNames.Density, "1.088 g/cm3");
            moon.BasicProperties.Add(Constants.PropertyNames.Gravity, "0.223 m/s2");
            moon.BasicProperties.Add(Constants.PropertyNames.EscapeVelocity, "0.573 m/s");
            #endregion

            #region Dione
            moon = new Satellite("Dione");
            saturn.Add(moon);

            moon.BasicProperties.Add(Constants.PropertyNames.Description, "4th largest natural satellite of Saturn.");
            moon.BasicProperties.Add(Constants.PropertyNames.Dimensions, "1128.8x1122.6x1119.2 km");
            moon.BasicProperties.Add(Constants.PropertyNames.Radius, "561.4 km");
            moon.BasicProperties.Add(Constants.PropertyNames.Area, "3.96E6 km2");
            moon.BasicProperties.Add(Constants.PropertyNames.Volume, "unknown km3");
            moon.BasicProperties.Add(Constants.PropertyNames.Mass, "1.095E21 kg");
            moon.BasicProperties.Add(Constants.PropertyNames.Density, "1.478 g/cm3");
            moon.BasicProperties.Add(Constants.PropertyNames.Gravity, "0.232 m/s2");
            moon.BasicProperties.Add(Constants.PropertyNames.EscapeVelocity, "0.51 m/s");
            #endregion


            #region Tethys
            moon = new Satellite("Tethys");
            saturn.Add(moon);

            moon.BasicProperties.Add(Constants.PropertyNames.Description, "5th largest natural satellite of Saturn.");
            moon.BasicProperties.Add(Constants.PropertyNames.Dimensions, "1076.8x1057.4x1052.6 km");
            moon.BasicProperties.Add(Constants.PropertyNames.Radius, "531.1 km");
            moon.BasicProperties.Add(Constants.PropertyNames.Area, "unknown km2");
            moon.BasicProperties.Add(Constants.PropertyNames.Volume, "unknown km3");
            moon.BasicProperties.Add(Constants.PropertyNames.Mass, "6.174E20 kg");
            moon.BasicProperties.Add(Constants.PropertyNames.Density, "0.984 g/cm3");
            moon.BasicProperties.Add(Constants.PropertyNames.Gravity, "0.146 m/s2");
            moon.BasicProperties.Add(Constants.PropertyNames.EscapeVelocity, "0.394 m/s");
            #endregion

            #region Enceladus
            moon = new Satellite("Enceladus");
            saturn.Add(moon);

            moon.BasicProperties.Add(Constants.PropertyNames.Description, "6th largest natural satellite of Saturn.");
            moon.BasicProperties.Add(Constants.PropertyNames.Dimensions, "513.2x502.8x496.6 km");
            moon.BasicProperties.Add(Constants.PropertyNames.Radius, "252.1 km");
            moon.BasicProperties.Add(Constants.PropertyNames.Area, "unknown km2");
            moon.BasicProperties.Add(Constants.PropertyNames.Volume, "unknown km3");
            moon.BasicProperties.Add(Constants.PropertyNames.Mass, "1.08E20 kg");
            moon.BasicProperties.Add(Constants.PropertyNames.Density, "1.609 g/cm3");
            moon.BasicProperties.Add(Constants.PropertyNames.Gravity, "0.113 m/s2");
            moon.BasicProperties.Add(Constants.PropertyNames.EscapeVelocity, "0.239 m/s");
            #endregion

            #region Mimas
            moon = new Satellite("Mimas");
            saturn.Add(moon);

            moon.BasicProperties.Add(Constants.PropertyNames.Description, "7th largest natural satellite of Saturn.");
            moon.BasicProperties.Add(Constants.PropertyNames.Dimensions, "415.6x393.4x381.2 km");
            moon.BasicProperties.Add(Constants.PropertyNames.Radius, "198.2 km");
            moon.BasicProperties.Add(Constants.PropertyNames.Area, "4.9E5 km2");
            moon.BasicProperties.Add(Constants.PropertyNames.Volume, "3.2E7 km3");
            moon.BasicProperties.Add(Constants.PropertyNames.Mass, "3.749E19 kg");
            moon.BasicProperties.Add(Constants.PropertyNames.Density, "1.148 g/cm3");
            moon.BasicProperties.Add(Constants.PropertyNames.Gravity, "0.064 m/s2");
            moon.BasicProperties.Add(Constants.PropertyNames.EscapeVelocity, "0.159 m/s");
            #endregion

            #region Hyperion
            moon = new Satellite("Hyperion");
            saturn.Add(moon);

            moon.BasicProperties.Add(Constants.PropertyNames.Description, "8th largest natural satellite of Saturn.");
            moon.BasicProperties.Add(Constants.PropertyNames.Dimensions, "360.2x266.0x205.4 km");
            moon.BasicProperties.Add(Constants.PropertyNames.Radius, "270 km");
            moon.BasicProperties.Add(Constants.PropertyNames.Area, "unknown km2");
            moon.BasicProperties.Add(Constants.PropertyNames.Volume, "unknown km3");
            moon.BasicProperties.Add(Constants.PropertyNames.Mass, "5.62E18 kg");
            moon.BasicProperties.Add(Constants.PropertyNames.Density, "0.544 g/cm3");
            moon.BasicProperties.Add(Constants.PropertyNames.Gravity, "0.021 m/s2");
            moon.BasicProperties.Add(Constants.PropertyNames.EscapeVelocity, "0.099 m/s");
            #endregion

            #region Phoebe
            moon = new Satellite("Phoebe");
            saturn.Add(moon);

            moon.BasicProperties.Add(Constants.PropertyNames.Description, "9th largest natural satellite of Saturn.");
            moon.BasicProperties.Add(Constants.PropertyNames.Dimensions, "218.8x217.0x203.6 km");
            moon.BasicProperties.Add(Constants.PropertyNames.Radius, "106.5 km");
            moon.BasicProperties.Add(Constants.PropertyNames.Area, "unknown km2");
            moon.BasicProperties.Add(Constants.PropertyNames.Volume, "unknown km3");
            moon.BasicProperties.Add(Constants.PropertyNames.Mass, "8.292E18 kg");
            moon.BasicProperties.Add(Constants.PropertyNames.Density, "1.638 g/cm3");
            moon.BasicProperties.Add(Constants.PropertyNames.Gravity, "0.039 m/s2");
            moon.BasicProperties.Add(Constants.PropertyNames.EscapeVelocity, "0.10 m/s");
            #endregion

            #region Janus
            moon = new Satellite("Janus");
            saturn.Add(moon);

            moon.BasicProperties.Add(Constants.PropertyNames.Description, "10th largest natural satellite of Saturn.");
            moon.BasicProperties.Add(Constants.PropertyNames.Dimensions, "203x185x152.6 km");
            moon.BasicProperties.Add(Constants.PropertyNames.Radius, "89.5 km");
            moon.BasicProperties.Add(Constants.PropertyNames.Area, "unknown km2");
            moon.BasicProperties.Add(Constants.PropertyNames.Volume, "3xE6 km3");
            moon.BasicProperties.Add(Constants.PropertyNames.Mass, "1.8975E18 kg");
            moon.BasicProperties.Add(Constants.PropertyNames.Density, "0.63 g/cm3");
            moon.BasicProperties.Add(Constants.PropertyNames.Gravity, "0.017 m/s2");
            moon.BasicProperties.Add(Constants.PropertyNames.EscapeVelocity, "unknown m/s");
            #endregion

            return saturn;
        }

        public static Planet CreateUranus(IStellarMap map = null)
        {
            Planet uranus = new Planet("Uranus");

            map ??= BaseStellarMap.DefaultMap;
            map.Add(uranus);
            
            uranus.BasicProperties.Add(Constants.PropertyNames.Description, "The 7th planet in the Solar System.");
            uranus.BasicProperties.Add(Constants.PropertyNames.Type, "Gas Giant Planet");
            uranus.BasicProperties.Add(Constants.PropertyNames.Radius, "58232 km");
            uranus.BasicProperties.Add(Constants.PropertyNames.Area, "4.27E10 km2");
            uranus.BasicProperties.Add(Constants.PropertyNames.Volume, "8.2713E14 km3");
            uranus.BasicProperties.Add(Constants.PropertyNames.Flattening, "0.09796");
            uranus.BasicProperties.Add(Constants.PropertyNames.Mass, "5.6834E26 kg");
            uranus.BasicProperties.Add(Constants.PropertyNames.Density, "0.867 g/cm3");
            uranus.BasicProperties.Add(Constants.PropertyNames.Gravity, "10.44 m/s2");
            uranus.BasicProperties.Add(Constants.PropertyNames.EscapeVelocity, "35.5 km/s");            

            #region Titania
            Satellite moon = new Satellite("Titania");
            uranus.Add(moon);

            moon.BasicProperties.Add(Constants.PropertyNames.Description, "The largest natural satellite of Uranus.");
            //moon.BasicProperties.Add(Constants.PropertyNames.Dimensions, "203x185x152.6 km");
            moon.BasicProperties.Add(Constants.PropertyNames.Radius, "788.4 km");
            moon.BasicProperties.Add(Constants.PropertyNames.Area, "7.82E6 km2");
            moon.BasicProperties.Add(Constants.PropertyNames.Volume, "2.065xE9 km3");
            moon.BasicProperties.Add(Constants.PropertyNames.Mass, "3.4E21 kg");
            moon.BasicProperties.Add(Constants.PropertyNames.Density, "1.711 g/cm3");
            moon.BasicProperties.Add(Constants.PropertyNames.Gravity, "0.379 m/s2");
            moon.BasicProperties.Add(Constants.PropertyNames.EscapeVelocity, "0.773 m/s");
            #endregion            

            #region Oberon
            moon = new Satellite("Oberon");
            uranus.Add(moon);

            moon.BasicProperties.Add(Constants.PropertyNames.Description, "The 2nd largest natural satellite of Uranus.");
            //moon.BasicProperties.Add(Constants.PropertyNames.Dimensions, "203x185x152.6 km");
            moon.BasicProperties.Add(Constants.PropertyNames.Radius, "761.4 km");
            moon.BasicProperties.Add(Constants.PropertyNames.Area, "7.285E6 km2");
            moon.BasicProperties.Add(Constants.PropertyNames.Volume, "1.849xE9 km3");
            moon.BasicProperties.Add(Constants.PropertyNames.Mass, "3.076E21 kg");
            moon.BasicProperties.Add(Constants.PropertyNames.Density, "1.63 g/cm3");
            moon.BasicProperties.Add(Constants.PropertyNames.Gravity, "0.346 m/s2");
            moon.BasicProperties.Add(Constants.PropertyNames.EscapeVelocity, "0.727 m/s");
            #endregion 

            #region Umbriel
            moon = new Satellite("Umbriel");
            uranus.Add(moon);

            moon.BasicProperties.Add(Constants.PropertyNames.Description, "The 3rd largest natural satellite of Uranus.");
            //moon.BasicProperties.Add(Constants.PropertyNames.Dimensions, "203x185x152.6 km");
            moon.BasicProperties.Add(Constants.PropertyNames.Radius, "584.7 km");
            moon.BasicProperties.Add(Constants.PropertyNames.Area, "4.296E6 km2");
            moon.BasicProperties.Add(Constants.PropertyNames.Volume, "8.373xE8 km3");
            moon.BasicProperties.Add(Constants.PropertyNames.Mass, "1.275E21 kg");
            moon.BasicProperties.Add(Constants.PropertyNames.Density, "1.39 g/cm3");
            moon.BasicProperties.Add(Constants.PropertyNames.Gravity, "0.2 m/s2");
            moon.BasicProperties.Add(Constants.PropertyNames.EscapeVelocity, "0.52 m/s");
            #endregion

            #region Ariel
            moon = new Satellite("Ariel");
            uranus.Add(moon);

            moon.BasicProperties.Add(Constants.PropertyNames.Description, "The 4th largest natural satellite of Uranus.");
            moon.BasicProperties.Add(Constants.PropertyNames.Dimensions, "1162.2x1155.8x11155.4 km");
            moon.BasicProperties.Add(Constants.PropertyNames.Radius, "578.9 km");
            moon.BasicProperties.Add(Constants.PropertyNames.Area, "4.211E6 km2");
            moon.BasicProperties.Add(Constants.PropertyNames.Volume, "8.126xE8 km3");
            moon.BasicProperties.Add(Constants.PropertyNames.Mass, "1.251E21 kg");
            moon.BasicProperties.Add(Constants.PropertyNames.Density, "1.592 g/cm3");
            moon.BasicProperties.Add(Constants.PropertyNames.Gravity, "0.269 m/s2");
            moon.BasicProperties.Add(Constants.PropertyNames.EscapeVelocity, "0.559 m/s");
            #endregion

            #region Miranda
            moon = new Satellite("Miranda");
            uranus.Add(moon);

            moon.BasicProperties.Add(Constants.PropertyNames.Description, "The 5th largest natural satellite of Uranus.");
            moon.BasicProperties.Add(Constants.PropertyNames.Dimensions, "480x468.4x465.8 km");
            moon.BasicProperties.Add(Constants.PropertyNames.Radius, "235.8 km");
            moon.BasicProperties.Add(Constants.PropertyNames.Area, "7.0E5 km2");
            moon.BasicProperties.Add(Constants.PropertyNames.Volume, "5.485xE7 km3");
            moon.BasicProperties.Add(Constants.PropertyNames.Mass, "6.4E19 kg");
            moon.BasicProperties.Add(Constants.PropertyNames.Density, "1.2 g/cm3");
            moon.BasicProperties.Add(Constants.PropertyNames.Gravity, "0.079 m/s2");
            moon.BasicProperties.Add(Constants.PropertyNames.EscapeVelocity, "0.193 m/s");
            #endregion

            return uranus;
        }

        public static Planet CreateNeptune(IStellarMap map = null)
        {
            Planet neptune = new Planet("Neptune");

            map ??= BaseStellarMap.DefaultMap;
            map.Add(neptune);

            neptune.BasicProperties.Add(Constants.PropertyNames.Description, "The 8th planet in the Solar System.");
            neptune.BasicProperties.Add(Constants.PropertyNames.Type, "Gas Giant Planet");
            neptune.BasicProperties.Add(Constants.PropertyNames.Radius, "24622 km");
            neptune.BasicProperties.Add(Constants.PropertyNames.Area, "4.27E9 km2");
            neptune.BasicProperties.Add(Constants.PropertyNames.Volume, "6.254E13 km3");
            neptune.BasicProperties.Add(Constants.PropertyNames.Flattening, "0.09796");
            neptune.BasicProperties.Add(Constants.PropertyNames.Mass, "1.024E26 kg");
            neptune.BasicProperties.Add(Constants.PropertyNames.Density, "1.638 g/cm3");
            neptune.BasicProperties.Add(Constants.PropertyNames.Gravity, "11.15 m/s2");
            neptune.BasicProperties.Add(Constants.PropertyNames.EscapeVelocity, "23.5 km/s");            

            #region Triton
            Satellite moon = new Satellite("Triton");
            neptune.Add(moon);

            moon.BasicProperties.Add(Constants.PropertyNames.Description, "The largest natural satellite of Neptune.");
            //moon.BasicProperties.Add(Constants.PropertyNames.Dimensions, "480x468.4x465.8 km");
            moon.BasicProperties.Add(Constants.PropertyNames.Radius, "1353.4 km");
            moon.BasicProperties.Add(Constants.PropertyNames.Area, "2.302E7 km2");
            moon.BasicProperties.Add(Constants.PropertyNames.Volume, "1.038xE10 km3");
            moon.BasicProperties.Add(Constants.PropertyNames.Mass, "2.139E21 kg");
            moon.BasicProperties.Add(Constants.PropertyNames.Density, "2.061 g/cm3");
            moon.BasicProperties.Add(Constants.PropertyNames.Gravity, "0.779 m/s2");
            moon.BasicProperties.Add(Constants.PropertyNames.EscapeVelocity, "1.445 m/s");
            #endregion

            #region Proteus
            moon = new Satellite("Proteus");
            neptune.Add(moon);

            moon.BasicProperties.Add(Constants.PropertyNames.Description, "The 2nd largest natural satellite of Neptune.");
            moon.BasicProperties.Add(Constants.PropertyNames.Dimensions, "424x390x396 km");
            moon.BasicProperties.Add(Constants.PropertyNames.Radius, "210 km");
            moon.BasicProperties.Add(Constants.PropertyNames.Area, "5.54E5 km2");
            moon.BasicProperties.Add(Constants.PropertyNames.Volume, "3.4xE7 km3");
            moon.BasicProperties.Add(Constants.PropertyNames.Mass, "4.4E19 kg");
            moon.BasicProperties.Add(Constants.PropertyNames.Density, "1.3 g/cm3");
            moon.BasicProperties.Add(Constants.PropertyNames.Gravity, "0.07 m/s2");
            moon.BasicProperties.Add(Constants.PropertyNames.EscapeVelocity, "0.17 m/s");
            #endregion

            #region Nereid
            moon = new Satellite("Nereid");
            neptune.Add(moon);

            moon.BasicProperties.Add(Constants.PropertyNames.Description, "The 3rd largest natural satellite of Neptune.");
            //moon.BasicProperties.Add(Constants.PropertyNames.Dimensions, "424x390x396 km");
            moon.BasicProperties.Add(Constants.PropertyNames.Radius, "178.5 km");
            //moon.BasicProperties.Add(Constants.PropertyNames.Area, "5.54E5 km2");
            //moon.BasicProperties.Add(Constants.PropertyNames.Volume, "3.4xE7 km3");
            //moon.BasicProperties.Add(Constants.PropertyNames.Mass, "4.4E19 kg");
            //moon.BasicProperties.Add(Constants.PropertyNames.Density, "1.3 g/cm3");
            //moon.BasicProperties.Add(Constants.PropertyNames.Gravity, "0.07 m/s2");
            //moon.BasicProperties.Add(Constants.PropertyNames.EscapeVelocity, "0.17 m/s");
            #endregion

            #region Larissa
            moon = new Satellite("Larissa");
            neptune.Add(moon);

            moon.BasicProperties.Add(Constants.PropertyNames.Description, "The 4th largest natural satellite of Neptune.");
            moon.BasicProperties.Add(Constants.PropertyNames.Dimensions, "216x204x168 km");
            moon.BasicProperties.Add(Constants.PropertyNames.Radius, "97 km");
            moon.BasicProperties.Add(Constants.PropertyNames.Area, "1.182E5 km2");
            moon.BasicProperties.Add(Constants.PropertyNames.Volume, "3.5xE6 km3");
            moon.BasicProperties.Add(Constants.PropertyNames.Mass, "4.2E18 kg");
            moon.BasicProperties.Add(Constants.PropertyNames.Density, "1.2 g/cm3");
            moon.BasicProperties.Add(Constants.PropertyNames.Gravity, "0.03 m/s2");
            moon.BasicProperties.Add(Constants.PropertyNames.EscapeVelocity, "0.076 m/s");
            #endregion

            return neptune;
        }

        public static DwarfPlanet CreateCeres(IStellarMap map = null)
        {
            DwarfPlanet c = new DwarfPlanet("Ceres");

            map ??= BaseStellarMap.DefaultMap;
            map.Add(c);

            return c;
        }

        public static DwarfPlanet CreatePluto(IStellarMap map = null)
        {
            DwarfPlanet p = new DwarfPlanet("Pluto");

            map ??= BaseStellarMap.DefaultMap;
            map.Add(p);

            return p;
        }

        public static ICollection<Asteroid> CreateAsteroids(IStellarMap map = null)
        {
            map ??= BaseStellarMap.DefaultMap;

            ICollection<Asteroid> asteroids = new List<Asteroid>();

            Asteroid a = new Asteroid("Vesta");
            asteroids.Add(a);
            map.Add(a);

            a = new Asteroid("Pallas");
            asteroids.Add(a);
            map.Add(a);

            a = new Asteroid("Hygiea");
            asteroids.Add(a);
            map.Add(a);

            a = new Asteroid("Euphrosyne");
            asteroids.Add(a);
            map.Add(a);

            a = new Asteroid("Interamnia");
            asteroids.Add(a);
            map.Add(a);

            a = new Asteroid("Davida");
            asteroids.Add(a);
            map.Add(a);

            a = new Asteroid("Herculina");
            asteroids.Add(a);
            map.Add(a);

            a = new Asteroid("Eunomia");
            asteroids.Add(a);
            map.Add(a);

            a = new Asteroid("Juno");
            asteroids.Add(a);
            map.Add(a);

            a = new Asteroid("Psyche");
            asteroids.Add(a);
            map.Add(a);

            a = new Asteroid("Europa");
            asteroids.Add(a);
            map.Add(a);

            return asteroids;
        }

        public static ICollection<Comet> CreateComets(IStellarMap map = null)
        {
            map ??= BaseStellarMap.DefaultMap;

            string[] cometNames = { "Halley's", "Caeser's", "Encke's", "Biela's", "Faye's", "Brorsen's", "d'Arrest's" };

            ICollection<Comet> comets = new List<Comet>();
            foreach (string name in cometNames)
            {
                Comet c = new Comet(name);
                comets.Add(c);
                map.Add(c);
            }

            return comets;
        }
    }
}

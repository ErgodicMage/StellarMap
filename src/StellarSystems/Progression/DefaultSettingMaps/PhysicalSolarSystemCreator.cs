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
            sol.Add(CreatePluto());

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

            moon.BasicProperties.Add(Constants.PropertyNames.Description, "Third inner natural satellite of Jupiter.");
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

            moon.BasicProperties.Add(Constants.PropertyNames.Description, "Fourth inner natural satellite of Jupiter.");
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

            moon.BasicProperties.Add(Constants.PropertyNames.Description, "First Galilean natural satellite of Jupiter.");
            moon.BasicProperties.Add(Constants.PropertyNames.Dimensions, "3660x3637.4x3630.6 km");
            moon.BasicProperties.Add(Constants.PropertyNames.Radius, "1821.6 km");
            moon.BasicProperties.Add(Constants.PropertyNames.Area, "4.191E7 km2");
            moon.BasicProperties.Add(Constants.PropertyNames.Volume, "2.53E10 km3");
            moon.BasicProperties.Add(Constants.PropertyNames.Mass, "8.932E22 kg");
            moon.BasicProperties.Add(Constants.PropertyNames.Density, "3.528 g/cm3");
            moon.BasicProperties.Add(Constants.PropertyNames.Gravity, "1.796 m/s2");
            moon.BasicProperties.Add(Constants.PropertyNames.EscapeVelocity, "2.558 m/s");
            #endregion

            #region Europa
            moon = new Satellite("Europa");
            jupiter.Add(moon);

            moon.BasicProperties.Add(Constants.PropertyNames.Description, "Second Galilean natural satellite of Jupiter.");
            moon.BasicProperties.Add(Constants.PropertyNames.Radius, "1560.8 km");
            moon.BasicProperties.Add(Constants.PropertyNames.Area, "3.09E7 km2");
            moon.BasicProperties.Add(Constants.PropertyNames.Volume, "1.593E10 km3");
            moon.BasicProperties.Add(Constants.PropertyNames.Mass, "4.798E22 kg");
            moon.BasicProperties.Add(Constants.PropertyNames.Density, "3.013 g/cm3");
            moon.BasicProperties.Add(Constants.PropertyNames.Gravity, "1.314 m/s2");
            moon.BasicProperties.Add(Constants.PropertyNames.EscapeVelocity, "2.025 m/s");
            #endregion

            #region Ganymede
            moon = new Satellite("Ganymede");
            jupiter.Add(moon);

            moon.BasicProperties.Add(Constants.PropertyNames.Description, "Third Galilean natural satellite of Jupiter.");
            moon.BasicProperties.Add(Constants.PropertyNames.Radius, "2634.1 km");
            moon.BasicProperties.Add(Constants.PropertyNames.Area, "8.72E7 km2");
            moon.BasicProperties.Add(Constants.PropertyNames.Volume, "7.66E10 km3");
            moon.BasicProperties.Add(Constants.PropertyNames.Mass, "1.4819E23 kg");
            moon.BasicProperties.Add(Constants.PropertyNames.Density, "1.936 g/cm3");
            moon.BasicProperties.Add(Constants.PropertyNames.Gravity, "1.428 m/s2");
            moon.BasicProperties.Add(Constants.PropertyNames.EscapeVelocity, "2.741 m/s");
            #endregion

            #region Callisto
            moon = new Satellite("Callisto");
            jupiter.Add(moon);

            moon.BasicProperties.Add(Constants.PropertyNames.Description, "Fourth Galilean natural satellite of Jupiter.");
            moon.BasicProperties.Add(Constants.PropertyNames.Radius, "2410.3 km");
            moon.BasicProperties.Add(Constants.PropertyNames.Area, "7.30E7 km2");
            moon.BasicProperties.Add(Constants.PropertyNames.Volume, "5.9E10 km3");
            moon.BasicProperties.Add(Constants.PropertyNames.Mass, "1.076E23 kg");
            moon.BasicProperties.Add(Constants.PropertyNames.Density, "1.8344 g/cm3");
            moon.BasicProperties.Add(Constants.PropertyNames.Gravity, "1.235 m/s2");
            moon.BasicProperties.Add(Constants.PropertyNames.EscapeVelocity, "2.440 m/s");
            #endregion

            return jupiter;
        }

        public static Planet CreateSaturn(IStellarMap map = null)
        {
            Planet s = new Planet("Saturn");

            map ??= BaseStellarMap.DefaultMap;
            map.Add(s);

            return s;
        }

        public static Planet CreateUranus(IStellarMap map = null)
        {
            Planet u = new Planet("Uranus");

            map ??= BaseStellarMap.DefaultMap;
            map.Add(u);

            return u;
        }

        public static Planet CreateNeptune(IStellarMap map = null)
        {
            Planet n = new Planet("Neptune");

            map ??= BaseStellarMap.DefaultMap;
            map.Add(n);

            return n;
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

using System.Collections.Generic;
using System.IO;

using StellarMap.Core.Bodies;
using StellarMap.Core.Types;
using StellarMap.Math.Types;

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
            
            return sol;

        }

        public static Star CreateSol(IStellarMap map = null)
        {
            Star sol = new Star("Sol");

            map ??= BaseStellarMap.DefaultMap;
            map.Add(sol);

            sol.BasicProperties.Add(Constants.PropertyNames.Designation, "Sol");
            sol.BasicProperties.Add(Constants.PropertyNames.StellarClass, "G2V");

            var catalogue = new Dictionary<string, string>();
            catalogue.Add("HabHyg", "0");
            catalogue.Add("Hip", "0");
            sol.Properties.AddGroup("Catalogue", catalogue);

            return sol;
        }

        public static Planet CreateMercury(IStellarMap map = null)
        {
            Planet m = new Planet("Mercury");

            map ??= BaseStellarMap.DefaultMap;
            map.Add(m);

            return m;
        }

        public static Planet CreateVenus(IStellarMap map = null)
        {
            Planet v = new Planet("Venus");

            map ??= BaseStellarMap.DefaultMap;
            map.Add(v);            

            return v;
        }

        public static Planet CreateEarth(IStellarMap map = null)
        {
            Planet earth = new Planet("Earth");

            map ??= BaseStellarMap.DefaultMap;
            map.Add(earth);              

            Satellite moon = new Satellite("Moon");
            earth.Add(moon);

            return earth;
        }

        public static Planet CreateMars(IStellarMap map = null)
        {
            Planet m = new Planet("Mars");

            map ??= BaseStellarMap.DefaultMap;
            map.Add(m);  

            return m;
        }

        public static Planet CreateJupiter(IStellarMap map = null)
        {
            Planet j = new Planet("Jupiter");

            map ??= BaseStellarMap.DefaultMap;
            map.Add(j);  

            return j;
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
    }
}

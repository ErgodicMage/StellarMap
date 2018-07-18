﻿using System;
using System.Collections.Generic;
using System.Text;

using StellarMap.Core.Bodies;
using StellarMap.Core.Types;
using StellarMap.Math.Types;

namespace Serialization
{
    public class SolarSystem
    {
        public static Star CreateSolSystem()
        {
            BaseStellarMap sm = new BaseStellarMap("SolarSystem");

            Star sol = new Star("Sol");
            sol.BasicProperties.Add(Star.StellarClass, "G2V");
            sm.Add(sol);

            sol.Add(new Planet("Mercury"));
            sol.Add(new Planet("Venus"));

            Planet earth = new Planet("Earth")
            {
                Map = sm
            };

            Satellite moon = new Satellite("Moon");
            earth.Add(moon);
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

            sol.GetPlanets();

            return sol;
        }

    }
}

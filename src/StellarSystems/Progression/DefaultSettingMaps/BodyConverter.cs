using System;
using System.Collections.Generic;

using StellarMap.Core.Bodies;
using StellarMap.Core.Types;
using StellarMap.Math.Types;

namespace StellarMap.Progression.DefaultSettingMaps
{
    public static class BodyConverter
    {
        public static ProgressionPlanet ConvertPlanet(ProgressionMap map, Planet oldPlanet)
        {
            ProgressionPlanet newPlanet = new ProgressionPlanet(oldPlanet.Name);
            newPlanet.Properties = oldPlanet.Properties;
            newPlanet.ParentIdentifier = string.Empty;
            newPlanet.Identifier = string.Empty;
            map.Add(newPlanet);

            IDictionary<string, Satellite> oldSatellites = oldPlanet.GetSatellites();
            if (oldSatellites != null)
            {
                foreach(Satellite oldSatellite in oldSatellites.Values)
                {
                    Satellite newSatellite = new Satellite(oldSatellite.Name);
                    newSatellite.Properties = oldSatellite.Properties;
                    newSatellite.Identifier = string.Empty;
                    newSatellite.ParentIdentifier = string.Empty;
                    newPlanet.Add(newSatellite);
                }
            }

            return newPlanet;
        }


    }
}

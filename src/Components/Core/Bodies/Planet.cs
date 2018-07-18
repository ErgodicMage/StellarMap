using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

using StellarMap.Core.Types;
using StellarMap.Math.Types;

namespace StellarMap.Core.Bodies
{
    [DataContract (Name = Constants.BodyTypes.Planet)]
    public class Planet : StellarBodywithObjects
    {
        #region Cosntructors
        public Planet() : base()
        {
        }

        public Planet(string name) : base(name)
        {
        }
        #endregion

        #region Public Properties
        [DataMember(Order = 11)]
        public GroupNamedIdentifiers PlanetGroupIdentifiers { get; set; }

        public IDictionary<string, string> Satellites { get { return PlanetGroupIdentifiers.GroupIdentifiers[Constants.NamedIdentifiers.Satellites].Identifiers; } }
        #endregion

        #region Get Functions
        public virtual Satellite GetSatellite(string name) => Get<Satellite>(name);

        public virtual IDictionary<string, Satellite> GetSatellites() => GetAll<Satellite>();
        #endregion

        #region Public Add Functions
        public void Add(Satellite satellite) => Add<Satellite>(satellite);
        #endregion

        #region Protected Functions
        protected override void Initialize()
        {
            PlanetGroupIdentifiers = new GroupNamedIdentifiers("GroupIdentifiers-Planet");
            //PlanetGroupIdentifiers.Add(GroupNamedIdentifiers.Satellites);
            base.Initialize();
        }

        protected override ObjectNamedIdentifiers GetObjectNamedIdentifiers(string name, bool create)
        {
            ObjectNamedIdentifiers identifiers = null;

            if (name == Constants.BodyTypes.Satellite)
            {
                if (PlanetGroupIdentifiers.GroupIdentifiers.ContainsKey(Constants.NamedIdentifiers.Satellites))
                    identifiers = PlanetGroupIdentifiers.GroupIdentifiers[Constants.NamedIdentifiers.Satellites];
                else if (create)
                {
                    PlanetGroupIdentifiers.Add(Constants.NamedIdentifiers.Satellites);
                    identifiers = PlanetGroupIdentifiers.GroupIdentifiers[Constants.NamedIdentifiers.Satellites];
                }
            }

            return identifiers;
        }
        #endregion
    }
}

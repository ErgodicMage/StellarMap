﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

using StellarMap.Core.Bodies;
using StellarMap.Core.Types;
using StellarMap.Math.Types;

namespace StellarMap.Progression
{
    [DataContract (Name = "ProgressionMap")]
    public class ProgressionMap : BaseStellarMap
    {
        #region Constructors
        public ProgressionMap()
        {

        }

        public ProgressionMap(string name) : base(name)
        {
        }
        #endregion

        #region Properties
        [DataMember (Order = 11)]
        public IDictionary<string, Habitat> Habitats { get; set; }

        [DataMember (Order = 12)]
        public IDictionary<string, ERBridge> Bridges { get; set; }

        [DataMember (Order = 13)]
        public IDictionary<string, StarSystem> StarSystems { get; set; }

        [DataMember (Order = 14)]
        public IDictionary<string, Cluster> Clusters { get; set; }

        [DataMember (Order = 15)]
        public IDictionary<string, Sector> Sectors { get; set; }
        #endregion

        #region Get Methods
        public virtual Habitat GetHabitat(string id) => Get<Habitat>(id);

        public virtual IDictionary<string, Habitat> GetHabitats(ICollection<string> identifiers)
        {
            IDictionary<string, Habitat> habitats = new Dictionary<string, Habitat>();

            Get<Habitat>(identifiers, habitats);

            return habitats;
        }

        public virtual ERBridge GetERBridge(string id) => Get<ERBridge>(id);

        public virtual IDictionary<string, ERBridge> GetERBridges(ICollection<string> identifiers)
        {
            IDictionary<string, ERBridge> bridges = new Dictionary<string, ERBridge>();

            Get<ERBridge>(identifiers, bridges);

            return bridges;
        }

        public virtual StarSystem GetStarSystem(string id) => Get<StarSystem>(id);

        public virtual IDictionary<string, StarSystem> GetStarSystems(ICollection<string> identifiers)
        {
            IDictionary<string, StarSystem> systems = new Dictionary<string, StarSystem>();

            Get<StarSystem>(identifiers, systems);

            return systems;
        }

        public virtual Cluster GetCluster(string id) => Get<Cluster>(id);

        public virtual IDictionary<string, Cluster> GetClusters(ICollection<string> identifiers)
        {
            IDictionary<string, Cluster> clusters = new Dictionary<string, Cluster>();

            Get<Cluster>(identifiers, clusters);

            return clusters;
        }

        public virtual Sector GetSector(string id) => Get<Sector>(id);

        public virtual IDictionary<string, Sector> GetSectors(ICollection<string> identifiers)
        {
            IDictionary<string, Sector> sectors = new Dictionary<string, Sector>();

            Get<Sector>(identifiers, sectors);

            return sectors;
        }
        #endregion

        #region Add Methods
        public virtual void Add(Habitat habitat) => Add<Habitat>(habitat);

        public virtual void Add(ICollection<Habitat> habitats) => Add<Habitat>(habitats);

        public virtual void Add(ERBridge bridge) => Add<ERBridge>(bridge);

        public virtual void Add(ICollection<ERBridge> bridges) => Add<ERBridge>(bridges);

        public virtual void Add(StarSystem system) => Add<StarSystem>(system);

        public virtual void Add(ICollection<StarSystem> systems) => Add<StarSystem>(systems);

        public virtual void Add(Cluster cluster) => Add<Cluster>(cluster);

        public virtual void Add(ICollection<Cluster> clusters) => Add<Cluster>(clusters);

        public virtual void Add(Sector sector) => Add<Sector>(sector);

        public virtual void Add(ICollection<Sector> sectors) => Add<Sector>(sectors);
        #endregion

        #region Public Methods
        public override string GenerateIdentifier<T>()
        {
            string id = string.Empty;
            Type dt = typeof(T);
            int count = 0;
            string prefix = string.Empty;

            switch (dt.Name)
            {
                case Constants.BodyTypes.Planet:
                    prefix = Constants.BodyTypes.Planet;
                    if (Planets != null)
                        count = Planets.Count;
                    break;
                case Constants.BodyTypes.Star:
                    prefix = Constants.BodyTypes.Star;
                    if (Stars != null)
                        count = Stars.Count;
                    break;
                case ProgressionConstants.BodyType.StarSystem:
                    prefix = ProgressionConstants.BodyType.StarSystem;
                    if (StarSystems != null)
                        count = StarSystems.Count;
                    break;
                case ProgressionConstants.BodyType.Cluster:
                    prefix = ProgressionConstants.BodyType.Cluster;
                    if (Clusters != null)
                        count = Clusters.Count;
                    break;
                case ProgressionConstants.BodyType.Sector:
                    prefix = ProgressionConstants.BodyType.Sector;
                    if (Sectors != null)
                        count = Sectors.Count;
                    break;
                case Constants.BodyTypes.Satellite:
                    prefix = Constants.BodyTypes.Satellite;
                    if (Satellites != null)
                        count = Satellites.Count;
                    break;
                case Constants.BodyTypes.Asteroid:
                    prefix = Constants.BodyTypes.Asteroid;
                    if (Asteroids != null)
                        count = Asteroids.Count;
                    break;
                case Constants.BodyTypes.Comet:
                    prefix = Constants.BodyTypes.Comet;
                    if (Comets != null)
                        count = Comets.Count;
                    break;
                case ProgressionConstants.BodyType.Habitat:
                    prefix = ProgressionConstants.BodyType.Habitat;
                    if (Habitats != null)
                        count = Habitats.Count;
                    break;
                case ProgressionConstants.BodyType.ERBridge:
                    prefix = ProgressionConstants.BodyType.ERBridge;
                    if (Bridges != null)
                        count = Bridges.Count;
                    break;
            }

            count++;

            StringBuilder sb = new StringBuilder();
            sb.Append(prefix);
            sb.Append("-");
            sb.Append(count.ToString("D5"));
            id = sb.ToString();

            if (string.IsNullOrEmpty(id))
                id = base.GenerateIdentifier<T>();

            return id;
        }
        #endregion

        #region Protected Methods
        protected override IDictionary<string, T> GetDictionary<T>(bool create)
        {
            IDictionary<string, T> dict = null;
            Type dt = typeof(T);

            if (dt == typeof(ProgressionStar))
            {
                if (create && Stars == null)
                    Stars = new Dictionary<string, Star>();
                dict = (IDictionary<string, T>)Stars;
            }
            else if (dt == typeof(ERBridge))
            {
                if (create && Bridges == null)
                    Bridges = new Dictionary<string, ERBridge>();
                dict = (IDictionary<string, T>)Bridges;
            }
            else if (dt == typeof(Habitat))
            {
                if (create && Habitats == null)
                    Habitats = new Dictionary<string, Habitat>();
                dict = (IDictionary<string, T>)Habitats;
            }
            else if (dt == typeof(StarSystem))
            {
                if (create && StarSystems == null)
                    StarSystems = new Dictionary<string, StarSystem>();
                dict = (IDictionary<string, T>)StarSystems;
            }
            else if (dt == typeof(Cluster))
            {
                if (create && Clusters == null)
                    Clusters = new Dictionary<string, Cluster>();
                dict = (IDictionary<string, T>)Clusters;
            }
            else if (dt == typeof(Sector))
            {
                if (create && Sectors == null)
                    Sectors = new Dictionary<string, Sector>();
                dict = (IDictionary<string, T>)Sectors;
            }

            if (dict == null)
                dict = base.GetDictionary<T>(create);

            return dict;
        }

        public override IList<string> GetBodyTypes()
        {
            IList<string> bodytypes = base.GetBodyTypes();

            bodytypes.Add(ProgressionConstants.BodyType.Habitat);
            bodytypes.Add(ProgressionConstants.BodyType.ERBridge);
            bodytypes.Add(ProgressionConstants.BodyType.StarSystem);
            bodytypes.Add(ProgressionConstants.BodyType.Cluster);
            bodytypes.Add(ProgressionConstants.BodyType.Sector);

            return bodytypes;
        }

        public override object GetBody(string bodytype)
        {
            object body = base.GetBody(bodytype);

            if (body == null)
            {
                switch (bodytype)
                {
                    case ProgressionConstants.BodyType.Habitat:
                        body = Habitats;
                        break;
                    case ProgressionConstants.BodyType.ERBridge:
                        body = Bridges;
                        break;
                    case ProgressionConstants.BodyType.StarSystem:
                        body = StarSystems;
                        break;
                    case ProgressionConstants.BodyType.Cluster:
                        body = Clusters;
                        break;
                    case ProgressionConstants.BodyType.Sector:
                        body = Sectors;
                        break;
                }
            }

            return body;
        }

        public override Type GetTypeOfBody(string bodytype)
        {
            Type t = base.GetTypeOfBody(bodytype);

            if (t == null)
            {
                switch (bodytype)
                {
                    case ProgressionConstants.BodyType.Habitat:
                        t = typeof(Dictionary<string, Habitat>);
                        break;
                    case ProgressionConstants.BodyType.ERBridge:
                        t = typeof(Dictionary<string, ERBridge>);
                        break;
                    case ProgressionConstants.BodyType.StarSystem:
                        t = typeof(Dictionary<string, StarSystem>);
                        break;
                    case ProgressionConstants.BodyType.Cluster:
                        t = typeof(Dictionary<string, Cluster>);
                        break;
                    case ProgressionConstants.BodyType.Sector:
                        t = typeof(Dictionary<string, Sector>);
                        break;
                }
            }

            return t;
        }

        public override bool SetBody(string bodytype, object data)
        {
            bool bret = base.SetBody(bodytype, data as object);

            if (!bret)
            {
                switch (bodytype)
                {
                    case ProgressionConstants.BodyType.Habitat:
                        Habitats = (Dictionary<string, Habitat>)data;
                        bret = true;
                        break;
                    case ProgressionConstants.BodyType.ERBridge:
                        Bridges = (Dictionary<string, ERBridge>)data;
                        bret = true;
                        break;
                    case ProgressionConstants.BodyType.StarSystem:
                        StarSystems = (Dictionary<string, StarSystem>)data;
                        bret = true;
                        break;
                    case ProgressionConstants.BodyType.Cluster:
                        Clusters = (Dictionary<string, Cluster>)data;
                        bret = true;
                        break;
                    case ProgressionConstants.BodyType.Sector:
                        Sectors = (Dictionary<string, Sector>)data;
                        bret = true;
                        break;
                }
            }

            return bret;
        }
        #endregion
    }
}

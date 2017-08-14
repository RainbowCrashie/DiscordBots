using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using MyLittlePony.Entities;

namespace MyLittlePony
{
    public class Equestria
    {
        private static readonly Lazy<Equestria> Lazy = new Lazy<Equestria>(() => new Equestria());
        public static Equestria EquestriaData { get; } = Lazy.Value;

        #region Fields and Properties

        internal const string AssetFilePath = "Assets/Equestria/";
        private const string DataFilePath = AssetFilePath + "MLP.xml";
        
        private static IEnumerable<Type> XmlKnownTypes { get; } = new[]
        {
            typeof(Location),
            typeof(CharacterBase),
            typeof(Pony),
            typeof(PonyGroup),
            typeof(EpisodeBase),
            typeof(MainEpisode),
            typeof(Song)
        };

        private List<Location> _locations;
        public List<Location> Locations
        {
            get => _locations ?? (_locations = new List<Location>());
            set => _locations = value;
        }

        private List<Pony> _ponies;
        public List<Pony> Ponies
        {
            get => _ponies ?? (_ponies = new List<Pony>());
            set => _ponies = value;
        }

        private List<PonyGroup> _groups;
        public List<PonyGroup> Groups
        {
            get => _groups ?? (_groups = new List<PonyGroup>());
            set => _groups = value;
        }
        
        private List<EpisodeBase> _episodes;
        public List<EpisodeBase> Episodes
        {
            get => _episodes ?? (_episodes = new List<EpisodeBase>());
            set => _episodes = value;
        }

        [IgnoreDataMember] public IEnumerable<Song> Songs => Episodes.SelectMany(ep => ep.Songs);

        #endregion

        #region Ctor
        #endregion

        #region Methods

        public void Load()
        {
            if (!File.Exists(DataFilePath))
                return;
            
            using (var fileStream = File.Open(DataFilePath, FileMode.Open))
            {
                var eq = GetSerializer().ReadObject(fileStream) as Equestria;
                if (eq == null)
                    return;

                Locations = eq.Locations;
                Ponies = eq.Ponies;
                Episodes = eq.Episodes;
            }
        }

        public void Save()
        {
            using (var fileStream = File.Open(DataFilePath, FileMode.Create))
            {
                GetSerializer().WriteObject(fileStream, this);
            }
        }

        private DataContractSerializer GetSerializer()
        {
            return new DataContractSerializer(
                GetType(),
                XmlKnownTypes,
                0x7FFF,
                false,
                true,
                null
            );
        }
        #endregion

    }
}
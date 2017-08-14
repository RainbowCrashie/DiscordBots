using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MyLittlePony.Entities
{
    public abstract class EpisodeBase : MlpEntityBase
    {
        #region Fields and Properties

        public abstract string Identifier { get; }
        
        public DateTime ReleaseDate { get; set; }
        public CharacterBase Featured { get; set; }
        private List<Pony> _characters;
        public List<Pony> Characters
        {
            get => _characters ?? (_characters = new List<Pony>());
            set => _characters = value;
        }
        private List<Location> _locations;
        public List<Location> Locations
        {
            get => _locations ?? (_locations = new List<Location>());
            set => _locations = value;
        }
        private List<Song> _songs;
        public List<Song> Songs
        {
            get => _songs ?? (_songs = new List<Song>());
            set => _songs = value;
        }

        private const string WikiUrlBase = "http://mlp.wikia.com/wiki";

        [IgnoreDataMember] public string WikiUrl => $"{WikiUrlBase}/{Name}";
        [IgnoreDataMember] public string TranscriptUrl => $"{WikiUrlBase}/Transcript/{Name}";

        #endregion

        #region Ctor
        #endregion

        #region Methods
        #endregion

    }
}
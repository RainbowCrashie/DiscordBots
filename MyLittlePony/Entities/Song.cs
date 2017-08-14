using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MyLittlePony.Entities
{
    public class Song : MlpEntityBase
    {
        #region Fields and Properties
        
        public int DurationSeconds { get; set; }
        public int BeginsAtSeconds { get; set; }
        public string YoutubeUrl { get; set; }
        public string WikiUrl { get; set; }

        [IgnoreDataMember] public string WikiUrlLyrics => WikiUrl + "#Lyrics";
        
        private List<Pony> _characters;
        public List<Pony> Characters
        {
            get => _characters ?? (_characters = new List<Pony>());
            set => _characters = value;
        }


        #endregion

        #region Ctor
        #endregion

        #region Methods

        public override string ToString()
        {
            return $"{Name} ({DurationSeconds})";
        }

        #endregion

    }
}
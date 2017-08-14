using System.Xml.Serialization;

namespace MyLittlePony.Entities
{
    public class MainEpisode : EpisodeBase
    {
        #region Fields and Properties

        public int SeasonNumber { get; set; }
        public int EpisodeNumber { get; set; }

        public override string Identifier => $"S{SeasonNumber}E{EpisodeNumber}";

        #endregion

        #region Ctor
        #endregion

        #region Methods
        public override string ToString()
        {
            return $"{Identifier}: {Name}";
        }
        #endregion

    }
}
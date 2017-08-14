using System.Collections.Generic;

namespace MyLittlePony.Entities
{
    /// <summary>
    /// CMC, shipping, 
    /// </summary>
    public class PonyGroup : CharacterBase
    {
        #region Fields and Properties

        private List<Pony> _ponies;
        public List<Pony> Ponies
        {
            get => _ponies ?? (_ponies = new List<Pony>());
            set => _ponies = value;
        }

        #endregion

        #region Ctor
        #endregion

        #region Methods
        #endregion

    }
}
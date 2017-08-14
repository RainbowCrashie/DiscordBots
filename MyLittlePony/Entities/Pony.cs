namespace MyLittlePony.Entities
{
    public class Pony : CharacterBase
    {
        #region Fields and Properties
        public Species Species { get; set; }
        public Location Habitat { get; set; }
        //something

        #endregion

        #region Ctor
        
        #endregion

        #region Methods

        public override string ToString()
        {
            return $"{Name} : {Species}";
        }

        #endregion

    }

    public enum Species
    {
        Pony,
        EarthPony,
        Pegasus,
        Unicorn,
        Alicorn,
        Seapony,
        Zebra,
        Donkey,
        Mule,
        Draconequus,
        Changeling,
        Horse,
        Breezie,
        Cow,
        Buffalo,
        Dog,
        Cat,
        Alligator,
        Rabbit,
        Owl,
        Dragon,
        Griffon,
        Phoenix,
        Insect,
        Ursa,
        Vampire,
        Giraffe,
        Monster
    }
}
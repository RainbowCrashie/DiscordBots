namespace MyLittlePony.Entities
{
    public abstract class MlpEntityBase
    {
        #region Fields and Properties

        public virtual string Name { get; set; }
        public virtual string ThumbnailUrl { get; set; }

        #endregion

        #region Ctor
        #endregion

        #region Methods
        public override string ToString()
        {
            return Name;
        }
        #endregion

    }
}
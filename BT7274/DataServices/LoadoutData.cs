using System;
using System.Collections.Generic;
using System.IO;
using BT7274.MilitiaHeadquarters;

namespace BT7274.DataServices
{
    public class LoadoutData
    {
        #region Fields and Properties

        private string LoadoutDataFilePath { get; } = "Assets/LoadoutData.xml";
        
        public PilotLoadoutList PilotLoadout { get; set; }
        public TitanLoadoutList TitanLoadout { get; set; }
        
        #endregion

        #region Ctor
        #endregion

        #region Methods

        public void Save()
        {
            new XmlSerializer<LoadoutData>().SerializeToFile(this, LoadoutDataFilePath);
            Console.WriteLine(LoadoutDataFilePath);
        }

        public void Load()
        {
            if (!File.Exists(LoadoutDataFilePath))
                return;

            var loadout = new XmlSerializer<LoadoutData>().DeserializeFromFile(LoadoutDataFilePath);
            PilotLoadout = loadout.PilotLoadout;
            TitanLoadout = loadout.TitanLoadout;
        }
        #endregion
    }

    public class PilotLoadoutList
    {
        public List<string> Tacticals { get; set; }
        public List<string> Primaries { get; set; }
        public List<string> Secondaries { get; set; }
        public List<string> Ordnances { get; set; }
        public List<string> Perks1 { get; set; }
        public List<string> Perks2 { get; set; }
        public List<string> Executions { get; set; }
        public List<string> Boosts { get; set; }
    }

    public class TitanLoadoutList
    {
        public List<TitanUniqueKitList> Titans { get; set; }
        public List<string> CommonKits { get; set; }
        public List<string> FallKits { get; set; }
    }

    public class TitanUniqueKitList
    {
        public string Name { get; set; }
        public List<string> UniqueKits { get; set; }

        public override string ToString() => Name;
    }
}
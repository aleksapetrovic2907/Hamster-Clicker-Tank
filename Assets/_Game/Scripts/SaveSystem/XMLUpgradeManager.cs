using System;
using System.Collections.Generic;
using UnityEngine;
using Aezakmi.UpgradeSystem;

namespace Aezakmi
{
    public class XMLUpgradeManager : XMLManagerBase
    {
        #region SINGLETON
        public static XMLUpgradeManager Instance;

        private void Awake()
        {
            if (Instance != this)
            {
                if (Instance != null)
                    Destroy(Instance.gameObject);
                Instance = this;
            }

            DontDestroyOnLoad(gameObject);
        }
        #endregion

        protected override string filePath => Application.persistentDataPath + "/upgrades.xml";
        protected override Type dataType => typeof(Upgrades);

        public Upgrades upgrades;

        protected override void SerializeData()
        {
            upgrades = new Upgrades();
            upgrades.upgradeLevels = new List<int>();

            for (int i = 0; i < UpgradesManager.Instance.upgrades.Count; i++)
                upgrades.upgradeLevels.Add(UpgradesManager.Instance.upgrades[i].level);

            xmlSerializer.Serialize(fileStream, upgrades);
        }

        protected override void DeserializeData()
        {
            upgrades = xmlSerializer.Deserialize(fileStream) as Upgrades;
        }

        protected override void GenerateNewData() { }
    }

    [Serializable]
    public class Upgrades
    {
        public List<int> upgradeLevels;
    }
}
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Aezakmi
{
    public class XMLInventoryManager : XMLManagerBase
    {
        #region SINGLETON
        public static XMLInventoryManager Instance;

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

        protected override string filePath => Application.persistentDataPath + "/inventory.xml";
        protected override Type dataType => typeof(InvNodes);

        public InvNodes invNodes;

        protected override void SerializeData()
        {
            invNodes = new InvNodes();
            invNodes.levels = new List<int>();

            for (int i = 0; i < InventoryManager.Instance.inventoryNodes.Count; i++)
            {
                var level = InventoryManager.Instance.inventoryNodes[i].empty ? -1 : InventoryManager.Instance.inventoryNodes[i].hamster.level;
                invNodes.levels.Add(level);
            }

            xmlSerializer.Serialize(fileStream, invNodes);
        }

        protected override void DeserializeData()
        {
            invNodes = xmlSerializer.Deserialize(fileStream) as InvNodes;
        }

        protected override void GenerateNewData()
        {
            return;
        }
    }

    [Serializable]
    public class InvNodes
    {
        public List<int> levels;
    }
}

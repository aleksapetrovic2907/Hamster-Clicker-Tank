using System;
using System.Collections.Generic;
using UnityEngine;

namespace Aezakmi
{
    public class XMLNodeBlockersManager : XMLManagerBase
    {
        #region SINGLETON
        public static XMLNodeBlockersManager Instance;

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

        protected override string filePath => Application.persistentDataPath + "/nodeBlockers.xml";
        protected override Type dataType => typeof(NodeBlockersData);

        public NodeBlockersData nodeBlockersData;

        protected override void SerializeData()
        {
            Debug.Log(this.name + " is saving data at " + filePath);
            nodeBlockersData = new NodeBlockersData();
            nodeBlockersData.blockersBought = new List<bool>();

            for (int i = 0; i < NodeBlockersManager.Instance.nodeBlockers.Count; i++)
                nodeBlockersData.blockersBought.Add(NodeBlockersManager.Instance.nodeBlockers[i] == null);

            xmlSerializer.Serialize(fileStream, nodeBlockersData);
        }

        protected override void DeserializeData() => nodeBlockersData = xmlSerializer.Deserialize(fileStream) as NodeBlockersData;

        protected override void GenerateNewData() { }
    }

    [Serializable]
    public class NodeBlockersData
    {
        public List<bool> blockersBought;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aezakmi
{
    public class XMLGameDataManager : XMLManagerBase
    {
        #region SINGLETON
        public static XMLGameDataManager Instance;

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

        protected override string filePath => Application.persistentDataPath + "/gameData.xml";
        protected override Type dataType => typeof(GameData);

        public GameData gameData;

        protected override void SerializeData()
        {
            gameData = new GameData();
            gameData.money = GameManager.Instance.money.ToString();
            gameData.hamsterCostLevel = GameManager.Instance.hamsterCostLevel;
            gameData.lastTimePlayed = DateTime.Now;

            xmlSerializer.Serialize(fileStream, gameData);
        }

        protected override void DeserializeData()
        {
            gameData = xmlSerializer.Deserialize(fileStream) as GameData;
        }

        protected override void GenerateNewData()
        {
            gameData = new GameData();
        }
    }

    [Serializable]
    public class GameData
    {
        public string money = "0";
        public string incomePerHour = "1000";
        public int hamsterCostLevel = 0;
        public DateTime lastTimePlayed;
    }
}
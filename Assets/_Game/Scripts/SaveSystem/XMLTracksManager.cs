using System;
using System.Collections.Generic;
using UnityEngine;

namespace Aezakmi
{
    public class XMLTracksManager : XMLManagerBase
    {
        #region SINGLETON
        public static XMLTracksManager Instance;

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

        protected override string filePath => Application.persistentDataPath + "/tracks.xml";
        protected override Type dataType => typeof(TracksData);

        public TracksData tracksData;

        protected override void SerializeData()
        {
            tracksData = new TracksData();
            tracksData.trackEntries = new List<TrackEntry>();

            // Fill track entries with wheels and their levels.
            for (int i = 0; i < TracksManager.Instance.tracks.Count; i++)
            {
                TrackEntry trackEntry = new TrackEntry();
                trackEntry.wheelEntries = new List<WheelEntry>();

                for (int j = 0; j < TracksManager.Instance.tracks[i].wheels.Count; j++)
                {
                    WheelEntry wheel = new WheelEntry();
                    wheel.level = TracksManager.Instance.tracks[i].wheels[j].empty ? -1 : TracksManager.Instance.tracks[i].wheels[j].hamster.level;
                    trackEntry.wheelEntries.Add(wheel);
                }

                tracksData.trackEntries.Add(trackEntry);
            }

            xmlSerializer.Serialize(fileStream, tracksData);
        }

        protected override void DeserializeData()
        {
            tracksData = xmlSerializer.Deserialize(fileStream) as TracksData;
        }

        protected override void GenerateNewData()
        {
            return;
        }
    }

    [Serializable]
    public class TracksData
    {
        public List<TrackEntry> trackEntries;
    }

    [Serializable]
    public class TrackEntry
    {
        public List<WheelEntry> wheelEntries;
    }

    [Serializable]
    public class WheelEntry
    {
        public int level;
    }
}

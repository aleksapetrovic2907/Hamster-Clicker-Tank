using System;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using NaughtyAttributes;

namespace Aezakmi
{
    public abstract class XMLManagerBase : MonoBehaviour
    {
        public bool saveOnExit = true;

        public bool fileExists { get { return File.Exists(filePath); } }
        protected abstract string filePath { get; } // The location at which the file will be saved and loaded from.
        protected abstract Type dataType { get; } // The type of the data we will serialize and deserialize.

        protected FileStream fileStream;
        protected XmlSerializer xmlSerializer;

        public void SaveToFile()
        {
            xmlSerializer = new XmlSerializer(dataType);
            fileStream = File.Create(filePath);
            SerializeData();
            fileStream.Close();
        }

        public void LoadFromFile()
        {
            if (!fileExists)
            {
                GenerateNewData();
                return;
            }

            xmlSerializer = new XmlSerializer(dataType);
            fileStream = File.Open(filePath, FileMode.Open);
            DeserializeData();
            fileStream.Close();
        }

        protected abstract void SerializeData();
        protected abstract void DeserializeData();
        protected abstract void GenerateNewData();

#if UNITY_EDITOR
        [Button]
        private void DeleteData() => File.Delete(filePath);

        [Button]
        private void OpenFolder() => Application.OpenURL(Application.persistentDataPath);

        private void OnApplicationQuit()
        {
            if (saveOnExit)
            {
                SaveToFile();
            }
        }
#endif

#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
        private void OnApplicationFocus(bool hasFocus)
        {
            if(!hasFocus && saveOnExit)
            {
                SaveToFile();
            }
        }
#endif
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

namespace Aezakmi
{
    public class TestManager : XMLManagerBase
    {
        protected override string filePath => Application.persistentDataPath + "/test.xml";
        protected override Type dataType => typeof(TestClass);

        public TestClass testClass;

        private void Start() => LoadFromFile();

        protected override void SerializeData()
        {
            xmlSerializer.Serialize(fileStream, testClass);
        }

        protected override void DeserializeData()
        {
            testClass = xmlSerializer.Deserialize(fileStream) as TestClass;
        }

        protected override void GenerateNewData()
        {
            testClass = new TestClass();

            testClass.myClasses = new List<SecondTestClass>();

            for (int i = 0; i < 3; i++)
            {
                var t = new SecondTestClass();
                t.test = UnityEngine.Random.Range(0, 5);
                testClass.myClasses.Add(t);
            }

            testClass.myInts = new List<int> { 1, 2, 3, 5 };
            testClass.myFloats = new float[3] { 1.2f, 1.3f, 3.14f };
            testClass.myBool = true;
        }
    }

    [Serializable]
    public class TestClass
    {
        public List<SecondTestClass> myClasses;
        public List<int> myInts;
        public float[] myFloats;
        public bool myBool;
    }

    [Serializable]
    public class SecondTestClass
    {
        public int test;
    }
}

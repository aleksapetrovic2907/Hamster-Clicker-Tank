using System.Collections.Generic;
using UnityEngine;
using ScottGarland.Numbers;
using NaughtyAttributes;

namespace Aezakmi.UpgradeSystem
{
    public class SpeedUpgrade : UpgradeBase
    {
        public override BigInteger Cost
        {
            get
            {
                var cost = 55.507f * Mathf.Pow(2.718f, .143877f * (level + 1)) - 62.6908f;
                return new BigInteger((int)cost * 3);
            }
        }

        [SerializeField] private List<float> speedMultiplierValues;

        public override void UpdateValue()
        {
            if (level >= speedMultiplierValues.Count) return;
            TracksManager.Instance.speedMultiplier = speedMultiplierValues[level];
            VoltmeterValuesManager.Instance.UpdateValues();
        }

        public override void RefreshUI()
        {
            base.RefreshUI();
            currentValue.text = "x" + speedMultiplierValues[level].ToString("F1");
            nextValue.text = "x" + speedMultiplierValues[level + 1].ToString("F1");
            VoltmeterValuesManager.Instance.UpdateValues();
        }

        private float m_difference = .2f;
        [Button]
        private void FillTables()
        {
            speedMultiplierValues = new List<float>();

            for (int i = 0; i < 500; i++)
            {
                speedMultiplierValues.Add(3 + i * m_difference);
            }
        }
    }
}
using System.Collections.Generic;
using UnityEngine;
using ScottGarland.Numbers;
using NaughtyAttributes;

namespace Aezakmi.UpgradeSystem
{
    public class IncomeUpgrade : UpgradeBase
    {
        public override BigInteger Cost
        {
            get
            {
                var cost = 55.507f * Mathf.Pow(2.718f, .143877f * (level + 1)) - 62.6908f;
                return new BigInteger((int)cost * 3);
            }
        }

        [SerializeField] private List<float> multiplierValues;

        public override void UpdateValue()
        {
            if (level >= multiplierValues.Count) return;
            GameManager.Instance.incomeMultiplier = multiplierValues[level];
        }

        public override void RefreshUI()
        {
            base.RefreshUI();
            currentValue.text = "x" + multiplierValues[level].ToString("F1");
            nextValue.text = "x" + multiplierValues[level + 1].ToString("F1");
        }

        // private float m_difference = .2f;
        // [Button]
        // private void FillTables()
        // {
        //     multiplierValues = new List<float>();

        //     for (int i = 0; i < 500; i++)
        //     {
        //         multiplierValues.Add(1 + i * m_difference);
        //     }
        // }
    }
}
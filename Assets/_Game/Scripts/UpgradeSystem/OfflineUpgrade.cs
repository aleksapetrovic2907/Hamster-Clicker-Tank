using System.Collections.Generic;
using UnityEngine;
using ScottGarland.Numbers;
using NaughtyAttributes;

namespace Aezakmi.UpgradeSystem
{
    public class OfflineUpgrade : UpgradeBase
    {
        public override BigInteger Cost
        {
            get
            {
                var cost = 55.507f * Mathf.Pow(2.718f, .143877f * (level + 1)) - 62.6908f;
                return new BigInteger((int)cost * 10);
            }
        }

        public OfflineUpgrade()
        {
            FillTables();
        }

        [SerializeField] private List<BigInteger> incomePerHour;

        public override void UpdateValue()
        {
            if (level >= incomePerHour.Count) return;
            GameManager.Instance.incomePerHour = incomePerHour[level];
        }

        public override void RefreshUI()
        {
            base.RefreshUI();
            currentValue.text = "$" + BigInteger.ToStringWithoutDecimals(incomePerHour[level]) + "/h";
            nextValue.text = "$" + BigInteger.ToStringWithoutDecimals(incomePerHour[level + 1]) + "/h";
        }

        [Button]
        private void FillTables()
        {
            incomePerHour = new List<BigInteger>();

            for (int i = 0; i < 500; i++)
            {
                incomePerHour.Add(new BigInteger(1000 + i * 1000));
            }
        }
    }
}
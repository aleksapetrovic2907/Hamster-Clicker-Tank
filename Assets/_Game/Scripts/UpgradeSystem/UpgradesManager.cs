using System.Collections.Generic;
using UnityEngine;

namespace Aezakmi.UpgradeSystem
{
    public partial class UpgradesManager : GloballyAccessibleBase<UpgradesManager>
    {
        public List<UpgradeBase> upgrades;

        private void Start()
        {
            ConnectButtons();

            if (XMLUpgradeManager.Instance != null && XMLUpgradeManager.Instance.fileExists)
                for (int i = 0; i < upgrades.Count; i++)
                    upgrades[i].level = XMLUpgradeManager.Instance.upgrades.upgradeLevels[i];

            SetValues();
            RefreshUI();

            GameManager.Instance.OfflineUpgradeLoaded();
        }

        public void BuyUpgrade(UpgradeBase upgrade)
        {
            GameManager.Instance.money -= upgrade.Cost;
            upgrade.Upgrade();
            RefreshUI();
            BuyHamsterUI.Instance.UpdateInteractibility();
        }

        private void SetValues()
        {
            foreach (var upgrade in upgrades)
                upgrade.UpdateValue();
        }
    }
}

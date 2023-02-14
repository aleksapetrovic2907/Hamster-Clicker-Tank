using System.Collections.Generic;
using UnityEngine;

namespace Aezakmi.UpgradeSystem
{
    public partial class UpgradesManager
    {
        [SerializeField] private GameObject shop;

        public bool ShopOpened { get { return shop.activeSelf; } }

        public void OpenShop()
        {
            shop.SetActive(true);
            RefreshUI();
        }

        public void CloseShop() => shop.SetActive(false);

        public void RefreshUI()
        {
            if (!ShopOpened) return;

            foreach (var upgrade in upgrades)
                upgrade.RefreshUI();
        }

        private void ConnectButtons()
        {
            foreach (var upgrade in upgrades)
            {
                upgrade.button.onClick.AddListener(delegate
                {
                    BuyUpgrade(upgrade);
                });
            }
        }
    }
}
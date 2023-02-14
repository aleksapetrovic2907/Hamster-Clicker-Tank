using System;
using UnityEngine;
using ScottGarland.Numbers;
using Aezakmi.Money;
using Aezakmi.UpgradeSystem;

namespace Aezakmi
{
    public class GameManager : GloballyAccessibleBase<GameManager>
    {
        public BigInteger money = new BigInteger(0);
        public int hamsterCostLevel = 0;
        public float incomeMultiplier = 1f;

        [SerializeField] private GameObject moneyVFXPrefab;

        private const float E = 2.718f;

        public BigInteger HamsterCost
        {
            get
            {
                var cost = 55.507f * Mathf.Pow(E, .143877f * (hamsterCostLevel + 1)) - 62.6908f;
                return new BigInteger((int)cost);
            }
        }

        public BigInteger incomePerHour = new BigInteger(0);

        private int m_hoursOffline = 0;

        private void Start()
        {
            if (XMLGameDataManager.Instance == null) return;

            money = new BigInteger(XMLGameDataManager.Instance.gameData.money);
            MoneyUI.Instance.UpdateUI();

            hamsterCostLevel = XMLGameDataManager.Instance.gameData.hamsterCostLevel;
            BuyHamsterUI.Instance.SetCost(HamsterCost);
            BuyHamsterUI.Instance.UpdateInteractibility();
        }

        public void OfflineUpgradeLoaded()
        {
            if (XMLGameDataManager.Instance == null || !XMLGameDataManager.Instance.fileExists) return;
            m_hoursOffline = (int)(DateTime.Now - XMLGameDataManager.Instance.gameData.lastTimePlayed).TotalHours;
            if (m_hoursOffline == 0) return;
            OfflineCanvasUI.Instance.ShowIncomeEarned(m_hoursOffline * incomePerHour);
        }
        public void OfflineEarnedMoneyClaimed()
        {
            money += m_hoursOffline * incomePerHour;
            MoneyUI.Instance.UpdateUI();
        }

        public void Clicked()
        {
            TracksManager.Instance.Accelerate();
        }

        public void CycleComplete()
        {
            foreach (Track track in TracksManager.Instance.tracks)
            {
                foreach (var wheel in track.wheels)
                {
                    if (wheel.empty) continue;

                    var moneyEarned = track.moneyMultiplier * (BigInteger.Pow(2, wheel.hamster.level) * (int)(incomeMultiplier * 10)) / 10;
                    money += moneyEarned;
                    var mVfx = Instantiate(moneyVFXPrefab, wheel.hamster.moneyVfxPosition.position, Quaternion.identity).GetComponent<MoneyVFX>();
                    mVfx.SetMoneyGained(moneyEarned);
                }
            }

            MoneyUI.Instance.UpdateUI();
            PrinterBehaviour.Instance.QueuePrint();
            BuyHamsterUI.Instance.UpdateInteractibility();
            UpgradesManager.Instance.RefreshUI();
        }

        public void BuyHamster()
        {
            money -= HamsterCost;
            hamsterCostLevel++;
            InventoryManager.Instance.BuyHamster();
            BuyHamsterUI.Instance.SetCost(HamsterCost);
            BuyHamsterUI.Instance.UpdateInteractibility();
            FreeHamsterUI.Instance.UpdateInteractibility();
            VibrationsManager.Instance.Vibrate();
        }
    }
}
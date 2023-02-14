using UnityEngine;
using TMPro;
using ScottGarland.Numbers;

namespace Aezakmi
{
    public class OfflineCanvasUI : GloballyAccessibleBase<OfflineCanvasUI>
    {
        [SerializeField] private GameObject parent;
        [SerializeField] private TextMeshProUGUI moneyEarnedText;

        public void ShowIncomeEarned(BigInteger amount)
        {
            moneyEarnedText.text = $"YOU'VE EARNED ${BigInteger.ToStringWithDecimals(amount)} WHILE YOU WERE OFFLINE.";
            parent.SetActive(true);
        }

        public void Claim()
        {
            parent.SetActive(false);
            GameManager.Instance.OfflineEarnedMoneyClaimed();
        }
    }
}

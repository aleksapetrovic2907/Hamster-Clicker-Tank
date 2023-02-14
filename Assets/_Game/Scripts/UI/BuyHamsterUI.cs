using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ScottGarland.Numbers;

namespace Aezakmi
{
    public class BuyHamsterUI : GloballyAccessibleBase<BuyHamsterUI>
    {
        [SerializeField] private Button button;
        [SerializeField] private TextMeshProUGUI costText;

        private const string COST_PREFIX = "$";

        public void Buy() => GameManager.Instance.BuyHamster();

        public void UpdateInteractibility()
        {
            bool affordable = GameManager.Instance.HamsterCost <= GameManager.Instance.money;
            button.interactable = affordable && !InventoryManager.Instance.full;
        }

        public void SetCost(BigInteger cost)
        {
            costText.text = COST_PREFIX + BigInteger.ToStringWithDecimals(cost);
        }
    }
}

using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ScottGarland.Numbers;

namespace Aezakmi.UpgradeSystem
{
    public abstract partial class UpgradeBase
    {
        public Button button;
        [SerializeField] protected TextMeshProUGUI currentValue;
        [SerializeField] protected TextMeshProUGUI nextValue;
        [SerializeField] private TextMeshProUGUI cost;

        public virtual void RefreshUI()
        {
            button.interactable = GameManager.Instance.money >= Cost;
            cost.text = "$" + BigInteger.ToStringWithDecimals(Cost);
        }
    }
}

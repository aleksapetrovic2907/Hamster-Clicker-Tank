using UnityEngine;
using TMPro;
using ScottGarland.Numbers;
using Aezakmi.Tweens;

namespace Aezakmi
{
    public class MoneyUI : GloballyAccessibleBase<MoneyUI>
    {
        [SerializeField] private TextMeshProUGUI moneyText;
        [SerializeField] private Scale scale;

        public void UpdateUI()
        {
            moneyText.text = BigInteger.ToStringWithDecimals(GameManager.Instance.money);
            scale.Rewind();
            scale.PlayTween();
        }
    }
}

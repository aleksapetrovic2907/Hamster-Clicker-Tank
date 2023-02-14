using UnityEngine;
using TMPro;
using DG.Tweening;
using ScottGarland.Numbers;

namespace Aezakmi
{
    public class MoneyVFX : MonoBehaviour
    {
        [SerializeField] private TextMeshPro text;
        [SerializeField] private float fadeDuration;

        public void SetMoneyGained(BigInteger amount)
        {
            text.text = "$" + BigInteger.ToStringWithDecimals(amount);
            text.DOColor(new Color(0, 255, 0, 0), fadeDuration).SetEase(Ease.InCirc).OnComplete(delegate { Destroy(this.gameObject); }).Play();
        }
    }
}

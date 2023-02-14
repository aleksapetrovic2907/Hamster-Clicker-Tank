using UnityEngine;
using TMPro;
using ScottGarland.Numbers;

namespace Aezakmi
{
    public class NodeBlocker : MonoBehaviour
    {
        public string cost;
        public BigInteger Cost { get { return new BigInteger(cost); } }

        [SerializeField] private TextMeshPro costText;

        private void Start() => costText.text = "$" + BigInteger.ToStringWithoutDecimals(Cost);
    }
}

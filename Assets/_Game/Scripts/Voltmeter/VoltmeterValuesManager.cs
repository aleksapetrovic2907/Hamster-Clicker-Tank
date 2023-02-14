using UnityEngine;
using TMPro;
using NaughtyAttributes;

namespace Aezakmi
{
    public class VoltmeterValuesManager : GloballyAccessibleBase<VoltmeterValuesManager>
    {
        [SerializeField] private TextMeshPro secondQuadrant;
        [SerializeField] private TextMeshPro thirdQuadrant;
        [SerializeField] private TextMeshPro fourthQuadrant;

        [Button]
        public void UpdateValues()
        {
            var mul = TracksManager.Instance.speedMultiplier;
            secondQuadrant.text = "x" + (Mathf.Lerp(1f, mul, .33f)).ToString("F1");
            thirdQuadrant.text = "x" + (Mathf.Lerp(1f, mul, .6777f)).ToString("F1");
            fourthQuadrant.text = "x" + mul.ToString("F1");
        }
    }
}

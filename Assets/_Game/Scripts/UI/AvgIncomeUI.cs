using UnityEngine;
using TMPro;
using ScottGarland.Numbers;

namespace Aezakmi
{
    public class AvgIncomeUI : MonoBehaviour
    {
        [SerializeField] private TextMeshPro text;
        [SerializeField] private float updateDelay;

        private float m_timer = 0f;

        private void Start() => UpdateAvgIncome();

        private void Update()
        {
            m_timer += Time.deltaTime;

            if (m_timer < updateDelay) return;

            m_timer -= updateDelay;
            UpdateAvgIncome();
        }

        public void UpdateAvgIncome()
        {
            BigInteger sum = 0;

            foreach (Track track in TracksManager.Instance.tracks)
            {
                foreach (var wheel in track.wheels)
                {
                    if (wheel.empty) continue;
                    sum += track.moneyMultiplier * (BigInteger.Pow(2, wheel.hamster.level) * (int)(GameManager.Instance.incomeMultiplier * 10)) / 10;
                }
            }

            var avgIncome = (sum * Mathf.CeilToInt((TracksManager.Instance.RotationsPerSecond * 100))) / 100;
            text.text = "$" + BigInteger.ToStringWithDecimals(avgIncome) + "/s";
        }
    }
}

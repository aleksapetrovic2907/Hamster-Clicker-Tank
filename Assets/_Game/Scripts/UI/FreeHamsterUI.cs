using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Aezakmi
{
    public class FreeHamsterUI : GloballyAccessibleBase<FreeHamsterUI>
    {
        public float cooldownTime = 180;

        [SerializeField] private Button button;
        [SerializeField] private TextMeshProUGUI timerText;

        private bool m_onCooldown = false;
        private float m_timer = 0f;

        private void Start() => UpdateInteractibility();

        public void UpdateInteractibility()
        {
            button.interactable = !(m_onCooldown || InventoryManager.Instance.full);
        }

        public void GetFreeHamster()
        {
            InventoryManager.Instance.BuyHamster();
            m_onCooldown = true;
            UpdateInteractibility();
            VibrationsManager.Instance.Vibrate();
        }

        private void Update()
        {
            if (!m_onCooldown) return;

            m_timer += Time.deltaTime;

            if (m_timer >= cooldownTime)
            {
                m_timer = 0f;
                m_onCooldown = false;
                UpdateInteractibility();
            }

            UpdateText();
        }

        private void UpdateText()
        {
            if (!m_onCooldown)
            {
                timerText.text = "FREE";
                return;
            }

            timerText.text = Mathf.CeilToInt((cooldownTime - m_timer)).ToString() + "s";
        }
    }
}
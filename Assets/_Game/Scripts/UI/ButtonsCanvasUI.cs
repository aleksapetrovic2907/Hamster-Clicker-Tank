using UnityEngine;

namespace Aezakmi
{
    public class ButtonsCanvasUI : MonoBehaviour
    {
        [SerializeField] private GameObject upgradeButton;
        [SerializeField] private GameObject buyButton;

        private void Start()
        {
            if (XMLGameDataManager.Instance.fileExists) return;

            upgradeButton.SetActive(false);
            buyButton.SetActive(false);
        }

        public void SetButtonsActive()
        {
            upgradeButton.SetActive(true);
            buyButton.SetActive(true);
        }
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

namespace Aezakmi
{
    public class SceneNavigator : MonoBehaviour
    {
        private void Start()
        {
            XMLTracksManager.Instance.LoadFromFile();
            XMLInventoryManager.Instance.LoadFromFile();
            XMLGameDataManager.Instance.LoadFromFile();
            XMLUpgradeManager.Instance.LoadFromFile();
            XMLNodeBlockersManager.Instance.LoadFromFile();
            SceneManager.LoadScene(1);
        }
    }
}
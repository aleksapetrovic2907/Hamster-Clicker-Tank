using UnityEngine;

namespace Aezakmi
{
    public class FPSSetter : MonoBehaviour
    {
        private void Awake() => Application.targetFrameRate = 60;
    }
}

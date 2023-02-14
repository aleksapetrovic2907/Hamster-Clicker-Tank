using UnityEngine;

namespace Aezakmi
{
    public class Node : MonoBehaviour
    {
        public Hamster hamster = null;
        public bool empty { get { return hamster == null; } }
        public GameObject indicator;

        [SerializeField] private Vector3 hamsterOffset;

        public virtual void PositionHamster()
        {
            hamster.transform.position = transform.position + hamsterOffset;
        }

        protected virtual void Start() => tag = Tags.NODE;

        public void EnableIndicator() => indicator.SetActive(true);
        public void DisableIndicator() => indicator.SetActive(false);
    }
}

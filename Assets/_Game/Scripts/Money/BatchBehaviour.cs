using System.Collections;
using UnityEngine;
using Aezakmi.Tweens;

namespace Aezakmi.Money
{
    public class BatchBehaviour : MonoBehaviour
    {
        [SerializeField] private Move move;
        [SerializeField] private new ParticleSystem particleSystem;

        private static int m_particleCount = 100;

        public void BatchFilled(float delay)
        {
            particleSystem.Emit(m_particleCount);
            StartCoroutine(MoveBatch(delay));
        }

        private IEnumerator MoveBatch(float delay)
        {
            yield return new WaitForSeconds(delay);
            move.PlayTween();
        }

        public void DestroyBatch()
        {
            PrinterBehaviour.Instance.batchAnimationActive = false;
            Destroy(gameObject);
        }
    }
}

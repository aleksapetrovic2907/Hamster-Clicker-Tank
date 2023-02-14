using UnityEngine;
using DG.Tweening;
using Aezakmi.Tweens;

namespace Aezakmi
{
    public class NeedleBehaviour : MonoBehaviour
    {
        [SerializeField] private float minRotation;
        [SerializeField] private float maxRotation;
        [SerializeField] private float lerpSpeed;
        [SerializeField] private Rotate needleRotate;
        private void Update()
        {
            // if (GearsManager.Instance.ConnectedToPipe && !needleRotate.Tweener.IsPlaying()) needleRotate.PlayTween();
            // else if (needleRotate.Tweener.IsPlaying()) needleRotate.Tweener.Pause();

            var targetAngle = new Vector3
            (
                transform.localEulerAngles.x,
                Mathf.Lerp(minRotation, maxRotation, TracksManager.Instance.ClickCountNormalized),
                transform.localEulerAngles.z
            );
            transform.localEulerAngles = Vector3.Lerp(transform.localEulerAngles, targetAngle, lerpSpeed * Time.deltaTime);
        }
    }
}

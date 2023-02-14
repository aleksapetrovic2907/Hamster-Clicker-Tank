using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using NaughtyAttributes;

namespace Aezakmi
{
    public class MoveCam : MonoBehaviour
    {
        [SerializeField] private KeyCode keyToActivate = KeyCode.A;
        [SerializeField] private Vector3 position;
        [SerializeField] private Vector3 euler;
        [SerializeField] private float duration;
        [SerializeField] private Ease ease = Ease.OutSine;
        [SerializeField] private UnityEvent eventsOnComplete;

        private bool m_activated = false;

        private void Update()
        {
            if (!Input.GetKeyDown(keyToActivate)) return;

            Activate();
        }

        [Button]
        public void Activate()
        {
            if (m_activated) return;

            m_activated = true;
            Tween moveToPos = transform.DOMove(position, duration).SetEase(ease);
            Tween rotate = transform.DORotate(euler, duration).SetEase(ease);

            Sequence moveCam = DOTween.Sequence();
            moveCam.Append(moveToPos).Join(rotate).OnComplete(delegate { eventsOnComplete.Invoke(); }).Play();
        }
    }
}

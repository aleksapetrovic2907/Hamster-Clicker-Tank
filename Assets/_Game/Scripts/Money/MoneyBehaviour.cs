using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using NaughtyAttributes;

namespace Aezakmi.Money
{
    public class MoneyBehaviour : MonoBehaviour
    {
        [SerializeField] private float moveToExitDuration;
        [SerializeField] private Ease moveToExitEase;

        [SerializeField] private float delay;

        [SerializeField] private float moveToFinalDuration;
        [SerializeField] private Ease moveToFinalEase;

        public float TotalDuration { get { return moveToExitDuration + delay + moveToFinalDuration; } }

        public void Print(Vector3 exitPrinterPosition, Vector3 finalPosition)
        {
            Tween moveToExit = transform.DOMove(exitPrinterPosition, moveToExitDuration).SetEase(moveToExitEase);
            Tween moveToFinal = transform.DOMove(finalPosition, moveToFinalDuration).SetEase(moveToFinalEase);
            Sequence print = DOTween.Sequence();
            print.Append(moveToExit).AppendInterval(delay).Append(moveToFinal).Play();
        }

        [SerializeField] private Renderer moneyRenderer;
        [Button]
        public void ShowSize()
        {
            Debug.Log(moneyRenderer.bounds.size);
        }
    }
}

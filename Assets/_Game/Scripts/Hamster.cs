using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

namespace Aezakmi
{
    public enum AnimState
    { Idle, Run, }

    public class Hamster : MonoBehaviour
    {
        public int level = 0;
        public Transform moneyVfxPosition;

        [SerializeField] private Transform hamster;
        [SerializeField] private Animator hamsterAnimator;
        [SerializeField] private Renderer hamsterRenderer;
        [SerializeField] private Vector2 minMaxRunSpeed;
        [SerializeField] private TextMeshPro levelText;
        [SerializeField] private Transform valueParent;
        [SerializeField] private List<GameObject> accessories;
        [SerializeField] private float punchScale;
        [SerializeField] private float punchDuration;
        [SerializeField] private Ease punchEase;

        private bool m_isMergeAnimationActive = false;

        public void RefreshUI()
        {
            levelText.text = (level + 1).ToString();
            UpdateSkin();
        }

        private void UpdateSkin()
        {
            Skin skin = HamsterSkinManager.Instance.GetSkin(level);
            hamsterRenderer.material.SetTexture("_BaseTexture", skin.baseTexture);
            hamsterRenderer.material.SetColor("_ShirtColor", skin.shirtColor);

            foreach (var accessory in accessories)
                accessory.SetActive(false);

            accessories[level % accessories.Count].SetActive(true);
        }

        public void DoMergeAnimation()
        {
            // transform.DOPunchScale(punchScale * Vector3.one, punchDuration, 15, .1f).SetEase(punchEase).Play();

            m_isMergeAnimationActive = true;
            UpdateAnimations(AnimState.Idle);

            Sequence moveToCamera = DOTween.Sequence();
            var originalPosition = transform.position;

            Tween moveUp = transform.DOMove(new Vector3(0.711000025f, 14.6800003f, -0.718999982f), punchDuration / 2f).SetEase(Ease.OutSine);
            Tween moveDown = transform.DOMove(originalPosition, punchDuration / 2f).SetEase(Ease.InOutSine);

            moveToCamera.Append(moveUp).AppendInterval(.75f).Append(moveDown).OnComplete(delegate
            {
                m_isMergeAnimationActive = false;
            }).Play();
        }

        public void SetDirection(Direction dir)
        {
            var scale = hamster.localScale;
            scale.y = (int)dir * Mathf.Abs(scale.y);
            hamster.localScale = scale;
        }

        public void UpdateAnimations(AnimState state)
        {
            if (state == AnimState.Run) hamsterAnimator.speed = Mathf.Lerp(minMaxRunSpeed.x, minMaxRunSpeed.y, TracksManager.Instance.ClickCountNormalized);
            else hamsterAnimator.speed = 1f;

            hamsterAnimator.SetBool("Running", (state == AnimState.Run) && !m_isMergeAnimationActive);
        }
    }
}
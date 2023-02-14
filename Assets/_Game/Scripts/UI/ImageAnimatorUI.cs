using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Aezakmi
{
    public class ImageAnimatorUI : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private List<Sprite> sprites;
        [SerializeField] private float swapDelay = .07f;

        private int m_count = 0;
        private float m_timer = 0f;

        private void Update()
        {
            m_timer += Time.deltaTime;

            if (m_timer < swapDelay) return;

            m_timer -= swapDelay;
            m_count = (m_count + 1) % sprites.Count;
            image.sprite = sprites[m_count];
        }
    }
}

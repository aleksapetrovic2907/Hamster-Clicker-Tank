using System.Collections.Generic;
using UnityEngine;

namespace Aezakmi
{
    public class Track : MonoBehaviour
    {
        public int moneyMultiplier = 1;
        public List<Wheel> wheels;
        public List<SegmentFollowPath> segments;

        public Direction direction;
        [SerializeField] private float speedModifier = .001f;

        public bool Empty
        {
            get
            {
                foreach (var wheel in wheels)
                    if (!wheel.empty) return false;

                return true;
            }
        }
        public bool Filled
        {
            get
            {
                foreach (var wheel in wheels)
                    if (wheel.empty) return false;

                return true;
            }
        }

        private void Start()
        {
            foreach (var wheel in wheels)
                wheel.rotateDirection = direction;
        }

        public void Rotate(float speed)
        {
            foreach (var segment in segments)
                segment.speed = speed * speedModifier;
        }

        public void StopMoving()
        {
            foreach (var segment in segments)
                segment.speed = 0;
        }

        public void SetHamstersDirection()
        {
            foreach (var wheel in wheels)
            {
                if (wheel.empty) continue;
                wheel.hamster.SetDirection(direction);
            }
        }
    }
}

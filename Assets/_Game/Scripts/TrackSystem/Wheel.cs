using UnityEngine;

namespace Aezakmi
{
    public enum Direction
    { CW = 1, CCW = -1 }

    public partial class Wheel : Node
    {
        public Direction rotateDirection = Direction.CW;

        [SerializeField] private Transform movingMeshTransform;

        // This is the maximum speed at which the meshes will rotate.
        // This does not impact the earnings of the gears.
        // Going faster than this @60fps will make the gears appear that they're going slower. 
        private const float MAX_SPEED = 1440f;

        private void Update()
        {
            if (empty) return;

            hamster.UpdateAnimations(AnimState.Run);
        }

        public const int EMPTY_WHEEL_LEVEL = -1;

        public void Rotate(float speed)
        {
            speed = Mathf.Clamp(speed, 0, MAX_SPEED);
            movingMeshTransform.localEulerAngles += Vector3.up * speed * (int)rotateDirection * Time.deltaTime;
        }
    }
}
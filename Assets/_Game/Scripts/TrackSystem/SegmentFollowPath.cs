using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using NaughtyAttributes;

namespace Aezakmi
{
    public class SegmentFollowPath : MonoBehaviour
    {
        public PathCreator pathCreator;
        public EndOfPathInstruction endOfPathInstruction;
        public float speed = 1;
        public Direction direction = Direction.CW;

        private float m_distanceTravelled;

        private void Start()
        {
            if (pathCreator == null) return;

            // Set distance travelled so object doesn't reset to the beginning of path.
            OnPathChanged();

            // Subscribed to the pathUpdated event so that we're notified if the path changes during the game.
            pathCreator.pathUpdated += OnPathChanged;
        }

        private void Update()
        {
            if (pathCreator == null) return;

            m_distanceTravelled += speed * (int)direction * Time.deltaTime;
            transform.position = pathCreator.path.GetPointAtDistance(m_distanceTravelled, endOfPathInstruction);
            transform.rotation = pathCreator.path.GetRotationAtDistance(m_distanceTravelled, endOfPathInstruction);
        }

        // If the path changes during the game, update the distance travelled so that the follower's position on the new path
        // is as close as possible to its position on the old path
        private void OnPathChanged()
        {
            m_distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
        }
    }
}

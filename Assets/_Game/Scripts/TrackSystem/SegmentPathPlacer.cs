using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using PathCreation.Examples;

namespace Aezakmi
{
    [ExecuteInEditMode]
    public class SegmentPathPlacer : PathSceneTool
    {
        [SerializeField] private Direction direction = Direction.CW;
        [SerializeField] private GameObject prefab;
        [SerializeField] private GameObject parent;
        [SerializeField]
        public float spacing = 3;
        [Min(0f)] public float offset = 0;

        [SerializeField] private Track track;

        const float minSpacing = .1f;

        private void Generate()
        {
            if (pathCreator != null && prefab != null && parent != null)
            {
                DestroyObjects();
                track.segments = new List<SegmentFollowPath>();

                VertexPath path = pathCreator.path;

                spacing = Mathf.Max(minSpacing, spacing);
                float dst = offset;


                while (dst < path.length)
                {
                    Vector3 point = path.GetPointAtDistance(dst);
                    Quaternion rot = path.GetRotationAtDistance(dst);
                    var go = Instantiate(prefab, point, rot, parent.transform);

                    if (go.GetComponent<SegmentFollowPath>() == null)
                    {
                        var sfp = go.AddComponent<SegmentFollowPath>();
                        sfp.pathCreator = pathCreator;
                        sfp.direction = direction;
                        track.segments.Add(sfp);
                    }

                    dst += spacing;
                }
            }
        }

        private void DestroyObjects()
        {
            int numChildren = parent.transform.childCount;
            for (int i = numChildren - 1; i >= 0; i--)
            {
                DestroyImmediate(parent.transform.GetChild(i).gameObject, false);
            }
        }

        protected override void PathUpdated()
        {
            if (pathCreator != null)
            {
                Generate();
            }
        }
    }
}

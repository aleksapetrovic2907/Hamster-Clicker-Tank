using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum Swipes
{ Up, Down, Left, Right }

namespace Aezakmi
{
    public class InputManager : GloballyAccessibleBase<InputManager>
    {
        [HideInInspector] public bool IsTouching { get { return Input.GetMouseButton(0); } }
        [HideInInspector] public bool IsClickingUI;

        [HideInInspector] public Vector2 StartPosition;
        [HideInInspector] public Vector2 CurrentPosition;
        [HideInInspector] public Vector2 EndPosition;

        [SerializeField] public Swipes? SwipeDirection = null;

        private void Update()
        {
            SwipeDirection = null;

            if (!IsTouching)
                return;

            IsClickingUI = IsPointerOverUIObject();
            DetectTouchPositions();
        }

        private void DetectTouchPositions()
        {
            CurrentPosition = Input.mousePosition;

            if (Input.GetMouseButtonDown(0))
                StartPosition = Input.mousePosition;

            if (Input.GetMouseButtonUp(0))
            {
                EndPosition = Input.mousePosition;
                DetectSwipes();
            }
        }

        private void DetectSwipes()
        {
            Vector2 swipeDirection = (EndPosition - StartPosition).normalized;

            float positiveX = Mathf.Abs(swipeDirection.x);
            float positiveY = Mathf.Abs(swipeDirection.y);

            if (positiveX > positiveY)
            {
                SwipeDirection = (swipeDirection.x > 0) ? Swipes.Right : Swipes.Left;
            }
            else
            {
                SwipeDirection = (swipeDirection.y > 0) ? Swipes.Up : Swipes.Down;
            }
        }

        private bool IsPointerOverUIObject()
        {
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
            return results.Count > 0;
        }
    }
}

using UnityEngine;

namespace Aezakmi
{
    public class DragManager : MonoBehaviour
    {
        [SerializeField] private LayerMask nodeLayer;
        [SerializeField] private LayerMask floorLayer;
        [SerializeField] private Vector3 dragOffset;

        private Camera m_mainCamera;
        private const float RAY_MAX_DISTANCE = 50f;

        private Node m_hitNode = null;
        private Node m_lastIndicatedNode = null;
        private Hamster m_hitHamster = null;

        private void Start() => m_mainCamera = Camera.main;

        private void Update()
        {
            // Grab.
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = m_mainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, RAY_MAX_DISTANCE, nodeLayer))
                {
                    if (!hit.collider.CompareTag(Tags.NODE)) return;
                    if (hit.collider.GetComponent<Node>().empty) return;

                    m_hitNode = hit.collider.GetComponent<Node>();
                    m_hitHamster = m_hitNode.hamster;
                    m_hitHamster.UpdateAnimations(AnimState.Idle);
                    m_hitNode.hamster = null;
                }

                TracksManager.Instance.UpdateState();
            }

            // Release.
            if (Input.GetMouseButtonUp(0) && m_hitHamster != null)
            {
                Ray ray = m_mainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, RAY_MAX_DISTANCE, nodeLayer))
                {
                    if (!hit.collider.CompareTag(Tags.NODE))
                    {
                        ReturnHamster();
                        ResetReferences();
                        return;
                    }

                    var newNode = hit.collider.GetComponent<Node>();

                    if (newNode.empty)
                    {
                        newNode.hamster = m_hitHamster;
                        newNode.DisableIndicator();
                        newNode.PositionHamster();
                        ResetReferences();
                    }
                    else
                    {
                        if (newNode.hamster.level == m_hitHamster.level)
                        {
                            MergeManager.Instance.Merge(newNode.hamster, m_hitHamster, newNode);
                            ResetReferences();
                        }
                        else
                        {
                            ReturnHamster();
                            ResetReferences();
                        }
                    }
                }
                else
                {
                    ReturnHamster();
                    ResetReferences();
                }

                TracksManager.Instance.UpdateState();
                FreeHamsterUI.Instance.UpdateInteractibility();
                BuyHamsterUI.Instance.UpdateInteractibility();
            }

            if (Input.GetMouseButton(0) && m_hitHamster != null)
            {
                // Drag.
                Ray ray = m_mainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, RAY_MAX_DISTANCE, floorLayer))
                {
                    var position = hit.point + dragOffset;
                    m_hitHamster.transform.position = position;
                }

                if (Physics.Raycast(ray, out hit, RAY_MAX_DISTANCE, nodeLayer))
                {
                    if (!hit.collider.CompareTag(Tags.NODE)) return;

                    var currentlyHitNode = hit.collider.GetComponent<Node>();

                    if (m_lastIndicatedNode == null) m_lastIndicatedNode = currentlyHitNode;

                    if (m_lastIndicatedNode != null && m_lastIndicatedNode != currentlyHitNode)
                    {
                        m_lastIndicatedNode.DisableIndicator();
                        m_lastIndicatedNode = currentlyHitNode;
                    }

                    if (currentlyHitNode.empty)
                        currentlyHitNode.EnableIndicator();
                }
                else if (m_lastIndicatedNode != null) m_lastIndicatedNode.DisableIndicator();
            }
        }

        private void ResetReferences()
        {
            if (m_lastIndicatedNode != null) m_lastIndicatedNode.DisableIndicator();
            m_hitHamster = null;
            m_hitNode = null;
            m_lastIndicatedNode = null;
        }

        private void ReturnHamster()
        {
            m_hitNode.hamster = m_hitHamster;
            m_hitNode.PositionHamster();
        }
    }
}
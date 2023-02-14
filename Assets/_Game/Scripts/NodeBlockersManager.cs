using System.Collections.Generic;
using UnityEngine;

namespace Aezakmi
{
    public class NodeBlockersManager : GloballyAccessibleBase<NodeBlockersManager>
    {
        public List<NodeBlocker> nodeBlockers;

        [SerializeField] private LayerMask nodeLayer;

        private Camera m_mainCamera;
        private const int RAY_MAX_DISTANCE = 50;

        private void Start()
        {
            m_mainCamera = Camera.main;

            if (XMLNodeBlockersManager.Instance == null || !XMLNodeBlockersManager.Instance.fileExists) return;

            for (int i = 0; i < XMLNodeBlockersManager.Instance.nodeBlockersData.blockersBought.Count; i++)
            {
                if (!XMLNodeBlockersManager.Instance.nodeBlockersData.blockersBought[i]) continue;
                Destroy(nodeBlockers[i].gameObject);
            }
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = m_mainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, RAY_MAX_DISTANCE, nodeLayer))
                {
                    if (!hit.collider.CompareTag(Tags.NODE_BLOCKER)) return;

                    var nodeBlocker = hit.collider.GetComponent<NodeBlocker>();

                    if (GameManager.Instance.money >= nodeBlocker.Cost)
                    {
                        GameManager.Instance.money -= nodeBlocker.Cost;
                        MoneyUI.Instance.UpdateUI();
                        // todo: add particles
                        Destroy(nodeBlocker.gameObject);
                    }
                }
            }
        }
    }
}

using System.Collections.Generic;
using UnityEngine;

namespace Aezakmi
{
    public class InventoryManager : GloballyAccessibleBase<InventoryManager>
    {
        public List<InventoryNode> inventoryNodes;
        public GameObject hamsterPrefab;

        public bool full
        {
            get
            {
                for (int i = 0; i < inventoryNodes.Count; i++)
                    if (inventoryNodes[i].empty) return false;

                return true;
            }
        }

        protected override void Awake()
        {
            base.Awake();

            if (XMLInventoryManager.Instance != null && XMLInventoryManager.Instance.fileExists)
            {
                for (int i = 0; i < inventoryNodes.Count; i++)
                {
                    var level = XMLInventoryManager.Instance.invNodes.levels[i];

                    if (level == -1) continue;

                    var hamster = Instantiate(hamsterPrefab).GetComponent<Hamster>();
                    hamster.level = level;
                    hamster.RefreshUI();

                    inventoryNodes[i].hamster = hamster;
                    inventoryNodes[i].PositionHamster();
                }
            }
        }

        public void BuyHamster(int level = 0)
        {
            var hamster = Instantiate(hamsterPrefab).GetComponent<Hamster>();
            hamster.level = level;
            hamster.RefreshUI();

            var freeNode = GetFirstFreeSlot();
            freeNode.hamster = hamster;
            freeNode.PositionHamster();
        }

        private Node GetFirstFreeSlot()
        {
            for (int i = 0; i < inventoryNodes.Count; i++)
                if (inventoryNodes[i].empty)
                    return inventoryNodes[i];

            return inventoryNodes[0];
        }
    }
}

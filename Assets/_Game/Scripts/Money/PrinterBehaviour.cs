using UnityEngine;
using NaughtyAttributes;

namespace Aezakmi.Money
{
    public class PrinterBehaviour : GloballyAccessibleBase<PrinterBehaviour>
    {
        [SerializeField] private GameObject batchPrefab;
        [SerializeField] private GameObject moneyPrefab;
        [SerializeField] private Transform moneyParent;
        [SerializeField] private Vector3 moneySize;
        [SerializeField] private float delayBetweenPrints;
        [SerializeField] private Transform instantiatePosition;
        [SerializeField] private Transform exitPrinterPosition;

        [Header("Slot Settings")]
        [SerializeField] private Transform firstSlot; // The first position where the printed money is thrown at. The other positions depend on it and money's size.
        [SerializeField] private Vector3Int dimensions;

        private float m_timer = 0f;
        private int m_printQueueCount = 0;
        private int m_currentBatchCount = 0;

        private BatchBehaviour m_currentBatch;
        public bool batchAnimationActive = false;

        private void Start()
        {
            CreateNewBatch();
        }

        private void Update()
        {
            if (m_printQueueCount == 0 || batchAnimationActive) return;

            m_timer += Time.deltaTime;

            if (m_timer >= delayBetweenPrints)
            {
                m_timer = 0;
                m_printQueueCount--;
                PrintCash();
            }
        }

        [Button]
        public void QueuePrint() => m_printQueueCount ++;

        private void PrintCash()
        {
            var money = Instantiate(moneyPrefab, instantiatePosition.position, Quaternion.identity, m_currentBatch.transform).GetComponent<MoneyBehaviour>();

            var z = (m_currentBatchCount / dimensions.x) % dimensions.y;
            var x = m_currentBatchCount % dimensions.x;
            var y = m_currentBatchCount / (dimensions.x * dimensions.y);

            var targetPosition = firstSlot.position + new Vector3(x * moneySize.x, y * moneySize.y, z * moneySize.z);

            money.Print(exitPrinterPosition.position, targetPosition);

            if (++m_currentBatchCount == (dimensions.x * dimensions.y * dimensions.z))
            {
                batchAnimationActive = true;
                m_currentBatch.BatchFilled(money.TotalDuration);
                CreateNewBatch();
                m_currentBatchCount = 0;
            }
        }

        private void CreateNewBatch()
        {
            m_currentBatch = Instantiate(batchPrefab, firstSlot.position + new Vector3(moneySize.x, 0, -moneySize.z), Quaternion.identity).GetComponent<BatchBehaviour>();
        }
    }
}

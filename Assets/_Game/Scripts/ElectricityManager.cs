using System.Collections.Generic;
using UnityEngine;

namespace Aezakmi
{
    public class ElectricityManager : MonoBehaviour
    {
        [Header("Orb Settings")]
        [SerializeField] private List<Transform> electricityOrbs;
        [SerializeField] private float maxScale;
        [SerializeField] private float orbLerpSpeed;
        private Vector3 m_targetScale = Vector3.zero;

        [Header("Coil Electricity Settings")]
        [SerializeField] private ParticleSystem coilElectricityA;
        [SerializeField] private ParticleSystem coilElectricityB;
        [SerializeField] private Vector2Int minMaxEmission;
        [SerializeField] private float coilLerpSpeed;

        private int m_targetEmission = 0;

        private void Update()
        {
            SetOrbScale();
            SetCoilElectricityEmissions();
        }

        private void SetOrbScale()
        {
            if (TracksManager.Instance.tracksRotating)
                m_targetScale = Vector3.one * Mathf.Lerp(0, maxScale, TracksManager.Instance.ClickCountNormalized);
            else
                m_targetScale = Vector3.zero;

            foreach (var orb in electricityOrbs)
                orb.localScale = Vector3.Lerp(orb.localScale, m_targetScale, orbLerpSpeed * Time.deltaTime);
        }

        private void SetCoilElectricityEmissions()
        {
#pragma warning disable 618
            if (!TracksManager.Instance.tracksRotating)
            {
                coilElectricityA.enableEmission = false;
                coilElectricityB.enableEmission = false;
                m_targetEmission = 0;
            }
            else
            {
                coilElectricityA.enableEmission = true;
                coilElectricityB.enableEmission = true;

                m_targetEmission = (int)Mathf.Lerp(minMaxEmission.x, minMaxEmission.y, TracksManager.Instance.ClickCountNormalized);
                var emRate = (int)Mathf.Lerp(coilElectricityA.emissionRate, m_targetEmission, coilLerpSpeed * Time.deltaTime);
                coilElectricityA.emissionRate = emRate;
                coilElectricityB.emissionRate = emRate;
            }
#pragma warning restore

        }
    }
}

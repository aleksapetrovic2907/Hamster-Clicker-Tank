using System.Collections.Generic;
using UnityEngine;

namespace Aezakmi
{
    public class TracksManager : GloballyAccessibleBase<TracksManager>
    {
        public List<Track> tracks;
        public Wheel turbine;
        public GameObject hamsterPrefab;

        public float TotalRotationSpeed { get { return baseSpeed * targetSpeedMultiplier; } }
        public float ClickCountNormalized { get { return clickCount / (float)maxClickCount; } }
        public float RotationsPerSecond { get { return TotalRotationSpeed / FULL_ROTATION; } }

        public bool tracksRotating = false;

        public float baseSpeed;
        public float speedMultiplier;
        public float targetSpeedMultiplier = 1f;
        public int clickCount;
        public int previousClickCount = 0;
        public int maxClickCount;
        public float multiplierDuration;
        public float accelerateLerpSpeed = 12f;

        private float m_currentRotation = 0f;
        private const float FULL_ROTATION = 360f;
        private float m_multiplierTimer = 0f;

        protected override void Awake()
        {
            base.Awake();

            if (XMLTracksManager.Instance != null && XMLTracksManager.Instance.fileExists)
            {
                var trackData = XMLTracksManager.Instance.tracksData;

                for (int i = 0; i < trackData.trackEntries.Count; i++)
                {
                    for (int j = 0; j < trackData.trackEntries[i].wheelEntries.Count; j++)
                    {
                        var level = trackData.trackEntries[i].wheelEntries[j].level;

                        if (level == -1) continue;

                        var hamster = Instantiate(hamsterPrefab).GetComponent<Hamster>();
                        hamster.level = level;
                        hamster.RefreshUI();

                        tracks[i].wheels[j].hamster = hamster;
                        tracks[i].wheels[j].PositionHamster();
                    }
                }
            }

            UpdateState();
        }

        private void Update()
        {
            Rotate();
            UpdateMultipliers();
        }

        private void UpdateMultipliers()
        {
            m_multiplierTimer += Time.deltaTime;

            if (m_multiplierTimer >= multiplierDuration)
            {
                previousClickCount = clickCount;
                m_multiplierTimer -= multiplierDuration;
                clickCount = Mathf.Clamp(--clickCount, 0, maxClickCount);
            }

            var mul = (((speedMultiplier - 1) * clickCount) / maxClickCount) + 1;
            targetSpeedMultiplier = Mathf.Lerp(targetSpeedMultiplier, mul, accelerateLerpSpeed * Time.deltaTime);
        }

        private void Rotate()
        {
            if (!tracksRotating) return;

            m_currentRotation += Time.deltaTime * TotalRotationSpeed;

            if (m_currentRotation >= FULL_ROTATION)
            {
                m_currentRotation -= FULL_ROTATION;
                GameManager.Instance.CycleComplete();
            }

            foreach (var track in tracks)
            {
                if (track.Empty)
                {
                    track.StopMoving();
                    continue;
                }

                track.Rotate(TotalRotationSpeed);

                foreach (var wheel in track.wheels)
                {
                    if (wheel.empty) continue;

                    wheel.Rotate(TotalRotationSpeed);
                }
            }

            turbine.Rotate(TotalRotationSpeed);
        }

        public void Accelerate()
        {
            if (!tracksRotating) return;

            clickCount = Mathf.Clamp(++clickCount, 0, maxClickCount);
            m_multiplierTimer = 0f;
        }

        public void UpdateState()
        {
            tracksRotating = false;

            for (int i = 0; i < tracks.Count; i++)
            {
                for (int j = 0; j < tracks[i].wheels.Count; j++)
                {
                    if (!tracks[i].wheels[j].empty)
                    {
                        tracksRotating = true;
                    }
                }
                tracks[i].SetHamstersDirection();
            }

            UpdateTracks();
        }

        private void UpdateTracks()
        {
            if (tracksRotating) return;

            foreach (var track in tracks)
                track.StopMoving();
        }
    }
}
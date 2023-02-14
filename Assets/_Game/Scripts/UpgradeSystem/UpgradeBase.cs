using UnityEngine;
using ScottGarland.Numbers;

namespace Aezakmi.UpgradeSystem
{
    public abstract partial class UpgradeBase : MonoBehaviour
    {
        public int level = 0;
        public abstract BigInteger Cost { get; }

        public virtual void Upgrade()
        {
            ++level;
            UpdateValue();
            VibrationsManager.Instance.Vibrate();
        }

        /// <summary>Updates value which the upgrade's affecting.</summary>
        public abstract void UpdateValue();
    }
}

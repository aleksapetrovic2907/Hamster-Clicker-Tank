using Lofelt.NiceVibrations;

namespace Aezakmi
{
    public class VibrationsManager : SingletonBase<VibrationsManager>
    {
        public void Vibrate() => HapticPatterns.PlayPreset(HapticPatterns.PresetType.LightImpact);
    }
}
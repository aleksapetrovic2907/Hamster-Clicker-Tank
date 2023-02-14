using UnityEngine;

namespace Aezakmi
{
    public class ClikShit : MonoBehaviour
    {
        private void Awake()
        {
#if TTP_ANALYTICS || TTP_REWARDED_INTERSTITIALS || TTP_PRIVACY_SETTINGS || TTP_APPSFLYER || TTP_REWARDED_ADS || TTP_PROMOTION || TTP_INTERSTITIALS || TTP_GAMEPROGRESSION || TTP_RATEUS || TTP_BANNERS || TTP_POPUPMGR || TTP_CRASHTOOL || TTP_OPENADS
            Tabtale.TTPlugins.TTPCore.Setup();
#endif
        }
    }
}

using UnityEngine;

namespace Aezakmi
{
    public class MergeManager : GloballyAccessibleBase<MergeManager>
    {
        [SerializeField] private GameObject mergeVFX;

        public void Merge(Hamster h1, Hamster h2, Node node)
        {
            h1.level++;
            h1.DoMergeAnimation();
            h1.RefreshUI();
            Destroy(h2.gameObject);
            var vfx = Instantiate(mergeVFX, node.transform.position, mergeVFX.transform.rotation, h1.transform);
            vfx.transform.position = node.transform.position;
            vfx.transform.localScale = mergeVFX.transform.localScale / h1.transform.localScale.x;
            VibrationsManager.Instance.Vibrate();
        }
    }
}

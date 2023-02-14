namespace Aezakmi
{
    public class InventoryNode : Node
    {
        private void Update()
        {
            if (empty) return;

            hamster.UpdateAnimations(AnimState.Idle);
        }
    }
}

using System.Linq;
using DefaultNamespace.Inventory;
using Dialogos.Services;

namespace Dialogos.ObjectsOfDialogos
{
    public class ObjCoco : ObjectBase
    {
        private static string CocoItemName = "coco";
        
        public override void OnPlayerInteraction()
        {
            base.OnPlayerInteraction();

            if (HasEnoughCocos(3))
            {
                DialogDuckAguaDeCoco.Instance.ShowMensagemCoco();
            }
        }
        
        private bool HasEnoughCocos(int requiredAmount)
        {
            var slots = SlotManager.LoadSlotData();
            var cocoSlot = slots.FirstOrDefault(slot => slot.objectName == CocoItemName);
            return cocoSlot != null && cocoSlot.amount >= requiredAmount;
        }
    }
}
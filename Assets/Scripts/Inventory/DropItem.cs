namespace DefaultNamespace.Inventory
{
    public class DropItem
    {
        public static bool Drop(string name, int quantity)
        {
            if (SlotManager.RemoveItemFromInventory(name, quantity))
            {
                SlotManager.LoadSlotData();
                return true;
            } 
            
            return false;
        }
    }
}
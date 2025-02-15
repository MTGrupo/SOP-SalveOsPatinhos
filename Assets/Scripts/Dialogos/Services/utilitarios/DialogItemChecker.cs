using System;
using System.Collections.Generic;
using DefaultNamespace.Inventory;
using Dialogos.Services.utilitarios.Interfaces;

namespace Dialogos.Services
{
    public class DialogItemChecker : IDialogItemChecker
    {
        private Dictionary<int, Func<bool>> conditions = new();
        private Dictionary<int, Action> actions = new();

        public void AddCondition(int dialogId, string itemName, int requiredAmount, Action action)
        {
            conditions[dialogId] = () => SlotManager.GetSlotDataByName(itemName)?.amount >= requiredAmount;
            actions[dialogId] = action;
        }

        public bool CheckAndExecute(int dialogId)
        {
            if (conditions.ContainsKey(dialogId) && conditions[dialogId].Invoke())
            {
                actions[dialogId]?.Invoke();
                return true;
            }
            return false;
        }
    }
}
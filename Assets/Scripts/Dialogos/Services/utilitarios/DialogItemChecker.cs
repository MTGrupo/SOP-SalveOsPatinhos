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
        private Dictionary<int, Action> falseActions = new(); 
        
        public void AddCondition(int dialogId, string itemName, int requiredAmount, Action trueAction, Action falseAction = null)
        {
            conditions[dialogId] = () => SlotManager.GetSlotDataByName(itemName)?.amount >= requiredAmount;

            actions[dialogId] = trueAction;

            if (falseAction != null)
            {
                falseActions[dialogId] = falseAction;
            }
        }
        
        public bool CheckAndExecute(int dialogId)
        {
            if (conditions.ContainsKey(dialogId) && conditions[dialogId].Invoke())
            {
                actions[dialogId]?.Invoke();
                return true;
            }
            else if (falseActions.ContainsKey(dialogId))
            {
                falseActions[dialogId]?.Invoke();
            }

            return false;
        }
    }
}
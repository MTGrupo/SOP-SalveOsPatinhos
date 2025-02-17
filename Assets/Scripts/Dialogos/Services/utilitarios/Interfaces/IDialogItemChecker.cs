using System;

namespace Dialogos.Services.utilitarios.Interfaces
{
    public interface IDialogItemChecker
    {
        bool CheckAndExecute(int dialogId);
        void AddCondition(int dialogId, string itemName, int requiredAmount, Action trueAction, Action falseAction = null);
    }
}
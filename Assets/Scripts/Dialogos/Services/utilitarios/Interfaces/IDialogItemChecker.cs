using System;

namespace Dialogos.Services.utilitarios.Interfaces
{
    public interface IDialogItemChecker
    {
        void AddCondition(int dialogId, string itemName, int requiredAmount, Action action);
        bool CheckAndExecute(int dialogId);
    }
}
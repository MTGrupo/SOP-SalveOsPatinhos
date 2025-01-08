using Assets.Scripts.Dialogos.Modal;
using Interaction;
using UnityEngine;

namespace Dialogos.Services
{
    public class StartDialogInObjects : InteractableObject, IInteraction
    {
        [SerializeField] private DialogoBase dialogoBase;
        public void OnPlayerInteraction()
        {
            dialogoBase.StartDialogo();
        }
    }
}
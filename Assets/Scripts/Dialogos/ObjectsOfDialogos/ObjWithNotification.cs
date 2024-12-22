using Assets.Scripts.Dialogos.Modal;
using UnityEngine;

namespace Dialogos.ObjectsOfDialogos
{
    public class ObjWithNotification : ObjectBase
    {
        [SerializeField] private DialogoBase dialogoBase;
        [SerializeField] private GameObject notification;
        public override void OnPlayerInteraction()
        {
            dialogoBase.NextDialogoNotShow();
            notification.SetActive(true);
            
            Invoke("HideNotification", 3f);
            
            base.OnPlayerInteraction();
        }
        void HideNotification()
        {
            notification.SetActive(false);
        }
        
    }
}
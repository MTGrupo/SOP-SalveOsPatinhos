using Assets.Scripts.Dialogos.Modal;
using UnityEngine;

namespace Dialogos.ObjectsOfDialogos
{
    public class ObjWithNotification : ObjectBase
    {
        [SerializeField] private DialogoBase dialogoBase;
        [SerializeField] private GameObject notification;
        [SerializeField] private Animator animator;
        
        public override void OnPlayerInteraction()
        {
            dialogoBase.NextDialogoNotShow();
            notification.SetActive(true);
            
            animator.SetTrigger("open");
            Invoke("HideNotification", 3f);
            
            base.OnPlayerInteraction();
        }
        void HideNotification()
        {
            animator.SetTrigger("open");
        }
        
    }
}
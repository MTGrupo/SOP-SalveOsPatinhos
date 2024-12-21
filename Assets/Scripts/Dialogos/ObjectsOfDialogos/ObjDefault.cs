using Assets.Scripts.Dialogos.Modal;
using UnityEngine;

namespace Dialogos.ObjectsOfDialogos
{
    public class ObjDefault : ObjectBase
    {
        [SerializeField] private DialogoBase dialogoBase;
        public override void OnPlayerInteraction()
        {
            dialogoBase.NextDialogoNotShow();
            base.OnPlayerInteraction();
        }
    }
}
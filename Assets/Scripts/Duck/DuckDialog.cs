using System.Collections;
using Assets.Scripts.Dialogos;
using Dialogos;
using UnityEngine;

namespace Duck
{
    public class DuckDialog : DuckBehavior
    {
        [SerializeField] private DuckDialogBase dialogoBase;
        [SerializeField] private DialogoObject dialogObject;
        [SerializeField] public GameObject iconeInteracao;
        [SerializeField] public GameObject acessorio;
        
        protected override IEnumerator Start()
        {
            yield return base.Start();
            dialogoBase.SetDialogoObject(dialogObject);
        }
        
        public override void OnPlayerInteraction()
        {
            if (IsFollowing) return;
            
            dialogoBase.StartDialogo();
        }
        
    }
}
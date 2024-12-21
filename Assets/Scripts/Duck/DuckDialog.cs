using System.Collections;
using Assets.Scripts.Dialogos.Modal;
using Dialogos;
using UnityEngine;

namespace Duck
{
    public class DuckDialog : DuckBehavior
    {
        [SerializeField] private DialogoBase dialogoBase;
        [SerializeField] private DialogoObject dialogObject;
        [SerializeField] public GameObject iconeInteracao;
        [SerializeField] public GameObject acessorio;
        
        protected override IEnumerator Start()
        {
            OnDuckRescued += AcessoriosPegos;
            
            yield return base.Start();
            dialogoBase.SetDialogoObject(dialogObject);
        }

        private void OnDestroy()
        {
            OnDuckRescued += AcessoriosPegos;
        }

        public override void OnPlayerInteraction()
        {
            if (IsFollowing) return;
            
            dialogoBase.StartDialogo();
        }

        void AcessoriosPegos()
        {
            if (IsRescued)
            {
                IsRescueded();
            }
        }

        protected virtual void IsRescueded()
        {
            if (acessorio) acessorio.SetActive(true);
            if (iconeInteracao) iconeInteracao.SetActive(false);
        }
    }
}
using Assets.Scripts.Dialogos.Modal;
using Dialogos.Enum;
using UnityEngine;
using UnityEngine.UI;

namespace Dialogos.Services
{
    public class DialogWithDecision : DialogoBase
    {
        [SerializeField] private Button btnSim;
        [SerializeField] private Button btnNao;
        
        protected override void Start()
        {
            base.Start();
            
            btnSim.gameObject.SetActive(false);
            btnNao.gameObject.SetActive(false);
        }
        
        protected override void ShowDialogo()
        {
            base.ShowDialogo();

            if (dialogoObject.GetDialogoAt(index).TipoDialogoEnum == TipoDialogoEnum.Decisao)
            {
                botaoProximo.gameObject.SetActive(false);
                btnSim.gameObject.SetActive(true);
                btnNao.gameObject.SetActive(true);
            } else {
                botaoProximo.gameObject.SetActive(true);
                btnSim.gameObject.SetActive(false);
                btnNao.gameObject.SetActive(false);
            }
        }
        
        protected override void ListenToEvents()
        {
            base.ListenToEvents();
            btnSim.onClick.AddListener(EscolherSim);
            btnNao.onClick.AddListener(EscolherNao);
        }

        void EscolherSim()
        {
            NextDialogo(); 
        }

        void EscolherNao()
        {
            OcultarDialogo();
        }
        
    }
}
using Assets.Scripts.Dialogos.Modal;
using Dialogos.Enum;
using Dialogos.ObjectsOfDialogos;
using UnityEngine;
using UnityEngine.UI;

namespace Dialogos.Services
{
    public class DialogWithChoices : DialogDuckBase
    {
        [SerializeField] private Button btnSim;
        [SerializeField] private Button btnNao;
        [SerializeField] private ObjectBase objectBase;
        
        protected override void Start()
        {
            base.Start();
            
            btnSim.gameObject.SetActive(false);
            btnNao.gameObject.SetActive(false);
        }

        protected override void ShowDialogo()
        {
            base.ShowDialogo();
            
            bool isDecision = dialogoObject.GetDialogoAt(index).TipoDialogoEnum == TipoDialogoEnum.Decisao;
            btnSim.gameObject.SetActive(isDecision);
            btnNao.gameObject.SetActive(isDecision);
            botaoProximo.gameObject.SetActive(!isDecision);
            
            if (dialogoObject.GetDialogoAt(index).TipoDialogoEnum == TipoDialogoEnum.Buscando_Itens)
            {
                objectBase.gameObject.SetActive(true);
                botaoProximo.gameObject.SetActive(false);
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
using System;
using Assets.Scripts.Dialogos.Enum;
using Quiz;
using UnityEngine;

namespace Dialogos.Services
{
    public class DialogWithQuiz : DuckDialogBase
    {
        [SerializeField] protected PainelPerguntaGUI painelPerguntaGui;
        
        protected override void Start()
        {
            base.Start();

            if (zonaDeFechar)
            {
                zonaDeFechar.gameObject.SetActive(false);
            }
        }

        public override void ShowDialogo()
        {
            base.ShowDialogo();

            if (dialogoObject.GetDialogoAt(index).TipoDialogoEnum == TipoDialogoEnum.Quiz)
            {
                dialogoPainel.SetActive(false);
                painelPerguntaGui.StartQuiz();
            }
            else
            {
                dialogoPainel.SetActive(true);
                painelPerguntaGui.painelPerguntas.SetActive(false);
            } 
            
            
        }
    }
}
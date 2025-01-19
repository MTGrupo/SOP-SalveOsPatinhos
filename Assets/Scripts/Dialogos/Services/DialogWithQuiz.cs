using Dialogos.Enum;
using Quiz;
using UnityEngine;

namespace Dialogos.Services
{
    public class DialogWithQuiz : DialogDuckBase
    {
        [SerializeField] private QuestionManager questionManager;
        [SerializeField] private PerguntaObj perguntas;
        protected override void ShowDialogo()
        {
            base.ShowDialogo();
            
            if (dialogoObject.GetDialogoAt(index).TipoDialogoEnum == TipoDialogoEnum.Quiz)
            {
                dialoguePanel.SetActive(false);
                questionManager.StartQuiz(perguntas);
                return;
            }
            
            dialoguePanel.SetActive(true);
            questionManager.DisableQuestionPanel();
        }
    }
}
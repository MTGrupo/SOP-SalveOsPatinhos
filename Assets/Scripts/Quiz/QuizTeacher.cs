using Quiz;
using UnityEngine;
using UnityEngine.Serialization;

namespace Dialogos.Services
{
    public class QuizTeacher : QuestionManager
    {
        [FormerlySerializedAs("duck")] [SerializeField] private DialogDuckBase duckBase;
        
        public int MIN_ACERTOS = 3;
        protected override void FinishedQuiz()
        {
            if (acertos >= MIN_ACERTOS)
            {
                base.FinishedQuiz();
                duckBase.NextDialogo();
                Debug.Log("Acertos: " + acertos);
            }
            else
            {
                base.FinishedQuiz();
            }
        }
    }
}
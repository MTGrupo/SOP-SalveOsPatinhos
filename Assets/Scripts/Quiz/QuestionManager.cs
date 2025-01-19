using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Quiz
{
    public class QuestionManager : MonoBehaviour
    {
        [SerializeField] public GameObject questionPanel;
        [SerializeField] private TextMeshProUGUI questionText;
        [SerializeField] private List<Button> alternative1Text;
        [SerializeField] private int limitQuestions;
        
        private int questionsAnswered = 0;
        private int currentQuestionIndex = 0;
        private PerguntaObj questionObj;
        private Pergunta currentQuestion;
        public int hits = 0;
        public bool isQuizFinished;
        public bool isHits;
        
        public void StartQuiz(PerguntaObj question)
        {
            hits = 0;
            questionObj = question;
            questionPanel.SetActive(true);
            questionsAnswered = 0;
            SetQuestion(questionObj.pergunta[currentQuestionIndex]);
        }
        
        public void DisableQuestionPanel()
        {
            questionPanel.SetActive(false);
        }

        private void SetQuestion(Pergunta question)
        {
            currentQuestion = question;
            
            questionText.text = question.pergunta;

            if (alternative1Text.Count != question.alternativas.Count) return;

            for (int i = 0; i < question.alternativas.Count; i++)
            {
                alternative1Text[i].GetComponentInChildren<TextMeshProUGUI>().text = question.alternativas[i].alternativa;
                
                alternative1Text[i].onClick.RemoveAllListeners();
                int index = i;
                alternative1Text[i].onClick.AddListener(() => ButtonClicked(index));
            }
        }

        private void ButtonClicked(int index)
        {
            if (currentQuestion == null) return;

            string selectedAnswer = currentQuestion.alternativas[index].alternativa;
            bool isCorrect = selectedAnswer == currentQuestion.respostaCorreta;

            if (isCorrect)
            {
                Right(index);
            }
            else
            {
                Wrong(index);
            }
            
            DisableButtons();
            Invoke("NextQuestion", 1f);
            
        }

        protected virtual void Right(int index)
        {
            isHits = true;
            hits++;
            StyleQuestions(alternative1Text[index], Color.green, Color.white, FontStyles.Bold);
        }
        
        void Wrong(int index)
        {
            StyleQuestions(alternative1Text[index], Color.red, Color.white, FontStyles.Bold);
        }

        private void MoveQuestionToFinalList()
        {
            Pergunta questionAnswered =  questionObj.pergunta[currentQuestionIndex];
            questionObj.pergunta.RemoveAt(currentQuestionIndex);
            questionObj.pergunta.Add(questionAnswered);
        }
        
        private void NextQuestion()
        {
            EnableButton();
            isHits = false;
            ResetStyle();
            
            MoveQuestionToFinalList();
            
            questionsAnswered++;

            if (questionsAnswered < limitQuestions)
            {
                currentQuestionIndex++;
                SetQuestion(questionObj.pergunta[currentQuestionIndex]);
            }
            else
            {
                FinishedQuiz();
            }
        }

        void DisableButtons()
        {
            foreach (var button in alternative1Text)
            {
                button.interactable = false;
            }
        }
        
        void EnableButton()
        {
            foreach (var button in alternative1Text)
            {
                button.interactable = true;
            }
        }
        
        protected virtual void FinishedQuiz()
        {
            isQuizFinished = true;
            questionPanel.SetActive(false);
        }

        void ResetStyle()
        {
            foreach (var button in alternative1Text)
            {
                StyleQuestions(button, Color.white, Color.black, FontStyles.Normal);
            }
        }
        
        private void StyleQuestions(Button button, Color backgroundColor, Color textColor, FontStyles fontStyle)
        {
            var imageComponent = button.GetComponent<Image>();
            if (imageComponent != null)
            {
                imageComponent.color = backgroundColor;
            }

            var textComponent = button.GetComponentInChildren<TextMeshProUGUI>();
            if (textComponent != null)
            {
                textComponent.color = textColor;
                textComponent.fontStyle = fontStyle;
            }
        }
    }
}
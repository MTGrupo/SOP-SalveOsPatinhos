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
        [SerializeField] private int limiteDePerguntas;
        
        private int perguntasRespondidas = 0;
        private int currentQuestionIndex = 0;
        private PerguntaObj perguntaObj;
        private Pergunta currentQuestion;
        public int acertos = 0;
        public bool isQuizFinished;
        public bool isAcerto;
        
        public void StartQuiz(PerguntaObj perguntas)
        {
            acertos = 0;
            perguntaObj = perguntas;
            questionPanel.SetActive(true);
            perguntasRespondidas = 0;
            SetQuestion(perguntaObj.pergunta[currentQuestionIndex]);
        }
        
        public void DesableQuestionPanel()
        {
            questionPanel.SetActive(false);
        }

        private void SetQuestion(Pergunta pergunta)
        {
            currentQuestion = pergunta;
            
            questionText.text = pergunta.pergunta;

            if (alternative1Text.Count != pergunta.alternativas.Count) return;

            for (int i = 0; i < pergunta.alternativas.Count; i++)
            {
                alternative1Text[i].GetComponentInChildren<TextMeshProUGUI>().text = pergunta.alternativas[i].alternativa;
                
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
                Acertou(index);
            }
            else
            {
                Errou(index);
            }
            
            DesabilitarButtons();
            Invoke("NextQuestion", 1f);
            
        }

        protected virtual void Acertou(int index)
        {
            isAcerto = true;
            acertos++;
            StyleQuestions(alternative1Text[index], Color.green, Color.white, FontStyles.Bold);
        }
        
        void Errou(int index)
        {
            StyleQuestions(alternative1Text[index], Color.red, Color.white, FontStyles.Bold);
        }

        private void MoverPerguntaParaFinal()
        {
            Pergunta perguntaRespondida = perguntaObj.pergunta[currentQuestionIndex];
            perguntaObj.pergunta.RemoveAt(currentQuestionIndex);
            perguntaObj.pergunta.Add(perguntaRespondida);
        }
        
        private void NextQuestion()
        {
            HabilitarButtons();
            isAcerto = false;
            ResetStyle();
            
            MoverPerguntaParaFinal();
            
            perguntasRespondidas++;

            if (perguntasRespondidas < limiteDePerguntas)
            {
                currentQuestionIndex++;
                SetQuestion(perguntaObj.pergunta[currentQuestionIndex]);
            }
            else
            {
                FinishedQuiz();
            }
        }

        void DesabilitarButtons()
        {
            foreach (var button in alternative1Text)
            {
                button.interactable = false;
            }
        }
        
        void HabilitarButtons()
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
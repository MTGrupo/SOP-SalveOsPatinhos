using System.Collections.Generic;
using Assets.Scripts.Dialogos.Modal;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Quiz
{
    public class PainelPerguntaGUI : MonoBehaviour
    {
        [SerializeField] public GameObject painelPerguntas;
        [SerializeField] private TextMeshProUGUI pergunta;
        [SerializeField] private List<Button> botoesAlternativas = new() { null, null, null };
        [SerializeField] private PerguntaObj perguntaObj;

        [SerializeField] private int limitePerguntas;
        [SerializeField] private int limiteAcertos;

        [SerializeField]
        private DialogoBase dialogoBase;
        
        private int perguntaIndex; 
        private int perguntasRespondidas = 0;
        private int acertos;

        private Color colorButtonGreen = Color.green;
        private Color colorButtonRed = Color.red;
        private Color colorButtonDefault = Color.white;

        public bool isFinishedQuiz = false;
        
        private void Start()
        {
            painelPerguntas.SetActive(false);
            
            for (int i = 0; i < botoesAlternativas.Count; i++)
            {
                int index = i;
                botoesAlternativas[i].onClick.AddListener(() => VerificarPerguntaCorreta(index));
            }
        }

        public void StartQuiz()
        {
            perguntaIndex = UnityEngine.Random.Range(0, perguntaObj.pergunta.Count);
            Pergunta perguntaAtual = perguntaObj.pergunta[perguntaIndex];
            
            painelPerguntas.SetActive(true);
            perguntasRespondidas = 0;
            acertos = 0;
            isFinishedQuiz = false;
            AtualizarPergunta();
        }

        public void AtualizarPergunta()
        {
            VerificarResetPerguntas();

            List<Pergunta> perguntasNaoRespondidas = perguntaObj.pergunta.FindAll(p => !p.respondida);

            if (perguntasRespondidas >= limitePerguntas || perguntasNaoRespondidas.Count == 0)
            {
                if (acertos >= limiteAcertos)
                {
                    FinalizarQuiz();
                    return;
                }

                painelPerguntas.SetActive(false);
                return;
            }

            perguntaIndex = UnityEngine.Random.Range(0, perguntasNaoRespondidas.Count);
            Pergunta perguntaAtual = perguntasNaoRespondidas[perguntaIndex];

            pergunta.text = perguntaAtual.pergunta;

            for (int i = 0; i < botoesAlternativas.Count; i++)
            {
                botoesAlternativas[i].GetComponentInChildren<TextMeshProUGUI>().text = perguntaAtual.alternativas[i].alternativa;
            }

            ResetarCoresBotoes();
            HabilitarBotoes();
        }
        
        private void VerificarResetPerguntas()
        {
            int perguntasNaoRespondidas = perguntaObj.pergunta.FindAll(p => !p.respondida).Count;

            if (perguntasNaoRespondidas < 8)
            {
                foreach (var pergunta in perguntaObj.pergunta)
                {
                    pergunta.respondida = false;
                    Debug.Log("Pergunta resetada para não respondida.");
                }
                Debug.Log("Todas as perguntas foram resetadas para não respondidas.");
            }
        }

        private void VerificarPerguntaCorreta(int alternativaSelecionada)
        {
            Pergunta perguntaAtual = perguntaObj.pergunta[perguntaIndex];
            Button buttonSelecionado = botoesAlternativas[alternativaSelecionada];

            int indiceCorreto = perguntaAtual.alternativas.FindIndex(a => a.alternativa == perguntaAtual.respostaCorreta);

            if (indiceCorreto != -1)
            {
                if (alternativaSelecionada == indiceCorreto)
                {
                    acertos++;
                    StyleButton(buttonSelecionado, colorButtonGreen, Color.white, FontStyles.Bold);
                }
                else
                {
                    StyleButton(buttonSelecionado, colorButtonRed, Color.white, FontStyles.Bold);
                    Button buttonCorreto = botoesAlternativas[indiceCorreto];
                    StyleButton(buttonCorreto, colorButtonGreen, Color.white, FontStyles.Bold);
                }

                perguntaAtual.respondida = true;
                perguntasRespondidas++;
                DesabilitarBotoes();
                Invoke(nameof(ProximaPergunta), 1.5f);
            }
            else
            {
                Debug.LogError("Resposta correta não encontrada!");
            }
        }

        private void ProximaPergunta()
        {
            perguntaIndex++;
            AtualizarPergunta();
        }

        private void FinalizarQuiz()
        {
            dialogoBase.NextDialogo();
            painelPerguntas.SetActive(false);
            isFinishedQuiz = true;
            Debug.Log("Quiz finalizado!");
        }

        private void StyleButton(Button button, Color color, Color textColor, FontStyles fontStyle)
        {
            button.GetComponent<Image>().color = color;
            var textComponent = button.GetComponentInChildren<TextMeshProUGUI>();
            textComponent.color = textColor;
            textComponent.fontStyle = fontStyle;
        }

        private void ResetarCoresBotoes()
        {
            foreach (var botao in botoesAlternativas)
            {
                botao.GetComponent<Image>().color = colorButtonDefault;
                var textComponent = botao.GetComponentInChildren<TextMeshProUGUI>();
                textComponent.color = Color.black;
                textComponent.fontStyle = FontStyles.Normal;
            }
        }

        private void DesabilitarBotoes()
        {
            foreach (var botao in botoesAlternativas)
            {
                botao.interactable = false;
            }
        }

        private void HabilitarBotoes()
        {
            foreach (var botao in botoesAlternativas)
            {
                botao.interactable = true;
            }
        }
    }
}

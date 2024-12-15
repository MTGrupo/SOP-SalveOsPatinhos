using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Quiz
{
    public class PainelPerguntaGUI : MonoBehaviour
    {
        [SerializeField] private GameObject painelPerguntas;
        [SerializeField] private TextMeshProUGUI pergunta;
        [SerializeField] private List<Button> botoesAlternativas = new() { null, null, null };
        [SerializeField] private PerguntaObj perguntaObj;
        [SerializeField] private int maxPerguntas;
        
        private int perguntasRespondidas = 0;
        
        private Queue<Pergunta> filaPerguntas = new();

        private Color colorButtonGreen = Color.green;
        private Color colorButtonRed = Color.red;
        private Color colorButtonDefault = Color.white;

        
        private void Start()
        {
            perguntasRespondidas = 0;
            foreach (var perguntaItem in perguntaObj.pergunta)
            {
                filaPerguntas.Enqueue(perguntaItem);
            }

            for (int i = 0; i < botoesAlternativas.Count; i++)
            {
                int index = i;
                botoesAlternativas[i].onClick.AddListener(() => VerificarPerguntaCorreta(index));
            }

            AtualizarPergunta();
        }

        public void AtualizarPergunta()
        {
            if (filaPerguntas.Count == 0)
            {
                painelPerguntas.SetActive(false);
                return;
            }

            Pergunta perguntaAtual = filaPerguntas.Peek();

            pergunta.text = perguntaAtual.pergunta;

            for (int i = 0; i < botoesAlternativas.Count; i++)
            {
                var alternativa = perguntaAtual.alternativas[i];
                botoesAlternativas[i].GetComponentInChildren<TextMeshProUGUI>().text = alternativa.alternativa;
            }
        }

        public void ProximaPergunta()
        {
            if (perguntasRespondidas >= maxPerguntas)
            {
                painelPerguntas.SetActive(false);
                return;
            }
            
            if (filaPerguntas.Count > 0)
            {
                Pergunta perguntaAtual = filaPerguntas.Dequeue();
                filaPerguntas.Enqueue(perguntaAtual);
            }

            AtualizarPergunta();
            ResetarCoresBotoes();
        }

        private void VerificarPerguntaCorreta(int alternativaSelecionada)
        {
            perguntasRespondidas++;
            
            Pergunta perguntaAtual = filaPerguntas.Peek();
            Button buttonSelecionado = botoesAlternativas[alternativaSelecionada];

            perguntaAtual.respondida = true;

            int indiceCorreto = perguntaAtual.alternativas.FindIndex(a => a.alternativa == perguntaAtual.respostaCorreta);

            if (alternativaSelecionada == indiceCorreto)
            {
                StyleButton(buttonSelecionado, colorButtonGreen, Color.white, FontStyles.Bold);
            }
            else
            {
                StyleButton(buttonSelecionado, colorButtonRed, Color.white, FontStyles.Bold);
                
                Button buttonCorreto = botoesAlternativas[indiceCorreto];
                StyleButton(buttonCorreto, colorButtonGreen, Color.white, FontStyles.Bold);
            }

            Invoke(nameof(ProximaPergunta), 1.5f);
        }

        private void StyleButton(Button button, Color color, Color textColor, FontStyles fontStyle)
        {
            button.GetComponent<Image>().color = color;
            button.GetComponentInChildren<TextMeshProUGUI>().color = textColor;
            button.GetComponentInChildren<TextMeshProUGUI>().fontStyle = fontStyle;
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
    }
}

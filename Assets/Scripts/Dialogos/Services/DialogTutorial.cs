using System.Collections;
using Actors;
using Assets.Scripts.Dialogos.Modal;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Dialogos.Services
{
    public class DialogTutorial : DialogoBase
    {
        [SerializeField] private GameObject changerScene;
        [SerializeField] private TextMeshProUGUI textTimeLoadScene;
        [SerializeField] private Button buttonLoadScene;
        [SerializeField] private GameObject duck;
        [SerializeField] private Button buttonMensagemInit;
        [SerializeField] private GraphicBehaviour graphicBehaviour;
        
        private string playerName;

        private const int ID_INTRO = 1;
        private const int ID_MOVE = 2;
        private const int ID_INTERACT = 3;
        private const int ID_CONGRATS = 4;

        protected override void Start()
        {
            graphicBehaviour.IsMoving = false;
            
            if (buttonLoadScene)
                buttonLoadScene.gameObject.SetActive(false);
            
            if (changerScene)
                changerScene.SetActive(false);
            
            if (duck)
                duck.SetActive(false);

            if (buttonMensagemInit)
            {
                buttonMensagemInit.onClick.AddListener(() =>
                {
                    buttonMensagemInit.gameObject.SetActive(false);
                });
            }
            
            buttonLoadScene.onClick.AddListener(ClickLoadScene);
            
            base.Start();
        }

        void Awake()
        {
            playerName = PlayerPrefs.GetString("playerName");
            StartDialogo();
        }

        protected override void ShowDialogo()
        {
            base.ShowDialogo();

            string deviceType = PlayerPrefs.GetString("DeviceType");

            if (deviceType.Equals("Pc"))
            {
                ShowPcDialog();
            }
            else if (deviceType.Equals("Mobile"))
            {
                ShowMobileDialog();
            }
        }

        private void ShowPcDialog()
        {
            switch (dialogoObject.GetDialogoAt(index).id)
            {
                case ID_INTRO:
                    ShowIntro();
                    break;
                case ID_MOVE:
                    texto.text = "Para se movimentar, utilize as teclas W, A, S e D.";
                    break;
                case ID_INTERACT:
                    ShowInteract("Para interagir com os patinhos, basta se aproximar deles e pressionar a tecla ESPAÇO.");
                    break;
                case ID_CONGRATS:
                    ShowTextCongrats();
                    break;
            }
        }

        private void ShowMobileDialog()
        {
            switch (dialogoObject.GetDialogoAt(index).id)
            {
                case ID_INTRO:
                    ShowIntro();
                    break;
                case ID_MOVE:
                    texto.text = "Para se mover, você pode usar o joystick no canto inferior esquerdo da tela.";
                    break;
                case ID_INTERACT:
                    ShowInteract("Para interagir com os patinhos, basta clicar no botão no canto inferior direito da tela");
                    break;
                case ID_CONGRATS:
                    ShowTextCongrats();
                    break;
            }
        }
        
        private void ShowInteract(string typeInteract)
        {
            texto.text = $"{typeInteract}";
            botaoProximo.gameObject.SetActive(false);
            duck.SetActive(true);
        }
        
        private void ShowIntro()
        {
            texto.text =
                $"Olá {playerName}. Eu sou o Sr. Pato, e estou aqui para te ensinar como salvar os patinhos! pergunta";
        }

        private void ShowTextCongrats()
        {
            texto.text = $"Parabéns {playerName}! Você salvou o patinho. Agora, vamos para o próximo nível!";
            buttonLoadScene.gameObject.SetActive(true);
            changerScene.SetActive(true);
            ButtonLoadScene();
        }

        private void ButtonLoadScene()
        {
            buttonLoadScene.gameObject.SetActive(true);

            if (textTimeLoadScene)
            {
                StartCoroutine(CountdownAndLoadScene(8));
            }
        }
        
        private void ClickLoadScene()
        {
            GameManager.LoadGame();
        }
        
        private IEnumerator CountdownAndLoadScene(int seconds)
        {
            int remainingSeconds = seconds;

            while (remainingSeconds > 0)
            {
                if (textTimeLoadScene)
                {
                    textTimeLoadScene.text = $"Carregando cena em {remainingSeconds} segundos...";
                }

                yield return new WaitForSeconds(1);
                remainingSeconds--;
            }

            if (textTimeLoadScene)
            {
                textTimeLoadScene.text = "Carregando cena...";
            }
            
            GameManager.LoadGame();
        }
    }
}

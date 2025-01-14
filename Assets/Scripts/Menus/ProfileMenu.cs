using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Menus
{
    public class ProfileMenu : MenuBehaviour
    {
        [SerializeField] Transform content;
        [SerializeField] TextMeshProUGUI playerDisplayName;
        [SerializeField] TMP_InputField nameInputField;
        [SerializeField] Button changeNameButton;
        [SerializeField] Button backButton;
        
        protected override bool IsOpen
        {
            get => content.gameObject.activeInHierarchy;
            set => content.gameObject.SetActive(value);
        }
        
        private void OnNameChangeSubmit()
        {
            string playerName = nameInputField.text;

            if (!string.IsNullOrEmpty(playerName))
            {
                PlayerProfile.Instance.SetPlayerName(playerName);
            }
        }

        void UpdateDisplayName(string name)
        {
            playerDisplayName.text = name;
            ClearInput();
        }
        
        void ClearInput()
        {
            nameInputField.text = string.Empty;
        }

        void Back()
        {
            switch (GameManager.CurrentState)
            {
                case GameState.Menu:
                    ShowMainMenu();
                    return;
                default:
                    Debug.Log("Provavelmente falta configurar algo.", this);
                    break;
            }
        }

        private void Start()
        {
            PlayerProfile.Instance.OnPlayerNameChanged += UpdateDisplayName;
        }

        void Awake()
        {
            ProfileMenu = this;
            Close();
			
            backButton.onClick.AddListener(Back);
            changeNameButton.onClick.AddListener(OnNameChangeSubmit);    
        }
    }
}
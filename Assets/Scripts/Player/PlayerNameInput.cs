using System;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Dialogos
{
    public class PlayerNameInput : MonoBehaviour
    {
        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private Button confirmButton;

        void Start()
        {
            confirmButton.onClick.AddListener(OnSubmit);
        }

        private void OnSubmit()
        {
            string namePlayer = inputField.text.Trim();

            if (!string.IsNullOrEmpty(namePlayer))
            {
                PlayerProfile.Instance.SetPlayerName(namePlayer);
                PlayerPrefs.SetString("playerName", namePlayer);
            }
            else
            {
                string generatedDefaultPlayerName = PlayerNameGenarator.GetPlayerName(PlayerProfile.Instance.PlayerName);
                PlayerProfile.Instance.SetPlayerName(generatedDefaultPlayerName);
                PlayerPrefs.SetString("playerName", generatedDefaultPlayerName);
            }
        }
    }
}
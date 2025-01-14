using PlayerAccount;
using UnityEngine;
using UnityEngine.UI;

namespace Menus
{
	public class MainMenu : MenuBehaviour
	{
		[Header("Componentes")]
		[SerializeField]
		Button profileButton;
		[SerializeField]
		Button continueButton;
		[SerializeField]
		Button newGameButton;
		[SerializeField]
		Button leaderboardButton;
		[SerializeField]
		Button optionsButton;
		[SerializeField]
		Button exitButton;
		[SerializeField]
		GameObject loginPainel;

		void Continue()
			=> ButtonInvoke(() => GameManager.LoadGame(true));

		void NewGame()
			=> ButtonInvoke(() => GameManager.LoadIntro());

		void Profile()
		{
			ShowProfile();
		}
		
		void Leaderboard()
		{
			if (!AuthenticationManager.Instance.IsAuthenticated)
			{
				loginPainel.SetActive(true);
				return;
			}
			
			ShowLeaderboard();
		}
		
		void Options()
		{
			ShowSettings();
		}
		
		void Exit()
			=> ButtonInvoke(GameManager.Exit);

		void Awake()
		{
			MainMenu = this;
			profileButton.onClick.AddListener(Profile);
			continueButton.onClick.AddListener(Continue);
			newGameButton.onClick.AddListener(NewGame);
			leaderboardButton.onClick.AddListener(Leaderboard);
			optionsButton.onClick.AddListener(Options);
			exitButton.onClick.AddListener(Exit);
		}

		void Start()
		{
			profileButton.interactable = ProfileMenu;
			leaderboardButton.interactable = LeaderboardMenu;
			optionsButton.interactable = SettingsMenu;
			continueButton.gameObject.SetActive(GameManager.HasGameData);
		}

#if UNITY_EDITOR
		void Reset()
		{
			var buttons = GetComponentsInChildren<Button>(true);
			
			profileButton = buttons[0];
			continueButton = buttons[1];
			newGameButton = buttons[2];
			leaderboardButton = buttons[3];
			optionsButton = buttons[4];
			exitButton = buttons[5];
		}
#endif
	}
}
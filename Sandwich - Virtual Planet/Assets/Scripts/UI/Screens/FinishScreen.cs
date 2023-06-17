using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FinishScreen : UIScreen
{
    private const string REPLAY_ANIMATION_KEY = "replay";

    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private Button mainMenuButton;
    [SerializeField]
    private Button replayButton;

    private void Awake()
    {
        replayButton.onClick.AddListener(OnReplayButtonClicked);
        mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
    }

    protected override void EnableButtons()
    {
        base.EnableButtons();
        replayButton.enabled = true;
        mainMenuButton.enabled = true;
    }

    protected override void DisableButtons()
    {
        base.DisableButtons();
        replayButton.enabled = false;
        mainMenuButton.enabled = false;
    }
    
    public void SetUp()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("Score: ");
        sb.Append(ScoreManager.instance.CurrentScore);
        scoreText.text = sb.ToString();
    }

    private void OnReplayButtonClicked()
    {
        animator.SetTrigger(REPLAY_ANIMATION_KEY);
    }
    private void OnMainMenuButtonClicked()
    {
        animator.SetTrigger(MAIN_MENU_ANIMATION_KEY);
    }

    
    //Called on animation
    private void ReplayGame()
    {
        GameManager.instance.StartGame();
    }
    protected override void DoHideAnimation()
    {
        OnMainMenuButtonClicked();
    }

}

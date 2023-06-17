using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScreen : UIScreen
{
    private const string PLAY_GAME_ANIMATION_TIRGGER = "play";

    [SerializeField]
    private GameObject mainMenuContainer;
    [SerializeField]
    private GameObject highestScoreContainer;
    [SerializeField]
    private TextMeshProUGUI highestScoreText;
    [SerializeField]
    private GameObject optionsContainer;

    [SerializeField]
    private Button backButton;
    [SerializeField]
    private Button playButton;
    [SerializeField]
    private Button quitButton;
    [SerializeField]
    private Button optionsButton;

    private void Awake()
    {
        quitButton.onClick.AddListener(Quit);
        playButton.onClick.AddListener(OnPlayButtonClicked);
        optionsButton.onClick.AddListener(OnOptionsButtonClicked);
        backButton.onClick.AddListener(OnBackButtonClicked);
    }

    public override void OnBeforeShow()
    {
        TrySetNewHighscore();
    }

    private void Start()
    {
        EnableButtons();
        SetHighScoreContainer();
    }

    private void SetHighScoreContainer()
    {
        int highScore = ScoreManager.instance.HighScore;
        if (highScore > 0)
        {
            highestScoreContainer.SetActive(true);
            highestScoreText.text = highScore.ToString();
        }
        else
        {
            highestScoreContainer.SetActive(false);
        }
    }

    private void TrySetNewHighscore()
    {
        int currentScore = ScoreManager.instance.CurrentScore;
        int highScore = ScoreManager.instance.HighScore;
        if (currentScore >= highScore)
        {
            highestScoreContainer.SetActive(true);
            highestScoreText.text = currentScore.ToString();
        }
        else
        {
            SetHighScoreContainer();
        }
    }


    private void OnPlayButtonClicked()
    {
        animator.SetTrigger(PLAY_GAME_ANIMATION_TIRGGER);
    }

    //called on animation
    private void StartGame()
    {
        AudioManager.instance.StopBGMMusic();
        GameManager.instance.StartGame();
    }

    private void OnOptionsButtonClicked()
    {
        optionsContainer.SetActive(true);
        mainMenuContainer.SetActive(false);
    }

    private void OnBackButtonClicked()
    {
        optionsContainer.SetActive(false);
        mainMenuContainer.SetActive(true);
    }

    protected override void EnableButtons()
    {
        base.EnableButtons();
        playButton.enabled = true;
        optionsButton.enabled = true;
        AudioManager.instance.PlayMenuMusic();
    }

    private void Quit()
    {
        Application.Quit();
    }

}

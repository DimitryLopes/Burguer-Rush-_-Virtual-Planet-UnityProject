using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private const string HIGH_SCORE_SAVE_KEY = "high_score";

    [SerializeField]
    private FloatingText floatingScoreText;
    [SerializeField]
    private TextMeshProUGUI scoreText;

    [SerializeField, Space, Header("Balancing")]
    private int scoreForFinishingBurguer;
    [SerializeField]
    private int scoreForDroppingIngredient;
    [SerializeField]
    private int scoreForFinishingIncompleteBurguer;
    [SerializeField]
    private float minimumIngredientScore;

    public static ScoreManager instance;
    private int currentScore;
    private int highScore;

    public int HighScore => highScore;
    public int CurrentScore => currentScore;

    private void Awake()
    {
        instance = this;
        highScore = PlayerPrefs.GetInt(HIGH_SCORE_SAVE_KEY, 0);
    }

    public void CalculateScore(bool ingredientsMatch, float distance)
    {
        float scoredPoints = minimumIngredientScore / (distance + 1);
        scoredPoints *= 10;
        Debug.Log("Scored: " + scoredPoints);

        if (ingredientsMatch)
        {
            AddScore(Mathf.FloorToInt(scoredPoints));
        }
        else
        {
            AddScore(- Mathf.FloorToInt(scoredPoints) / 2);
        }
    }

    public void RemoveScoreForDroppingIngredient()
    {
        AddScore(scoreForDroppingIngredient);
    }

    private void AddScore(int score)
    {
        currentScore += score;
        floatingScoreText.Text = score.ToString();
        floatingScoreText.DoAnimation();
        UpdateScore();
    }

    public void OnFinishCompleteBurguer()
    {
        AddScore(scoreForFinishingBurguer);
    }

    public void OnFinishIncompleteBurguer()
    {
        AddScore(scoreForFinishingIncompleteBurguer);
    }

    public void RestartCurrentScore()
    {
        currentScore = 0;
        UpdateScore();
    }

    private void UpdateScore()
    {
        scoreText.text = currentScore.ToString();
    }

    public void Save()
    {
        if(currentScore > highScore)
        {
            PlayerPrefs.SetInt(HIGH_SCORE_SAVE_KEY, currentScore);
        }
    }
}

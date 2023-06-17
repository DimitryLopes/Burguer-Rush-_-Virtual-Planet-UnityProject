using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    private const string TIMER_TEXT_FORMAT = "{0:0}:{1:00}";

    [SerializeField]
    private Slider timerSlider;
    [SerializeField]
    private TextMeshProUGUI timerText;
    [SerializeField]
    private float maxTime;

    private float currentTimer;
    private bool isTimerRunning;

    private GameManager gameManager;

    public static TimeManager instance;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        gameManager = GameManager.instance;
    }


    void Update()
    {
        if (isTimerRunning)
        {
            currentTimer -= Time.deltaTime;
            timerSlider.value = (currentTimer / maxTime);
            if(currentTimer <= 0)
            {
                ResetTimer();
                gameManager.FinishSession();
            }
            UpdateTimerText();
        }
    }

    private void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(currentTimer / 60F);
        int seconds = Mathf.FloorToInt(currentTimer - minutes * 60);

        timerText.text = string.Format(TIMER_TEXT_FORMAT, minutes, seconds);
    }

    public void StartTimer()
    {
        ResetTimer();
        isTimerRunning = true;
    }

    public void ResetTimer()
    {
        isTimerRunning = false;
        currentTimer = maxTime;
    }

    public void Pause()
    {
        isTimerRunning = false;
    }

    public void Resume()
    {
        isTimerRunning = true;
    }
}

using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class CountdownScreen : MonoBehaviour
{
    private const string COUNTDOWN_FORMAT = "{0:0}";
        
    [SerializeField]
    private int maxTime;

    [SerializeField]
    private TextMeshProUGUI countdownText;

    public void StartCountDown(Action callback)
    {
        gameObject.SetActive(true);
        StartCoroutine(Coundown(callback));
    }

    private void FinishCountdown(Action callback)
    {
        callback?.Invoke();
        gameObject.SetActive(false);
    }

    private IEnumerator Coundown(Action callback)
    {
        float currentTime = maxTime + 0.49f;
        do
        {
            currentTime -= Time.deltaTime;
            countdownText.text = string.Format(COUNTDOWN_FORMAT, currentTime);
            yield return null;
        } while (currentTime > 0.55f);

        FinishCountdown(callback);
    }
}

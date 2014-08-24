using UnityEngine;
using System.Collections;
using UnityEngine.SocialPlatforms.Impl;

public class EndingAnimation : MonoBehaviour {

    public ScoreManager scoreManager;
    public TimeMeter timeMeter;
    public Ball ball;

    public UnityEngine.UI.Image Blackness;
    [Range(0.1f, 5f)]
    public float BlacknessDuration = 0.5f;

    public UnityEngine.UI.Text ScoreText;
    public UnityEngine.UI.Text HighscoreText;
    [Range(0.1f, 5f)]
    public float ScoreTextDuration = 1f;

    public UnityEngine.UI.Text RetryText;
    [Range(0.1f, 5f)]
    public float RetryTextDuration = 0.5f;

    bool canRetry;
    string originalScoreText;
    string originalHighscoreText;

    public void Initialize() {
        Blackness.fillClockwise = false;
        Blackness.fillAmount = 0;
        scoreManager.SaveScores();
        var finalScoreText = originalScoreText ?? (originalScoreText = ScoreText.text);
        finalScoreText = finalScoreText.Replace("%t", scoreManager.TimeAlive.ToString("0.00"));
        finalScoreText = finalScoreText.Replace("%w", scoreManager.WorldsEncountered.ToString("0"));
        ScoreText.text = finalScoreText;
        var finalHighscoreText = originalHighscoreText ?? (originalHighscoreText = HighscoreText.text);
        finalHighscoreText = finalHighscoreText.Replace("%t", PlayerPrefs.GetFloat("hightime", 0).ToString("0.00"));
        finalHighscoreText = finalHighscoreText.Replace("%w", PlayerPrefs.GetInt("highworlds", 0).ToString("0"));
        HighscoreText.text = finalHighscoreText;
        {
            var newColor = ScoreText.color;
            newColor.a = 0;
            ScoreText.color = newColor;
            HighscoreText.color = newColor;
        }
        {
            var newColor = RetryText.color;
            newColor.a = 0;
            RetryText.color = newColor;
        }
        StartCoroutine(AnimateBlackness());
    }

    void Update() {
        if (ball.rigidbody2D.velocity != Vector2.zero)
            ball.Reset();
        if (canRetry && Input.GetMouseButtonDown(0)) {
            canRetry = false;
            FindObjectOfType<Board>().InitializeBoard();
            StartCoroutine(AnimateBlacknessOut());
        }
    }

    IEnumerator AnimateBlackness() {
        while (Blackness.fillAmount < 1) {
            Blackness.fillAmount += Time.deltaTime / BlacknessDuration;
            yield return new WaitForEndOfFrame();
        }
        {
            Blackness.fillAmount = 1;
            timeMeter.enabled = false;
            timeMeter.gameObject.SetActive(false);
            FindObjectOfType<Ball>().Reset();
            StartCoroutine(AnimateScoreText());
        }
    }

    IEnumerator AnimateScoreText() {
        while (ScoreText.color.a < 1) {
            var newColor = ScoreText.color;
            newColor.a += Time.deltaTime / ScoreTextDuration;
            ScoreText.color = newColor;
            HighscoreText.color = newColor;
            yield return new WaitForEndOfFrame();
        }
        {
            var newColor = ScoreText.color;
            newColor.a = 1;
            ScoreText.color = newColor;
            StartCoroutine(AnimateRetryText());
        }
    }

    IEnumerator AnimateRetryText() {
        canRetry = true;
        while (RetryText.color.a < 1) {
            var newColor = RetryText.color;
            newColor.a += Time.deltaTime / RetryTextDuration;
            RetryText.color = newColor;
            yield return new WaitForEndOfFrame();
        }
        {
            var newColor = RetryText.color;
            newColor.a = 1;
            RetryText.color = newColor;
        }
    }

    IEnumerator AnimateBlacknessOut() {
        {
            var newColor = ScoreText.color;
            newColor.a = 0;
            ScoreText.color = newColor;
        }
        {
            var newColor = RetryText.color;
            newColor.a = 0;
            RetryText.color = newColor;
        }
        Blackness.fillClockwise = true;
        while (Blackness.fillAmount > 0) {
            Blackness.fillAmount -= Time.deltaTime / BlacknessDuration;
            yield return new WaitForEndOfFrame();
        }
        {
            Blackness.fillAmount = 0;
            gameObject.SetActive(false);
            timeMeter.enabled = true;
            timeMeter.gameObject.SetActive(true);
            FindObjectOfType<TimeMeter>().StartIntro();
        }
    }
}
﻿using UnityEngine;
using System.Collections;
using UnityEngine.SocialPlatforms.Impl;

public class EndingAnimation : MonoBehaviour {

    public AudioManager audioManager;
    public ScoreManager scoreManager;
    public TimeMeter timeMeter;
    public Board board;
    public Rotatable rotatable;
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

    Canvas canvas;
    bool canRetry;
    string originalScoreText;
    string originalHighscoreText;

    void Start() {
        canvas = GetComponent<Canvas>();
        Deactivate();
    }

    public void Activate() {
        canvas.planeDistance = 1;
        enabled = true;
    }

    public void Deactivate() {
        canvas.planeDistance = 99999; // using this method to hide the canvas prevents terrible spike in font-related CPU processing when showing canvas
        enabled = false;
    }

    public void Initialize() {
        rotatable.RotationEnabled = false;
        board.GameStarted = false;
        audioManager.PlayGameOverSound();
        scoreManager.SaveScores();

        Blackness.fillClockwise = false;
        Blackness.fillAmount = 0;
        var finalScoreText = originalScoreText ?? (originalScoreText = ScoreText.text);
        finalScoreText = finalScoreText.Replace("%t", scoreManager.TimeAlive.ToString("0.00"));
        finalScoreText = finalScoreText.Replace("%w", scoreManager.WorldsEncountered.ToString("0"));
        ScoreText.text = finalScoreText;
        var finalHighscoreText = originalHighscoreText ?? (originalHighscoreText = HighscoreText.text);
        finalHighscoreText = finalHighscoreText.Replace("%t", PlayerPrefs.GetFloat("hightime", 0).ToString("0.00"));
        finalHighscoreText = finalHighscoreText.Replace("%w", PlayerPrefs.GetInt("highworlds", 0).ToString("0"));
        HighscoreText.text = finalHighscoreText;
        ScoreText.color = ScoreText.color.withAlpha(0);
        HighscoreText.color = HighscoreText.color.withAlpha(0);
        RetryText.color = RetryText.color.withAlpha(0);
        StartCoroutine(AnimateBlackness());
    }

    void Update() {
        if (ball.GetComponent<Rigidbody2D>().velocity != Vector2.zero)
            ball.Reset();
        if (canRetry && Input.GetMouseButtonDown(0)) {
            canRetry = false;
            board.InitializeBoard();
            StartCoroutine(AnimateBlacknessOut());
        }
    }

    IEnumerator AnimateBlackness() {
        // this toggling of the blackness is required so that it actually renders, because Unity is dumb
        {
            Blackness.enabled = false;
            yield return new WaitForEndOfFrame();
            Blackness.enabled = true;
        }
        while (Blackness.fillAmount < 1) {
            Blackness.fillAmount += Time.deltaTime / BlacknessDuration;
            yield return new WaitForEndOfFrame();
        }
        Blackness.fillAmount = 1;
        timeMeter.enabled = false;
        timeMeter.gameObject.SetActive(false);
        ball.Reset();
        StartCoroutine(AnimateScoreText());
    }

    IEnumerator AnimateScoreText() {
        while (ScoreText.color.a < 1) {
            ScoreText.color = ScoreText.color.withAlpha(ScoreText.color.a + Time.deltaTime / ScoreTextDuration);
            HighscoreText.color = HighscoreText.color.withAlpha(HighscoreText.color.a + Time.deltaTime / ScoreTextDuration);
            yield return new WaitForEndOfFrame();
        }
        ScoreText.color = ScoreText.color.withAlpha(1);
        StartCoroutine(AnimateRetryText());
    }

    IEnumerator AnimateRetryText() {
        canRetry = true;
        while (RetryText.color.a < 1) {
            RetryText.color = RetryText.color.withAlpha(RetryText.color.a + Time.deltaTime / RetryTextDuration);
            yield return new WaitForEndOfFrame();
        }
        RetryText.color = RetryText.color.withAlpha(1);
    }

    IEnumerator AnimateBlacknessOut() {
        ScoreText.color = ScoreText.color.withAlpha(0);
        HighscoreText.color = HighscoreText.color.withAlpha(0);
        RetryText.color = RetryText.color.withAlpha(0);
        Blackness.fillClockwise = true;
        while (Blackness.fillAmount > 0) {
            Blackness.fillAmount -= Time.deltaTime / BlacknessDuration;
            yield return new WaitForEndOfFrame();
        }
        Blackness.fillAmount = 0;
        Deactivate();
        timeMeter.enabled = true;
        timeMeter.gameObject.SetActive(true);
        rotatable.RotationEnabled = true;
        board.GameStarted = true;
        timeMeter.StartIntro();
    }
}
using UnityEngine;
using System.Collections;

public class Swapper : MonoBehaviour {

    public Board Board;
    [Range(1, 10)]
    public int LevelFlashCount = 3;
    [Range(0.01f, 1f)]
    public float LevelFlashInterval = 0.125f;

    AudioManager audioManager;
    ScoreManager scoreManager;
    SpriteRenderer spriteRenderer;

    Color originalColor;
    public bool AnimatingHue;
    public float AnimateHueSpeed = 1.5f;
    [Range(0, 1)]
    public float AnimateHueSaturation = 0.5f;
    float currentHue;

	void Start() {
	    if (!Board) {
	        Board = FindObjectOfType<Board>();
        }
        audioManager = FindObjectOfType<AudioManager>();
        scoreManager = FindObjectOfType<ScoreManager>();
	    spriteRenderer = GetComponent<SpriteRenderer>();
	    originalColor = spriteRenderer.color;
	}
	
	void Update() {
	    if (AnimatingHue) {
            currentHue = (currentHue + AnimateHueSpeed * Time.deltaTime) % 1f;
            spriteRenderer.color = new HSBColor(currentHue, AnimateHueSaturation, 1, 1).ToColor();
	    } else {
	        spriteRenderer.color = originalColor;
	    }
	}

    void OnTriggerEnter2D(Collider2D other) {
        var ball = other.gameObject.GetComponent<Ball>();
        if (ball) {
            if (Board.GameStarted) {
                StartCoroutine(SwapLevelAnimation(ball, LevelFlashCount, LevelFlashInterval));
            }
        }
    }

    IEnumerator SwapLevelAnimation(Ball ball, int levelFlashCount, float levelFlashInterval) {
        ball.Stop();
        AnimatingHue = true;
        audioManager.PlaySwapperSound();;

        var newBallPosition = transform.position;
        newBallPosition.z = ball.transform.position.z;
        ball.transform.position = newBallPosition;

        var activeLevelIndex = Mathf.FloorToInt(Random.value * Board.ActiveLevels.Length);
        var activeLevelToSwapOut = Board.ActiveLevels[activeLevelIndex];

        var inactiveLevelIndex = Mathf.FloorToInt(Random.value * Board.InactiveLevels.Count);
        var inactiveLevelToSwapIn = Board.InactiveLevels[inactiveLevelIndex];
        inactiveLevelToSwapIn.Reset();
        inactiveLevelToSwapIn.transform.localRotation = Quaternion.Euler(0, 0, 90 * activeLevelIndex);

        for (int i = 0; i < levelFlashCount; i++) {
            activeLevelToSwapOut.gameObject.SetActive(true);
            inactiveLevelToSwapIn.gameObject.SetActive(false);
            yield return new WaitForSeconds(levelFlashInterval);
            activeLevelToSwapOut.gameObject.SetActive(false);
            inactiveLevelToSwapIn.gameObject.SetActive(true);
            yield return new WaitForSeconds(levelFlashInterval);
        }

        Board.ActiveLevels[activeLevelIndex] = inactiveLevelToSwapIn;
        Board.InactiveLevels.RemoveAt(inactiveLevelIndex);
        Board.InactiveLevels.Add(activeLevelToSwapOut);

        scoreManager.WorldsEncountered++;
        AnimatingHue = false;
        if (Board.GameStarted) {
            ball.Move();
        }
    }
}

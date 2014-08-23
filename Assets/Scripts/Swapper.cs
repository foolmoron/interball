using UnityEngine;
using System.Collections;

public class Swapper : MonoBehaviour {

    public Board Board;
    [Range(1, 10)]
    public int LevelFlashCount = 3;
    [Range(0.01f, 1f)]
    public float LevelFlashInterval = 0.125f;

	void Start() {
	    if (!Board) {
	        Board = FindObjectOfType<Board>();
	    }
	}
	
	void Update() {
	}

    void OnTriggerEnter2D(Collider2D other) {
        var ball = other.gameObject.GetComponent<Ball>();
        if (ball) {
            StartCoroutine(SwapLevelAnimation(ball, LevelFlashCount, LevelFlashInterval));
        }
    }

    IEnumerator SwapLevelAnimation(Ball ball, int levelFlashCount, float levelFlashInterval) {
        ball.Stop();
        var newBallPosition = transform.position;
        newBallPosition.z = ball.transform.position.z;
        ball.transform.position = newBallPosition;

        var activeLevelIndex = Mathf.FloorToInt(Random.value * Board.ActiveLevels.Length);
        var activeLevelToSwapOut = Board.ActiveLevels[activeLevelIndex];

        var inactiveLevelIndex = Mathf.FloorToInt(Random.value * Board.InactiveLevels.Count);
        var inactiveLevelToSwapIn = Board.InactiveLevels[inactiveLevelIndex];
        inactiveLevelToSwapIn.LevelColor = new Color(Random.value, Random.value, Random.value);
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

        ball.Move();
    }
}

using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class Board : MonoBehaviour {

    public string[] LevelsToLoad;

    public Level[] Levels { get; private set; }
    public Level[] ActiveLevels { get; private set; }
    public List<Level> InactiveLevels { get; private set; }

    bool canStartGame;

    void Start() {
        StartCoroutine(LoadAllLevels(LevelsToLoad));
    }

    void Update() {
        if (canStartGame && Input.GetMouseButtonDown(0)) { // wait until player grabs board to start game
            FindObjectOfType<TimeManager>().CountingDown = true;
            FindObjectOfType<Ball>().Move();
            var arrows = FindObjectsOfType<IntroArrow>();
            for (int i = 0; i < arrows.Length; i++) {
                Destroy(arrows[i].gameObject);
            }
            canStartGame = false;
        }
    }

    void InitializeBoard() {
        if (Levels.Length < 4)
            Debug.LogError("Need at least 4 levels to make game work!");

        InactiveLevels = new List<Level>(Levels);
        ActiveLevels = new Level[4];
        for (int i = 0; i < 4; i++) {
            var randomIndex = Mathf.FloorToInt(Random.value * InactiveLevels.Count);
            var level = InactiveLevels[randomIndex];
            level.gameObject.SetActive(true);
            level.Reset();
            level.transform.localRotation = Quaternion.Euler(0, 0, 90 * i);
            ActiveLevels[i] = level;
            InactiveLevels.RemoveAt(randomIndex);
        }

        canStartGame = true;
        GetComponent<Rotatable>().RotationEnabled = true;
        FindObjectOfType<TimeMeter>().StartIntro();
    }

    IEnumerator LoadAllLevels(string[] sceneNames) {
        for (int i = 0; i < sceneNames.Length; i++) {
            var sceneName = sceneNames[i];
            Application.LoadLevelAdditive(sceneName);
        }
        yield return new WaitForEndOfFrame(); // wait until next frame for levels to be initialized

        Levels = FindObjectsOfType<Level>();
        for (int i = 0; i < Levels.Length; i++) {
            Levels[i].transform.parent = transform;
            Levels[i].gameObject.SetActive(false);
        }
        InitializeBoard();
    }
}

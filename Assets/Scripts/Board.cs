using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class Board : MonoBehaviour {

    public string[] LevelsToLoad;

    Level[] levels;
    List<Level> activeLevels;
    List<Level> inactiveLevels;

    void Start() {
        StartCoroutine(LoadAllLevels(LevelsToLoad));
    }

    void InitializeBoard() {
        if (levels.Length < 4)
            Debug.LogError("Need at least 4 levels to make game work!");

        inactiveLevels = new List<Level>(levels);
        activeLevels = new List<Level>();
        for (int i = 0; i < 4; i++) {
            var randomIndex = Mathf.FloorToInt(Random.value * inactiveLevels.Count);
            var level = inactiveLevels[randomIndex];
            level.gameObject.SetActive(true);
            level.LevelColor = new Color(Random.value, Random.value, Random.value);
            level.transform.localRotation = Quaternion.Euler(0, 0, 90 * i);
            activeLevels.Add(level);
            inactiveLevels.RemoveAt(randomIndex);
        }
    }

    IEnumerator LoadAllLevels(string[] sceneNames) {
        for (int i = 0; i < sceneNames.Length; i++) {
            var sceneName = sceneNames[i];
            Application.LoadLevelAdditive(sceneName);
        }
        yield return new WaitForEndOfFrame(); // wait until next frame for levels to be initialized

        levels = FindObjectsOfType<Level>();
        for (int i = 0; i < levels.Length; i++) {
            levels[i].transform.parent = transform;
            levels[i].gameObject.SetActive(false);
        }
        InitializeBoard();
    }
	
	void Update() {
	}
}

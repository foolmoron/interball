using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class Level : MonoBehaviour {

    [Range(0, 1)]
    public float LevelAlpha = 0.25f;
    public Color LevelColor;
    Color oldLevelColor = Color.clear;

	void Start() {
	}
	
	void Update() {
	    if (LevelColor != oldLevelColor) {
	        var allSprites = GetComponentsInChildren<SpriteRenderer>();
	        for (int i = 0; i < allSprites.Length; i++) {
                allSprites[i].color = LevelColor.withAlpha(LevelAlpha);
	        }

	        oldLevelColor = LevelColor;
	    }
	}
}

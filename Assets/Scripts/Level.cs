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

    public void Reset() {
        gameObject.SetActive(true); // required to get components in children
        LevelColor = new Color(Random.value, Random.value, Random.value);
        var allOrbs = GetComponentsInChildren<TimeOrb>();
        for (int i = 0; i < allOrbs.Length; i++) {
            allOrbs[i].Reset();
        }
    }
	
	void Update() {
	    if (LevelColor != oldLevelColor) {
	        var allSprites = GetComponentsInChildren<SpriteRenderer>();
	        for (int i = 0; i < allSprites.Length; i++) {
	            if (allSprites[i].GetComponent<StaticColor>()) // totally ignore sprites with this component on them
	                continue;

                allSprites[i].color = LevelColor.withAlpha(LevelAlpha);
	        }

	        oldLevelColor = LevelColor;
	    }
	}
}

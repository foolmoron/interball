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
        LevelColor = new HSBColor(Random.value, 1, 0.784f, 1).ToColor(); // 0.784f is the value that allows all hues to be visible with white dots
        var allOrbs = GetComponentsInChildren<TimeOrb>();
        for (int i = 0; i < allOrbs.Length; i++) {
            allOrbs[i].Reset();
        }
        var allBumpersGhosts = GetComponentsInChildren<BumperGhost>();
        for (int i = 0; i < allBumpersGhosts.Length; i++) {
            Destroy(allBumpersGhosts[i].gameObject);
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

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BumperGhost : MonoBehaviour {

    [Range(0.01f, 5f)]
    public float GhostDuration = 0.25f;
    [Range(0f, 3f)]
    public float GhostMaxExtraScale = 0.5f;
    [Range(0f, 1f)]
    public float GhostOriginalAlpha = 0.2f;

    SpriteRenderer ghostSprite;
    float ghostTime;
    float ghostInterp;
    float originalScaleX;
    float originalScaleY;

    void Start() {
        ghostSprite = GetComponent<SpriteRenderer>();
        originalScaleX = transform.localScale.x;
        originalScaleY = transform.localScale.y;
    }

    void Update() {
        if (ghostInterp < 1) {
            ghostTime += Time.deltaTime;
            ghostInterp = ghostTime / GhostDuration;
            var extraScale = Mathf.Lerp(0, GhostMaxExtraScale, ghostInterp);
            var alpha = Mathf.Lerp(GhostOriginalAlpha, 0, ghostInterp);

            ghostSprite.transform.localScale = new Vector3(originalScaleX + extraScale, originalScaleY + extraScale, 1);
            ghostSprite.color = ghostSprite.color.withAlpha(alpha);
        } else {
            Destroy(gameObject);
        }
	}
}

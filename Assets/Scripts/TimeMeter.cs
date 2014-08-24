using UnityEngine;
using System.Collections;

public class TimeMeter : MonoBehaviour {

    TimeManager timeManager;
	UnityEngine.UI.Image image;

    [Range(0, 1)]
    public float MaxHue;
    [Range(0, 1)]
    public float MinHue;
    [Range(0, 1)]
    public float Saturation;
    [Range(0, 1)]
    public float Brightness;

    [Range(0.1f, 10f)]
    public float IntroDuration = 3f;
    float introTime;
    bool introAnimating;

    [Range(0.01f, 1f)]
    public float FlashDuration = 0.5f;
    [Range(0.9f, 1f)]
    public float MinScale = 0.97f;
    float flashTime;
    bool flashAnimating;

	void Start() {
	    timeManager = FindObjectOfType<TimeManager>();
		image = GetComponent<UnityEngine.UI.Image>();
	}

    public void StartIntro() {
        introAnimating = true;
        introTime = 0f;
        flashAnimating = false;
        flashTime = 0;
    }

    public void Flash() {
        flashAnimating = true;
        flashTime = 0;
    }

    void Update() {
        var finalSaturation = Saturation;

        if (flashAnimating) {
            flashTime += Time.deltaTime;
            var flashPercentage = flashTime / FlashDuration;
            finalSaturation = Mathf.Lerp(1, Saturation, flashPercentage);

            var finalScale = Mathf.Lerp(MinScale, 1, flashPercentage);
            transform.localScale = new Vector3(finalScale, finalScale, 1);

            if (flashPercentage > 1) {
                flashAnimating = false;
            }
        }

        if (!introAnimating && timeManager.CurrentTimePercentage > 0) {
            image.fillAmount = timeManager.CurrentTimePercentage;
            image.color = new HSBColor(Mathf.Lerp(MinHue, MaxHue, timeManager.CurrentTimePercentage), 1, Brightness, 1).ToColor().withAlpha(finalSaturation);
        } else {
            introTime += Time.deltaTime;
            var introPercentage = introTime / IntroDuration;
            if (introPercentage < timeManager.CurrentTimePercentage && introPercentage < 1) {
                image.fillAmount = introPercentage;
                image.color = new HSBColor(Mathf.Lerp(MinHue, MaxHue, introPercentage), 1, Brightness, 1).ToColor().withAlpha(finalSaturation);
            } else {
                introAnimating = false;
            }
        }
    }
}
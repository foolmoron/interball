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

	void Start() {
	    timeManager = FindObjectOfType<TimeManager>();
		image = GetComponent<UnityEngine.UI.Image>();
	}

    public void StartIntro() {
        introAnimating = true;
        introTime = 0f;
    }

    void Update() {
        if (!introAnimating && timeManager.CurrentTimePercentage > 0) {
            image.fillAmount = timeManager.CurrentTimePercentage;
            image.color = new HSBColor(Mathf.Lerp(MinHue, MaxHue, timeManager.CurrentTimePercentage), Saturation, Brightness, 1).ToColor();
        } else {
            introTime += Time.deltaTime;
            var introPercentage = introTime / IntroDuration;
            if (introPercentage < timeManager.CurrentTimePercentage && introPercentage < 1) {
                image.fillAmount = introPercentage;
                image.color = new HSBColor(Mathf.Lerp(MinHue, MaxHue, introPercentage), Saturation, Brightness, 1).ToColor();
            } else {
                introAnimating = false;
            }
        }
    }
}
using System.Net.Mime;
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
    bool introAnimating = true;

	void Start() {
	    timeManager = FindObjectOfType<TimeManager>();
		image = GetComponent<UnityEngine.UI.Image>();
	}

    public void StartIntro() {
        StartCoroutine(IntroAnimation());
    }

    void Update() {
        if (!introAnimating) {
            image.fillAmount = timeManager.CurrentTimePercentage;
            image.color = new HSBColor(Mathf.Lerp(MinHue, MaxHue, timeManager.CurrentTimePercentage), Saturation, Brightness, 1).ToColor();
        }
    }

    IEnumerator IntroAnimation() { 
        var introPercentage = 0f;
        var introTime = 0f;
        while (introPercentage < timeManager.CurrentTimePercentage && introPercentage < 1) {
            introTime += Time.deltaTime;
            introPercentage = introTime / IntroDuration;
            image.fillAmount = introPercentage;
            image.color = new HSBColor(Mathf.Lerp(MinHue, MaxHue, introPercentage), Saturation, Brightness, 1).ToColor();
            yield return new WaitForEndOfFrame();
        }
        introAnimating = false;
    }
}
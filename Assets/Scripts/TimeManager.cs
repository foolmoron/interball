using UnityEngine;
using System.Collections;

public class TimeManager : MonoBehaviour {

    [Range(1, 100)]
    public float MaxTimeLeft = 20f;
    [Range(0, 100)]
    public float CurrentTimeLeft;
    public bool CountingDown;
    public EndingAnimation EndingAnimation;

    public float CurrentTimePercentage { get { return CurrentTimeLeft / MaxTimeLeft; } }
    
	void Start() {
        Reset();
	}

    public void Reset() {
        CurrentTimeLeft = MaxTimeLeft;
        CountingDown = false;
    }
	
	void Update() {
	    if (CountingDown) {
	        if (CurrentTimeLeft > MaxTimeLeft) {
	            CurrentTimeLeft = MaxTimeLeft;
	        }

	        if (CurrentTimeLeft > Time.deltaTime) {
	            CurrentTimeLeft -= Time.deltaTime;
	        } else {
	            CurrentTimeLeft = 0;
	            CountingDown = false;
                GameOver();
	        }
	    }
	}

    void GameOver() {
        EndingAnimation.gameObject.SetActive(true);
        EndingAnimation.Initialize();
    }
}

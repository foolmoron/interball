using UnityEngine;
using System.Collections;

public class TimeManager : MonoBehaviour {

    [Range(1, 100)]
    public float MaxTimeLeft = 20f;
    [Range(0, 100)]
    public float CurrentTimeLeft;
    public bool CountingDown = true;

    public float CurrentTimePercentage { get { return CurrentTimeLeft / MaxTimeLeft; } }
    
	void Start() {
	    CurrentTimeLeft = MaxTimeLeft;
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
        
    }
}

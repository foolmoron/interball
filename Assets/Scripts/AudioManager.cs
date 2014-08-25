using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

    public AudioClip[] BumperSounds;
    public AudioClip[] TimeOrbSounds;
    public AudioClip GameOverSound;
    public AudioClip SwapperSound;

    bool canPlayBumperSound;
    bool canPlayTimeOrbSound;

    public void PlayRandomBumperSound() {
        if (canPlayBumperSound) {
            var sound = BumperSounds[Mathf.FloorToInt(Random.value * BumperSounds.Length)];
            AudioSource.PlayClipAtPoint(sound, Vector3.zero);
        }
    }

    public void PlayRandomTimeOrbSound() {
        if (canPlayTimeOrbSound) {
            var sound = TimeOrbSounds[Mathf.FloorToInt(Random.value * TimeOrbSounds.Length)];
            AudioSource.PlayClipAtPoint(sound, Vector3.zero);
        }
    }

    public void PlayGameOverSound() {
        AudioSource.PlayClipAtPoint(GameOverSound, Vector3.zero);
    }

    public void PlaySwapperSound() {
        AudioSource.PlayClipAtPoint(SwapperSound, Vector3.zero);
    }

    void Update() {
        canPlayBumperSound = true;
        canPlayTimeOrbSound = true;
    }
}

using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

    public AudioClip[] BumperSounds;
    public AudioClip GameOverSound;
    public AudioClip SwapperSound;

    bool canPlayBumperSound;

    public void PlayRandomBumperSound() {
        if (canPlayBumperSound) {
            var sound = BumperSounds[Mathf.FloorToInt(Random.value * BumperSounds.Length)];
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
    }
}

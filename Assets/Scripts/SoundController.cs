using UnityEngine;
using System.Collections;

public class SoundController : MonoBehaviour {

	public static SoundController Instance;
	public AudioSource soundEffect;
	public AudioSource music;


	private float lowPitchRange = 1f;
	private float highPitchRange = 1f;

	void Awake(){
		if (Instance != null && Instance != this) {
			DestroyImmediate(gameObject);
			return;
		}
		Instance = this;
		DontDestroyOnLoad (gameObject);
	}

	public void PlaySingle(params AudioClip[] clips){
		RandomizeSoundEffect (clips);
		soundEffect.Play ();
	}

	public void NotRandomPlaySingle(params AudioClip[] clips){

		soundEffect.Play ();
	}

	public void PlayVoice(params AudioClip[] clips){
		AudioSource audiosource = gameObject.AddComponent<AudioSource> ();
		audiosource.clip = clips[0];
		audiosource.Play ();
	}


	private void RandomizeSoundEffect(AudioClip[] clips){
		int randomSoundIndex = Random.Range(0, clips.Length);
		float randomPitch = Random.Range (lowPitchRange, highPitchRange);

		soundEffect.pitch = randomPitch;
		soundEffect.clip = clips [randomSoundIndex];
	}
}

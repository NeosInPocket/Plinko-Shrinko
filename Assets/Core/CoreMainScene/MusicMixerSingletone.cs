using UnityEngine;

public class MusicMixerSingletone : SingletoneTemplate<MusicMixer>
{
	protected override void SecondaryInitialize()
	{
		AudioClip gameMusic = Resources.Load<AudioClip>("shrinkMusic");
		Instance.mixer = Instance.gameObject.AddComponent<AudioSource>();
		Instance.mixer.clip = gameMusic;
		Instance.mixer.Play();
		Instance.mixer.volume = DataAccessorSingleTone.Instance.Music;
	}
}

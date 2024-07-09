using UnityEngine;

public class MusicMixer : MonoBehaviour
{
	[SerializeField] public AudioSource mixer;

	public float CurrentMusicVolume => mixer.volume;
}

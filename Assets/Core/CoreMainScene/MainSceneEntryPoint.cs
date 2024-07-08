using UnityEngine;

public class MainSceneEntryPoint : MonoBehaviour
{
	public void Awake()
	{
		var dataAcces = new DataAccessorSingleTone();
		var misucMixer = new MusicMixerSingletone();

		dataAcces.Initialize();
		misucMixer.Initialize();
	}
}

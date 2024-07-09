using UnityEngine;
using UnityEngine.UI;

public class OptionsSetter : MonoBehaviour
{
	[SerializeField] public Slider sliderMusic;
	[SerializeField] public Slider sliderEffect;

	private void Start()
	{
		sliderMusic.value = MusicMixerSingletone.Instance.CurrentMusicVolume;
		sliderEffect.value = DataAccessorSingleTone.Instance.Effect;
	}

	public void MusicSliderValueChangesCallback(float value)
	{
		MusicMixerSingletone.Instance.mixer.volume = value;
		DataAccessorSingleTone.Instance.Music = value;
		DataAccessorSingleTone.Instance.SaveAccess();
	}

	public void EffectSliderValueChangesCallback(float value)
	{
		DataAccessorSingleTone.Instance.Effect = value;
		DataAccessorSingleTone.Instance.SaveAccess();
	}
}

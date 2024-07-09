using UnityEngine;

public class EffectsVolumeControl : MonoBehaviour
{
	[SerializeField] public AudioSource source;

	private void Start()
	{
		source.volume = DataAccessorSingleTone.Instance.Effect;
	}
}

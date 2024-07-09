using TMPro;
using UnityEngine;

public class MainWindowHolder : MonoBehaviour
{
	[SerializeField] private TMP_Text mainCrystals;
	[SerializeField] private TMP_Text mainLevel;
	[SerializeField] private Animator mainAnimator;

	private void Start()
	{
		ReleaseCrystals();
	}

	public void ReleaseCrystals()
	{
		mainCrystals.text = DataAccessorSingleTone.Instance.Crystals.ToString();
		mainLevel.text = "LEVEL " + DataAccessorSingleTone.Instance.Level.ToString();
	}

	public void SetAnimatorTrigger(string trigger)
	{
		mainAnimator.SetTrigger(trigger);
	}
}

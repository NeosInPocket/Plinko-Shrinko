using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressWatcher : MonoBehaviour
{
	[SerializeField] private TMP_Text levelTMP;
	[SerializeField] private TMP_Text rewardTMP;
	[SerializeField] private Image currentLoading;
	[SerializeField] private Image healthLoading;

	public int ScoreProgress { get; private set; }
	public int MaxScoreProgress { get; private set; }
	public int Crystals { get; private set; }

	private void Start()
	{
		MaxScoreProgress = (int)(3 * Mathf.Sqrt(DataAccessorSingleTone.Instance.Level));
		levelTMP.text = "LEVEL " + DataAccessorSingleTone.Instance.Level;
		Crystals = (int)(22 * Mathf.Sqrt(DataAccessorSingleTone.Instance.Level));
		rewardTMP.text = Crystals.ToString();
		healthLoading.fillAmount = (float)DataAccessorSingleTone.Instance.Shield / 3f;
		currentLoading.fillAmount = 0;
	}

	public void Damaged(int lifesLeft)
	{
		healthLoading.fillAmount = (float)lifesLeft / 3f;
	}

	public bool WatchIncrease()
	{
		ScoreProgress++;

		currentLoading.fillAmount = (float)ScoreProgress / (float)MaxScoreProgress;

		if (ScoreProgress == MaxScoreProgress)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
}

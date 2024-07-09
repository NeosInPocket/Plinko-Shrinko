using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalEnd : MonoBehaviour
{
	[SerializeField] private TMP_Text nextLevelText;
	[SerializeField] private TMP_Text mainCore;
	[SerializeField] private TMP_Text crystalsText;
	[SerializeField] private string defaultLevel;

	public void ShowFinals(bool win, int crystals)
	{
		gameObject.SetActive(true);

		crystalsText.text = $"+{crystals}";
		if (win)
		{
			var nextLevel = DataAccessorSingleTone.Instance.Level;
			mainCore.text = "LEVEL COMPLETED";
			nextLevelText.text = $"NEXT LEVEL: {nextLevel + 1}";

			DataAccessorSingleTone.Instance.Level++;
			DataAccessorSingleTone.Instance.Crystals += crystals;
			DataAccessorSingleTone.Instance.SaveAccess();
		}
		else
		{
			var nextLevel = DataAccessorSingleTone.Instance.Level;
			mainCore.text = "DEFEATED";
			nextLevelText.text = $"RETRY LEVEL {nextLevel}?";
		}
	}

	public void ReloadLevel()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void LoadDefault()
	{
		SceneManager.LoadScene(defaultLevel);
	}
}

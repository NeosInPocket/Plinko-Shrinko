
using UnityEngine;

public class DataAccessorSingleTone : SingletoneTemplate<DataAccessor>
{
	protected override void SecondaryInitialize()
	{
		var gameSettings = Resources.Load<GameSettings>("GameSettings");
		var clearSettings = gameSettings.reloadSettings;

		if (clearSettings)
		{
			PlayerPrefs.DeleteAll();
			Instance.Level = gameSettings.defaultLevel;
			Instance.Crystals = gameSettings.defaultCrystals;
			Instance.Shield = gameSettings.defaultShield;
			Instance.Horn = gameSettings.defaultHorn;
			Instance.Music = gameSettings.defaultMusic;
			Instance.Effect = gameSettings.defaultEffect;
			Instance.Manual = gameSettings.defaultManual;

			Instance.SaveAccess();
		}
		else
		{
			Instance.Level = PlayerPrefs.GetInt("Level", 404);
			Instance.Crystals = PlayerPrefs.GetInt("Crystals", 404);
			Instance.Shield = PlayerPrefs.GetInt("Shield", 404);
			Instance.Horn = PlayerPrefs.GetInt("Horn", 404);
			Instance.Music = PlayerPrefs.GetFloat("Music", 1f);
			Instance.Effect = PlayerPrefs.GetFloat("Effect", 1f);
			Instance.Manual = PlayerPrefs.GetInt("Manual", 404);
		}
	}
}


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
			Instance.Level = PlayerPrefs.GetInt("Level", gameSettings.defaultLevel);
			Instance.Crystals = PlayerPrefs.GetInt("Crystals", gameSettings.defaultCrystals);
			Instance.Shield = PlayerPrefs.GetInt("Shield", gameSettings.defaultShield);
			Instance.Horn = PlayerPrefs.GetInt("Horn", gameSettings.defaultHorn);
			Instance.Music = PlayerPrefs.GetFloat("Music", gameSettings.defaultMusic);
			Instance.Effect = PlayerPrefs.GetFloat("Effect", gameSettings.defaultEffect);
			Instance.Manual = PlayerPrefs.GetInt("Manual", gameSettings.defaultManual);
		}
	}
}

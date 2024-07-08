
using UnityEngine;

public class DataAccessorSingleTone : SingletoneTemplate<DataAccessor>
{
	private bool clearSettings;
	public int Level;
	public int Crystals;
	public int Shield;
	public int Horn;
	public int Music;
	public int Effect;
	public int Manual;

	protected override void SecondaryInitialize()
	{
		var gameSettings = Resources.Load<GameSettings>("GameSettings");
		clearSettings = gameSettings.reloadSettings;

		if (clearSettings)
		{
			PlayerPrefs.DeleteAll();

			Level = gameSettings.defaultLevel;
			Crystals = gameSettings.defaultCrystals;
			Shield = gameSettings.defaultShield;
			Horn = gameSettings.defaultHorn;
			Music = gameSettings.defaultMusic;
			Effect = gameSettings.defaultEffect;
			Manual = gameSettings.defaultManual;

			SaveAccess();
		}
		else
		{
			Level = PlayerPrefs.GetInt("Level", 404);
			Crystals = PlayerPrefs.GetInt("Crystals", 404);
			Shield = PlayerPrefs.GetInt("Shield", 404);
			Horn = PlayerPrefs.GetInt("Horn", 404);
			Music = PlayerPrefs.GetInt("Music", 404);
			Effect = PlayerPrefs.GetInt("Effect", 404);
			Manual = PlayerPrefs.GetInt("Manual", 404);
		}
	}

	public bool SaveAccess()
	{
		PlayerPrefs.SetInt("Level", Level);
		PlayerPrefs.SetInt("Crystals", Crystals);
		PlayerPrefs.SetInt("Shield", Shield);
		PlayerPrefs.SetInt("Horn", Horn);
		PlayerPrefs.SetInt("Music", Music);
		PlayerPrefs.SetInt("Effect", Effect);
		PlayerPrefs.SetInt("Manual", Manual);

		PlayerPrefs.Save();

		if (Level > 1)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

}

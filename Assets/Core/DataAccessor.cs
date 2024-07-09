using UnityEngine;

public class DataAccessor : MonoBehaviour
{
	public int Level;
	public int Crystals;
	public int Shield;
	public int Horn;
	public float Music;
	public float Effect;
	public int Manual;

	public bool SaveAccess()
	{
		PlayerPrefs.SetInt("Level", Level);
		PlayerPrefs.SetInt("Crystals", Crystals);
		PlayerPrefs.SetInt("Shield", Shield);
		PlayerPrefs.SetInt("Horn", Horn);
		PlayerPrefs.SetFloat("Music", Music);
		PlayerPrefs.SetFloat("Effect", Effect);
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

	public void SkillIncreaseData(bool shieldSkill, int crystals)
	{
		if (shieldSkill)
		{
			Shield++;
		}
		else
		{
			Horn++;
		}

		Crystals -= crystals;
		SaveAccess();
	}
}

using UnityEngine;

[CreateAssetMenu(menuName = "Game Settings", fileName = "GameSettings")]
public class GameSettings : ScriptableObject
{
	public bool reloadSettings;

	public int defaultLevel;
	public int defaultCrystals;
	public int defaultShield;
	public int defaultHorn;
	public float defaultMusic;
	public float defaultEffect;
	public int defaultManual;
}

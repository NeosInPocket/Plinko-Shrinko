using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillCore : MonoBehaviour
{
	[SerializeField] public int crystals;
	[SerializeField] public TMP_Text bar;
	[SerializeField] public Image barFill;
	[SerializeField] public TMP_Text crystalsText;
	[SerializeField] public Button mainCard;
	[SerializeField] public SkillCore secondarySkill;
	[SerializeField] public MainWindowHolder mainWindowHolder;
	[SerializeField] public TMP_Text currentStoreCrystals;
	public bool shieldUpgrade;

	private void Start()
	{
		CoreRelease();
	}

	public void CoreRelease()
	{
		Color crystalsColor = Color.white;
		bool cardInteractable = true;

		int allCrystals = DataAccessorSingleTone.Instance.Crystals;
		int currentSkillValue = shieldUpgrade ? DataAccessorSingleTone.Instance.Shield : DataAccessorSingleTone.Instance.Horn;
		bool enoughCrystals = allCrystals >= crystals;
		bool isMax = currentSkillValue < 3;

		if (!isMax)
		{
			cardInteractable = false;
			crystalsColor = Color.white;
		}
		else
		{
			if (enoughCrystals)
			{
				cardInteractable = true;
				crystalsColor = Color.white;
			}
			else
			{
				cardInteractable = false;
				crystalsColor = Color.red;
			}
		}

		mainCard.interactable = cardInteractable;
		crystalsText.color = crystalsColor;
		barFill.fillAmount = (float)currentSkillValue / 3f;
		bar.text = $"{currentSkillValue}/3";

		currentStoreCrystals.text = DataAccessorSingleTone.Instance.Crystals.ToString();
	}

	public void SkillIncrease()
	{
		DataAccessorSingleTone.Instance.SkillIncreaseData(shieldUpgrade, crystals);
		secondarySkill.CoreRelease();
		CoreRelease();

		mainWindowHolder.ReleaseCrystals();
	}
}

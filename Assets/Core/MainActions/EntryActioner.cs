using UnityEngine;

public class EntryActioner : MonoBehaviour
{
	[SerializeField] private ReturnSphere returnSphere;

	public void StartActioner()
	{
		gameObject.SetActive(true);
	}

	public void StartReturnSphere()
	{
		returnSphere.EnableAllSubscriptions();
		gameObject.SetActive(false);
	}
}

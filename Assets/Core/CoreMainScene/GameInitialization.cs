using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInitialization : MonoBehaviour
{
	[SerializeField] string sceneToGo;

	public void GoToScene()
	{
		SceneManager.LoadScene(sceneToGo);
	}
}

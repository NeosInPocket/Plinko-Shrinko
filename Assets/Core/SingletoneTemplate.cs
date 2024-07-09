using UnityEngine;

public class SingletoneTemplate<T> where T : MonoBehaviour
{
	private static T instance;

	public static T Instance
	{
		get => instance;
	}

	public void Initialize()
	{
		if (instance == null)
		{
			GameObject obj = new GameObject();
			obj.name = typeof(T).Name;
			instance = obj.AddComponent<T>();

			GameObject.DontDestroyOnLoad(instance.gameObject);

			SecondaryInitialize();
		}
	}

	protected virtual void SecondaryInitialize() { }
}
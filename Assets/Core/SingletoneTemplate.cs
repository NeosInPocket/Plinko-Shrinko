using UnityEngine;

public class SingletoneTemplate<T> where T : MonoBehaviour
{
	private static T instance;

	public static T Instance
	{
		get
		{
			if (instance == null)
			{
				instance = GameObject.FindObjectOfType<T>();
				if (instance == null)
				{
					GameObject obj = new GameObject();
					obj.name = typeof(T).Name;
					instance = obj.AddComponent<T>();
				}
			}
			return instance;
		}
	}

	protected virtual void Awake()
	{
		if (instance == null)
		{
			instance = this as T;
			GameObject.DontDestroyOnLoad(instance.gameObject);
		}
		else
		{
			GameObject.Destroy(instance.gameObject);
		}
	}
}
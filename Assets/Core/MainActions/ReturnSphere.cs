using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class ReturnSphere : MonoBehaviour
{
	[SerializeField] private SpriteRenderer spriteRenderer;
	[SerializeField] private new Rigidbody2D rigidbody2D;
	[SerializeField] private GameObject finalEffect;
	[SerializeField] private float rotateAroundRadius;
	[SerializeField] private float shootSpeed;
	[SerializeField] private Transform pivot;
	[SerializeField] private float[] upgradeHorn;
	[SerializeField] private FinalEnd finalEnd;
	[SerializeField] private ProgressWatcher progressWatcher;
	[SerializeField] private CollisionsSetter collisionsSetter;
	[SerializeField] private GameObject damageSound;
	[SerializeField] private GameObject passSound;
	private Vector3 rot;
	private Vector2 angular;
	private float currentRotateAngle;
	private float currentRot;
	private int currentLifes;
	private int dir = 1;
	private bool rotating;
	private float radius;

	private void Start()
	{
		EnhancedTouchSupport.Enable();
		TouchSimulation.Enable();

		currentRot = upgradeHorn[DataAccessorSingleTone.Instance.Horn];
		currentLifes = DataAccessorSingleTone.Instance.Shield;

		radius = CollisionsSetter.ScreenWidth * rotateAroundRadius;
		transform.localPosition = new Vector2(-radius, 0);
		rotating = true;

	}

	public void EnableAllSubscriptions()
	{
		Touch.onFingerDown += LaunchSphere;
	}

	public void LaunchSphere(Finger finger)
	{
		Touch.onFingerDown -= LaunchSphere;

		rotating = false;
		rigidbody2D.velocity = transform.position.normalized * shootSpeed;
	}

	private CollisionSphere currentSphere = null;

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (Defeat) return;

		if (collider.TryGetComponent<CollisionSphere>(out CollisionSphere component))
		{
			if (currentSphere != null) return;

			if (!component.Active)
			{
				if (DamageSphere())
				{
					return;
				}
			}
			else
			{
				currentSphere = component;
				StartCoroutine(WaitForCollisionExit());

				component.HitSphere();
				passSound.SetActive(false);
				passSound.SetActive(true);

				if (progressWatcher.WatchIncrease())
				{
					EndGame(true);
					return;
				}
			}

			rigidbody2D.velocity = -rigidbody2D.velocity;
			StartCoroutine(WaitForCenter());
		}
	}

	public IEnumerator WaitForCollisionExit()
	{
		yield return new WaitForSeconds(0.3f);
		currentSphere = null;
	}

	public bool Defeat
	{
		get; set;
	}

	public void EndGame(bool win)
	{
		StopAllCoroutines();

		if (win)
		{
			rotating = false;
			rigidbody2D.velocity = Vector2.zero;
			finalEnd.ShowFinals(true, progressWatcher.Crystals);
		}
		else
		{
			rotating = false;
			rigidbody2D.velocity = Vector2.zero;
			spriteRenderer.enabled = false;
			finalEffect.SetActive(true);
			finalEnd.ShowFinals(false, 0);
		}

		Defeat = true;
	}

	public bool DamageSphere()
	{
		if (Defeat) return false;

		damageSound.SetActive(false);
		damageSound.SetActive(true);

		currentLifes--;
		progressWatcher.Damaged(currentLifes);

		if (currentLifes <= 0)
		{
			EndGame(false);
			return true;
		}

		StopAllCoroutines();

		rigidbody2D.velocity = Vector2.zero;
		rotating = true;
		transform.localPosition = new Vector2(-radius, 0);

		if (Random.Range(0, 1f) > 0.5f)
		{
			dir *= -1;
		}

		Touch.onFingerDown += LaunchSphere;
		return false;
	}

	private void Update()
	{
		if (Vector2.Distance(transform.position, Vector2.zero) > collisionsSetter.MaxRadius + 0.3f)
		{
			DamageSphere();
		}

		if (!rotating || Defeat) return;
		rot.z += Time.deltaTime * currentRot * dir;
		pivot.eulerAngles = rot;
	}

	public IEnumerator WaitForCenter()
	{
		while (Vector2.Distance(transform.position, Vector2.zero) > radius)
		{
			yield return null;
		}

		rigidbody2D.velocity = Vector2.zero;
		rotating = true;

		if (Random.Range(0, 1f) > 0.5f)
		{
			dir *= -1;
		}

		Touch.onFingerDown += LaunchSphere;
	}

	public void KillAllSubscriptions()
	{
		Touch.onFingerDown -= LaunchSphere;
	}

	private void OnDestroy()
	{
		EnhancedTouchSupport.Disable();
		TouchSimulation.Disable();
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class ManualShower : MonoBehaviour
{
	[SerializeField] private EntryActioner entryActioner;
	[SerializeField] private TMP_Text actionerText;
	[SerializeField] private Animator speechController;
	[SerializeField] private Transform handle;
	[SerializeField] private float handleOffset;
	[SerializeField] private float handleOffsetX;
	[SerializeField] private Transform player;
	[SerializeField] private CollisionsSetter collisionsSetter;
	private List<Action> AllSpeeches;
	private int currentIndexer;

	private void Start()
	{
		EnhancedTouchSupport.Enable();
		TouchSimulation.Enable();

		var manual = DataAccessorSingleTone.Instance.Manual == 1;
		if (manual)
		{
			DataAccessorSingleTone.Instance.Manual = 0;
			DataAccessorSingleTone.Instance.SaveAccess();

			AllSpeeches = new List<Action>()
			{
				SpherePoint,
				YourSphereTarget,
				TrueTarget,
				Overflow,
				Progress,
				Luck
			};

			Touch.onFingerDown += ShowSpeech;
		}
		else
		{
			entryActioner.StartActioner();
			gameObject.SetActive(false);
		}
	}

	public void ShowSpeech(Finger finger)
	{
		if (currentIndexer >= AllSpeeches.Count)
		{
			Touch.onFingerDown -= ShowSpeech;
			entryActioner.StartActioner();
			gameObject.SetActive(false);
			return;
		}

		AllSpeeches[currentIndexer]();
		currentIndexer++;
	}

	public void SpherePoint()
	{
		actionerText.text = "LOOK HERE! HERE IS YOUR BALL: IT CONSTANTLY ROTATES ABOUT ITS CENTER";
		StartCoroutine(HandleFollowTarget(player));
	}

	public void YourSphereTarget()
	{
		StopAllCoroutines();
		handle.gameObject.SetActive(false);
		actionerText.text = "your combat task is to shoot your ball at the rotating meteorites";
		StartCoroutine(HandleFollowTarget(collisionsSetter.startSpheres.FirstOrDefault(x => x.Active).transform));
	}

	public void TrueTarget()
	{
		StopAllCoroutines();
		handle.gameObject.SetActive(false);
		actionerText.text = "you must hit the glowing meteorite to earn a point";
	}

	public void Overflow()
	{
		actionerText.text = "aim accurately! flying off the screen you will lose your health, which is not infinite";
	}

	public void Progress()
	{
		actionerText.text = "follow the progress of the level, as soon as you complete it you will receive a reward in the form of crystals";
		speechController.enabled = true;
	}

	public void Luck()
	{
		speechController.enabled = false;
		handle.gameObject.SetActive(false);
		actionerText.text = "good luck!";
	}

	public IEnumerator HandleFollowTarget(Transform target)
	{
		handle.gameObject.SetActive(true);
		Vector2 pos = handle.transform.position;

		while (true)
		{
			pos = target.position;
			pos.y -= handleOffset;
			pos.x -= handleOffsetX;
			handle.transform.position = pos;
			yield return null;
		}
	}

	private void OnDestroy()
	{
		Touch.onFingerDown -= ShowSpeech;
	}
}

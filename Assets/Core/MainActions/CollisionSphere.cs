using UnityEngine;

public class CollisionSphere : MonoBehaviour
{
	[SerializeField] private SpriteRenderer sphereRenderer;
	[SerializeField] private GameObject activationEffect;
	[SerializeField] private GameObject hitEffect;
	[SerializeField] private Sprite bwSprite;
	[SerializeField] private Sprite activeSprite;
	public float Radius => sphereRenderer.size.x / 2;
	public CollisionsSetter collisionsSetter;
	public bool Active { get; set; }

	public void HitSphere()
	{
		hitEffect.SetActive(false);
		hitEffect.SetActive(true);
		activationEffect.SetActive(false);
		sphereRenderer.sprite = bwSprite;

		collisionsSetter.ChooseRandomCollision(this);
		Active = false;
	}

	public void SetActiveSphere()
	{
		activationEffect.SetActive(false);
		activationEffect.SetActive(true);
		sphereRenderer.sprite = activeSprite;
		Active = true;
	}
}

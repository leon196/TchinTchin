using UnityEngine;
using System.Collections;

public class TchinTchinController : MonoBehaviour
{
	public Transform target;
	public Transform elbowHint;
	public Transform hand;

	public float moveHandSpeed = 1;
	public float slowMotionTime = 0.2f;
	public float slowdownTimeScale = 0.5f;

	private Transform targetHelper;
	public Vector3 offset = Vector3.zero;

	private Animator animator;
	private float fireStart = 0f;
	private float fireDelay = 0.4f;
	private Vector3 screenPosition;
	private Vector3 screenPositionTarget;

	void Start()
	{
		animator = GetComponentInChildren<Animator>();
		// offset = hand.position - target.position;
		screenPosition = new Vector3(0.85f, 0.25f, 1f);
		screenPositionTarget = new Vector3(0.5f, 0.25f, 2.5f);
		target.parent = hand;
		GameObject go = new GameObject("TargetHelper");
		go.hideFlags = HideFlags.HideInHierarchy;
		targetHelper = go.transform;
	}

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			fireStart = Time.time;
		}

		float ratio = Mathf.Clamp01((Time.time - fireStart) / fireDelay);
		float depth = Mathf.Sin(ratio * Mathf.PI);
		Transform camTransform = Camera.main.transform;

		Vector3 pos = Vector3.Lerp(screenPosition, screenPositionTarget, depth);
		pos = Camera.main.ViewportToWorldPoint(offset + pos + Vector3.forward * depth);
		targetHelper.position = Vector3.Lerp(targetHelper.position, pos, Time.deltaTime * 5f);
		// targetHelper.position = Vector3.Lerp(targetHelper.position, Camera.main.ViewportToWorldPoint(offset + screenPosition + Vector3.forward * depth), Time.deltaTime * 5f);

		Vector3 forward = Vector3.Normalize(targetHelper.position - camTransform.position);
		forward.y = 0f;
		forward = Vector3.Normalize(forward);
		Vector3 left = Vector3.Cross(forward, Vector3.up);
		targetHelper.LookAt(targetHelper.position + forward + left);

		// Time.timeScale = Mathf.Lerp(1f, slowdownTimeScale, Mathf.InverseLerp(0.9f, 1f, depth));
	}
	
	void OnAnimatorIK(int layerIndex)
	{
		if (animator != null)
		{
			animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
			animator.SetIKPosition(AvatarIKGoal.RightHand, targetHelper.position);

			animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1f);
			animator.SetIKRotation(AvatarIKGoal.RightHand, targetHelper.rotation);

			if (elbowHint)
			{
				animator.SetIKHintPositionWeight(AvatarIKHint.RightElbow, 1f);
				animator.SetIKHintPosition(AvatarIKHint.RightElbow, elbowHint.position);
			}
		}
	}
}

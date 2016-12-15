using UnityEngine;
using System.Collections;

public class SimpleController : MonoBehaviour 
{
	public float moveSpeed = 1f;

	void Awake ()
	{
	}

	void Update () 
	{
		float forward = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
		float strafe = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
		float jump = Input.GetKey(KeyCode.Space) ? 1f : 0f * Time.deltaTime * moveSpeed;
		transform.Translate(Vector3.forward * forward, Space.Self);
		transform.Translate(Vector3.right * strafe, Space.Self);
		transform.Translate(Vector3.up * jump, Space.Self);
	}
}
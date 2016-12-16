using UnityEngine;
using System.Collections;

public class Boid
{
	public Vector3 position;
	public Vector3 target;
	public Vector3 velocity;
	public float size;
	public float speed;
	public float friction;
	public float targetScale;
	public float avoidScale;

	public Boid ()
	{
		float velocityAngle = Random.Range(0f, 1f) * Mathf.PI * 2f;
		velocity.x = Mathf.Cos(velocityAngle);
		velocity.y = 0f;//Random.Range(-1f, 1f);
		velocity.z = Mathf.Sin(velocityAngle);

		size = 1f + Random.Range(0, 1f);
		speed = 0.4f - Random.Range(0, 0.2f);
		friction = 0.99f;// - Random.Range(0f, 0.1f);

		targetScale = 2f;
		avoidScale = 1f;
	}

	public void Move (Vector3 movement)
	{
		velocity.x += movement.x;
		// velocity.y += movement.y;
		velocity.z += movement.z;

		position.x += velocity.x * speed * Time.deltaTime;
		// position.y += velocity.y * speed * Time.deltaTime;
		position.z += velocity.z * speed * Time.deltaTime;

		velocity.x *= friction;
		// velocity.y *= friction;
		velocity.z *= friction;
	}
}
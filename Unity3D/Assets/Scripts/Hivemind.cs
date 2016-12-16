using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hivemind : MonoBehaviour {

	public GameObject[] agentPrefabArray;
	public Transform target;
	public Transform collider;
	public int count = 30;
	public float globalSpeed = 1f;

	private float range = 10f;
	private GameObject[] agentArray;
	private Boid[] boidArray;
	private Vector3 vectorTarget;
	private Vector3 vectorAvoid;
	private Vector3 vectorRotate;

	GameObject GetRandomIndividual ()
	{
		return agentPrefabArray[Random.Range(0, agentPrefabArray.Length)];
	}

	void Start ()
	{
		agentArray = new GameObject[count];
		boidArray = new Boid[count];
		for (int i = 0; i < count; ++i) {
			GameObject go = GameObject.Instantiate(GetRandomIndividual());
			go.transform.parent = transform;
			agentArray[i] = go;
			Boid boid = new Boid();
			boid.position = new Vector3(Random.Range(-range, range), 0f, Random.Range(-range, range));
			boidArray[i] = boid;
		}
	}

	Vector3 GetRight (Vector3 vector)
	{
		return new Vector3(vector.z, 0f, -vector.x);
	}
	
	void Update ()
	{
		for (int current = 0; current < count; ++current) {
			GameObject agent = agentArray[current];
			Boid boid = boidArray[current];
			vectorTarget = target.position - boid.position;
			vectorAvoid = Vector3.zero;
			vectorRotate = Vector3.zero;

			for (int other = 0; other < count; ++other) {
				if (current != other) {
					Boid boidOther = boidArray[other];
					float distance = Vector3.Distance(boid.position, boidOther.position);
					float width = boid.size + boidOther.size;
					distance = distance - width;
					if (distance < 0.1f) {
						Vector3 avoid = boid.position - boidOther.position;
						vectorAvoid += Vector3.Normalize(avoid) * Mathf.Max(width, distance);
					}
				}
			}

			vectorAvoid *= boid.avoidScale;
			vectorTarget *= boid.targetScale;
			vectorRotate = GetRight(Vector3.Normalize(vectorTarget)) / vectorTarget.magnitude;
			vectorRotate *= Mathf.Lerp(-1f, 1f, (current % 2));

			Vector3 move = vectorTarget + vectorAvoid + vectorRotate * 10f;
			// move.y = 0;
			move = Vector3.Normalize(move) * globalSpeed;

			// float threshold = 4f;
			// float a = Vector3.Distance(boid.position, collider.position);
			// float w = boid.size - threshold;
			// if (a - w < 0.1f) {
			// 	move += 100f * Vector3.Normalize(boid.position - collider.position) * Mathf.Max(w, a);
			// }

			boid.Move(move);
			agent.transform.position = boid.position;
			agent.transform.LookAt(boid.position + boid.velocity);
		}
	}
}

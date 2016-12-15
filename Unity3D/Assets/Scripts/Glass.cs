using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glass : MonoBehaviour {

	public ParticleSystem tchin;
	public GameObject brokenGlassPrefab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Glass") {
			tchin.transform.position = transform.position + transform.TransformPoint(Vector3.up) * 0.15f;
			tchin.Emit(100);
		} else if (other.tag == "Break") {
			GameObject go = Instantiate(brokenGlassPrefab, transform.position, transform.rotation) as GameObject;
			GameObject.Destroy(go, 5f);
		}
	}
}

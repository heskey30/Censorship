﻿using UnityEngine;
using System.Collections;

public class Idea : MonoBehaviour {
	public string ideaStr;
	public Vector3 origin;
	public Vector3 destination;
	float time = 0;
	public float speed;
	public float totalTime;
	Transform t;


	// Use this for initialization
	void Start () {
		totalTime = (origin - destination).magnitude/speed;
		t = transform;
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		t.position = Vector3.Lerp(origin, destination, time/totalTime);
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		Node otherNode = col.GetComponent<Node>();
		if (otherNode && time/totalTime > .5)
		{
			otherNode.reciveIdea(ideaStr);
			Destroy(gameObject);
			return;
		}
		
	}

}

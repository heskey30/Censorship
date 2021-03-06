﻿using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Idea : NetworkBehaviour {
    [SyncVar]
	public string ideaStr;
	[SyncVar]
	public Vector3 origin;
	[SyncVar]
	public Vector3 destination;
	public Node originObj;
	protected float time = 0;
	public float speed;
	public float totalTime;
    public int index;

    [SyncVar]
	public GameObject dest;

	public float minTimeToTarget = .1f; //how long should the minimum lifetime of an idea be? Used to fix a memory leak. 
	/*
    string[] ideas = new string[] {
        "Feminism",
        "Mens Rights Movement",
        "Censorship/Privacy violation is a problem",
        "Censorship/Privacy violation is necessary",
        "Conservative",
        "Liberal",
        "Violent Extremism",
        "Pacifism",
        "Xenophobia",
        "Globalism"
    };
	*/
    // Use this for initialization
    public virtual void Start() {
		totalTime = (origin - destination).magnitude/speed;
		GetComponent<SpriteRenderer>().color = IdeaList.instance.list[IdeaList.instance.ideaDict[ideaStr]].color;
	}

	// Update is called once per frame
	void Update() {
		time += Time.deltaTime;
		transform.position = Vector3.Lerp(origin, destination, time / totalTime);
		if (isServer) {
			CheckLifetime ();
		}
	}

    
    [Server]
    public virtual void CheckLifetime() {
		if (Mathf.Abs(time-totalTime) < minTimeToTarget)
		{
			if (dest)
			{
				dest.GetComponent<Node>().receiveIdea(ideaStr);
			}
			NetworkServer.Destroy(gameObject);
		}
    }

	/*void OnTriggerEnter2D(Collider2D col)
	{
		print("Collision");
		Node otherNode = col.GetComponent<Node>();
		print(totalTime);
		if (otherNode && (time/totalTime > .5 || totalTime < .2))
		{
			
			return;
		}
		
	}*/

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0)) {
            Global.textbg = true;
            Global.text = "";
            Global.text = "<size=16><b><color=#" + ColorToHex(IdeaList.instance.list[index].color) + ">" + ideaStr + "</color></b></size>" + ": " + IdeaList.instance.list[index].description;
        }
    }

    string ColorToHex(Color32 color)
    {
        string hex = color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2");
        return hex;
    }

}

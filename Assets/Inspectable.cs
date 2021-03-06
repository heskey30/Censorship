﻿using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Inspectable : NetworkBehaviour {
	public bool mobile = false;

	public float spawnRange = 0;

	public bool selected;
	public bool firable = true;

	public SpriteRenderer selectorTemplate;

	public MovementController movement;

	SpriteRenderer selector;


	public void Start()
	{
		movement = GetComponent<MovementController>();
	}

	public void deselect()
	{
		selected = false;
		if (selector)
		{
			Destroy(selector.gameObject);
		}
	}

	public void select()
	{
		if (!selected)
		{
			selector = Instantiate(selectorTemplate);
			selector.transform.parent = transform;
			selector.transform.localPosition = Vector3.zero;
			selected = true;
            GameObject.FindGameObjectWithTag("CommandCard").GetComponent<GridAccess>().OnSelectUnit(this.gameObject);
		}
	}

	public void goTo(Vector3 position)
	{
		if (movement)
		{
			movement.goTo(position);
		}
	}

    public void Stop() {
        if (movement) {
            movement.Stop();
        }
    }

	void OnDestroy()
	{
		Global g = Global.getLocalPlayer();
		if (g)
		{
			g.deSelect(this);
		}
	}


    [Command]
    void CmdDestroySelf()
    {
        NetworkServer.Destroy(gameObject);
    }
    
    [Client]
    public virtual void DestroySelf()
    {
        if (hasAuthority)
            CmdDestroySelf();
    }


}

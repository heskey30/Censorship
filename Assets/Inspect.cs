﻿using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;
using ExtensionMethods;
public class Inspect : NetworkBehaviour {
	public Vector2 offset;

	GameObject inspecting;

	Image img;
	Text text;
	BoxCollider2D col;

	// Use this for initialization
    [ClientCallback]
	void Start () {
		img = GetComponentInChildren<Image>();
		text = GetComponentInChildren<Text>();
		col = GetComponentInParent<BoxCollider2D>();
		Disable();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    [Client]
	public void Enable(GameObject target)
	{
		inspecting = target;
		img.enabled = true;
		text.enabled = true;
		col.enabled = true;
		transform.position = target.transform.position.xy() + offset;
	}

    [Client]
	public void Disable()
	{
		img.enabled = false;
		text.enabled = false;
		col.enabled = false;
	}

    [Command]
    void CmdDestroyTarget(NetworkInstanceId id)
    {
        GameObject target = NetworkServer.FindLocalObject(id);
        NetworkServer.Destroy(target);
    }

    [Client]
	public void FireTarget()
	{
		print("Destroying: " + inspecting);
		CmdDestroyTarget(inspecting.GetComponent<NetworkIdentity>().netId);
		Disable();
	}
}

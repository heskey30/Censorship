﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UpdateTimeText : MonoBehaviour {
    Text dayRef;
    public TimeBehavior timeKeeper;
	Global global;
    int lastday;
	// Use this for initialization
	void Start () {
        dayRef = GetComponent<Text>();
        lastday = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (!timeKeeper)
		{
            if (!Global.isReady())
                return;
            Global temp = Global.getLocalPlayer();
			timeKeeper = temp.GetComponent<TimeBehavior>(); 
		}
		if (!global)
		{
			global = timeKeeper.GetComponent<Global>();
		}
        dayRef.text = "DAY: " + timeKeeper.day;
        if (timeKeeper.day != lastday)
        {
			//print("running with " + global.isLocalPlayer);
            global.addIncome();
        }
        lastday = timeKeeper.day;
	}
}

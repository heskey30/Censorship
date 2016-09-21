﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SliderBehavior : MonoBehaviour {
    public int associatedScore;
    public Image fill;
    AbstractIdea ideaTracker;
    Slider tracker;
    public float sliderMax=0.25f;
	// Use this for initialization
	void Start () {
        ideaTracker = IdeaList.staticList[associatedScore];
        tracker = GetComponentInParent<Slider>();
        tracker.maxValue = sliderMax;
        fill.color = ideaTracker.color;
        Text txtRef = GetComponentInChildren<Text>(); 
        txtRef.text = ideaTracker.name;
        txtRef.alignment = TextAnchor.MiddleCenter;
        txtRef.color = Color.white;
        txtRef.fontSize = 9;
	}
	
	// Update is called once per frame
	void Update () {
        tracker.value = ideaTracker.value/IdeaList.nodeCount;
	}
}

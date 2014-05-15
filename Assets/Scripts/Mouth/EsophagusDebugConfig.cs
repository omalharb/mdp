﻿using UnityEngine;
using System.Collections;

public class EsophagusDebugConfig : MonoBehaviour 
{
	public float foodSpawnDelay = 1f;
	public float oxygenDeplete = .05f;
	public float oxygenGain = .05f;
	public float stomachDeplete = .005f;
	public float stomachGain = .075f;
	public int maxLostFoodAmount = 10;
	public bool debugActive = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
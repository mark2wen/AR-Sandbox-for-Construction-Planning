﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Use this to keep track of what state we are in
//Should other objects find the current state here?  Should this manager what is called for each state?

public enum DisplayMode : short {Depth, CutFill, Design, Calibrate};

public class ModeManager : MonoBehaviour {

    public static ModeManager instance = null;
    public static DisplayMode dMode = DisplayMode.Depth;
	private Road road;
	private TerrainManager terrainManager;

	void Awake () {
        //Ensures that this object is a singleton
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
	}

	void Start()
	{
		terrainManager = GameObject.Find ("Terrain_Manager").GetComponent<TerrainManager>();
		road = GameObject.Find ("Road").GetComponent<Road>();
	}

	
	void Update () {
		switch (dMode) {
            case DisplayMode.Depth:     //What should occur while in Depth Mode?
			    //Debug.Log("I am in Depth mode");
				terrainManager.SetTerrainTheme(TerrainManager.TerrainTheme.rainbow);
				disableRoad();
                break;
			case DisplayMode.CutFill:   //What should occur while in CutFill Mode?
                 // Debug.Log("I am in CutFill mode");
				terrainManager.SetTerrainTheme (TerrainManager.TerrainTheme.greyscale);
				//disableTerrain();
				enableRoad ();
				road.DisableControlPoints ();
				break;
            case DisplayMode.Calibrate: //What should occur while in Calibrate Mode?
                // Debug.Log("I am in Calibrate mode");
				terrainManager.SetTerrainTheme(TerrainManager.TerrainTheme.rainbow);
				disableRoad();
				break;
			case DisplayMode.Design:    //What should occur while in Design Mode?
				terrainManager.SetTerrainTheme (TerrainManager.TerrainTheme.greyscale);
				enableRoad ();
				road.EnableControlPoints ();
				if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && Input.GetKeyDown (KeyCode.Z)) {
					road.Undo ();		
				}

                break;
        }
	}
		
	void enableTerrain() {
		terrainManager.terrainGenerator.gameObject.SetActive (true);
	}
		
	void disableTerrain() {
		terrainManager.terrainGenerator.gameObject.SetActive (false);
	}
		
	void enableRoad() {
		road.gameObject.SetActive (true);
	}
		
	void disableRoad() {
		road.gameObject.SetActive (false);
	}

}

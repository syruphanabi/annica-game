﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Ground : MonoBehaviour {
    public Image gameOverImage;
	public TearManager tear_manager;
    bool death = false;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
        if (death) {
        	System.Threading.Thread.Sleep(500); 
            gameOverImage.gameObject.SetActive(true);
        }
	}
	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.name == "annica" && !death) {
			//Destroy(collision.gameObject);
			//GetComponent<AudioSource>().Play();
			// Debug.Log("die of falling down");
			death = true;
		}
		if (collision.gameObject.name == "Mask's tear(Clone)") {
			tear_manager.less_tears ();
			Destroy (collision.gameObject);
		}
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetResolution : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        Screen.SetResolution(2880,1440, true);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

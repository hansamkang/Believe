using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DhomaNomalBullet : EnemyNomalBullet {

	// Update is called once per frame
	void Update () {
		if(Director.DhomaState == true)
        {
            Destroy(gameObject);
        }
	}
}

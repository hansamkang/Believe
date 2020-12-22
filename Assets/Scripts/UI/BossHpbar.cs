using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHpbar : MonoBehaviour {
    public GameObject HpGauge;
    public float gauge;
    public GameObject bossNameImage;
	
	
	// Update is called once per frame
	void Update () {
        gauge = HpGauge.GetComponent<Image>().fillAmount;
        if (gauge<=0.1)
        {
            HpGauge.GetComponent<Image>().enabled = false;
            gameObject.GetComponent<Image>().enabled = false;
            bossNameImage.GetComponent<Image>().enabled = false;
        }
	}
}

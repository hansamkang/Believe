using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {
    Animator animator;
    BoxCollider2D co;
	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        co = GetComponent<BoxCollider2D>();
        StartCoroutine("Corutine");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    IEnumerator Corutine()
    {
        animator.Play("Laser");
        yield return new WaitForSeconds(1.15f);
        co.enabled = true;
        yield return new WaitForSeconds(0.3f);
        co.enabled = false;
        yield return new WaitForSeconds(1.6f);
        Destroy(gameObject);
    }
}

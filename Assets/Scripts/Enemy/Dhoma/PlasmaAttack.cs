using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaAttack : MonoBehaviour {
    public ParticleSystem particle;
    BoxCollider2D boxcolider;
    Animator animator;
    
	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        boxcolider = GetComponent<BoxCollider2D>();
        StartCoroutine(plasma());
    }

    IEnumerator plasma()
    {
        animator.Play("PlasmaAttack_01");
        yield return new WaitForSeconds(2.0f);
        animator.Play("PlasmaAttack_idle");
        var emission = particle.emission;
        emission.enabled = true;
        boxcolider.enabled = true;
        yield return new WaitForSeconds(0.2f);
        boxcolider.enabled = false;
        yield return new WaitForSeconds(1.7f);
        Destroy(gameObject);
    }
}

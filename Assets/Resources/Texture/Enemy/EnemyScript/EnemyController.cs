using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	public float animSpeed=1;
	public float speed=1;

	// Use this for initialization
	void Start () {
		GetComponent<Animator> ().speed = animSpeed;
	}

	void Update(){
		transform.Translate (-1 * Time.deltaTime*speed, 0, 0);
	}

}


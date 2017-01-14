using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveFire : MonoBehaviour {

	public float speed = 2;

	void Update(){
		transform.Translate (-1 * Time.deltaTime*speed, 0, 0);
	}
}

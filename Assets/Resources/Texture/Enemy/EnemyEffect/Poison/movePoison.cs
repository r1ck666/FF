using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movePoison : MonoBehaviour {

	public float speed = 1;

	void Update(){
		transform.Translate (-1 * Time.deltaTime*speed, 0, 0);
	}
}

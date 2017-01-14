using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySponer : MonoBehaviour {

	[SerializeField] GameObject[] enemies;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Spone () {
		Instantiate (enemies[0], new Vector3 (0, 0, 0), Quaternion.identity);
	}
}

using UnityEngine;
using System.Collections;

public class shootFire : MonoBehaviour {

	public GameObject fire;

	void Start () {
		StartCoroutine(burst());
	}

	IEnumerator burst(){
		while (true) {
			Instantiate (fire, transform.position, transform.rotation);
			yield return new WaitForSeconds (3.0f);
		}
	}
}
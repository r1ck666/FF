using UnityEngine;
using System.Collections;

public class shootPoison : MonoBehaviour {

	public GameObject poison;

	void Start () {
		StartCoroutine(burst());
	}

	IEnumerator burst(){
		while (true) {
			Instantiate (poison, transform.position, transform.rotation);
			yield return new WaitForSeconds (5.0f);
		}
	}
}
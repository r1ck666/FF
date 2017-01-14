using UnityEngine;
using System.Collections;

public class EffectCtrl : MonoBehaviour {

	Animator animator;
	//出現位置の補正値
	public Vector3 fixedPosition;

	float endTime;
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update () {
		endTime = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
		if (endTime >= 1.0f) {
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D coll) {
		//レイヤー名を取得
	  string layerName = LayerMask.LayerToName(coll.gameObject.layer);
		if (layerName == "Enemy"){
			Destroy(coll.gameObject);
		}
	}

	void SkillStart () {
		GetComponent<Collider2D>().enabled = true;
	}

	void SkillEnd () {
		GetComponent<Collider2D>().enabled = false;
	}
}

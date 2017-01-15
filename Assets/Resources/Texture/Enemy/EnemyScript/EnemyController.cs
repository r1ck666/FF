using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	public float animSpeed=1;
	public float speed=1;
	public int enemyType=0;

	GameObject _playerObj;

	void Start () {
		GetComponent<Animator> ().speed = animSpeed;
		_playerObj = GameObject.Find("UnityChan");
	}

	void Update(){
		Vector3 dir = _playerObj.transform.position - transform.position;
		dir = dir.normalized;

		switch (enemyType)
		{
			case 1:
				transform.Translate(dir.x * Time.deltaTime * speed, dir.y * Time.deltaTime * speed, 0);
				break;

			default:
				transform.Translate(dir.x * Time.deltaTime * speed, 0, 0);
				break;
		}

		Vector3 scl = transform.localScale;
		if (dir.x < 0)
		{
			scl.x = 0.5f;
			transform.localScale = scl;
		}
		else
		{
			scl.x = -0.5f;
			transform.localScale = scl;
		}

		// 落ちたな(確信)
		if (transform.position.y <= -0.5f || transform.position.y >= 10f
			|| transform.position.x <= -20f || transform.position.x >= 20f)
		{
			Destroy(transform.gameObject);

			GameManager mng = GameManager.Instance;
			mng.Score += 100;
		}
	}
}


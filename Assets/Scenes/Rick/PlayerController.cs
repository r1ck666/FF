using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	static int hashSpeed = Animator.StringToHash ("Speed");
	static int hashFallSpeed = Animator.StringToHash ("FallSpeed");
	static int hashGroundDistance = Animator.StringToHash ("GroundDistance");
	static int hashIsCrouch = Animator.StringToHash ("IsCrouch");

	//static int hashDamage = Animator.StringToHash ("Damage");
	//static int hashIsDead = Animator.StringToHash ("IsDead");

	[SerializeField, HideInInspector] Animator animator;
	[SerializeField, HideInInspector] SpriteRenderer spriteRenderer;
	[SerializeField, HideInInspector] Rigidbody2D rig2d;

	[SerializeField] private float characterHeightOffset = 0.2f;
	[SerializeField] LayerMask groundMask;

	// 移動スピード
    [SerializeField] float speed = 5;

	bool canJump = false;
	[SerializeField] float distance;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
		spriteRenderer = GetComponent<SpriteRenderer> ();
		rig2d = GetComponent<Rigidbody2D> ();
	}

	// Update is called once per frame
	void Update () {

		// 右・左
        float x = Input.GetAxisRaw ("Horizontal");

		rig2d.velocity = new Vector2 (x * speed, rig2d.velocity.y);

		bool isDown = Input.GetAxisRaw ("Vertical") < 0;

		var distanceFromGround = Physics2D.Raycast (transform.position, Vector3.down, 1, groundMask);
		distance = distanceFromGround.distance - characterHeightOffset;

		if ( distanceFromGround.distance == 0) {
			canJump = false;
		} else {
			distance = distanceFromGround.distance - characterHeightOffset;
			if ( distance < 0.05f ) {
				canJump = true;
			} else {
				canJump = false;
			}
		}

		if (Input.GetButtonDown ("Jump") && canJump) {
			rig2d.velocity = new Vector2 (rig2d.velocity.x, 5);
		}

		// update animator parameters
		animator.SetBool (hashIsCrouch, isDown);
		animator.SetFloat (hashGroundDistance, distanceFromGround.distance == 0 ? 99 : distanceFromGround.distance - characterHeightOffset);
		animator.SetFloat (hashFallSpeed, rig2d.velocity.y);
		animator.SetFloat (hashSpeed, Mathf.Abs (x));

		//if( Input.GetKeyDown(KeyCode.Z) ){  animator.SetTrigger(hashAttack1); }
		//if( Input.GetKeyDown(KeyCode.X) ){  animator.SetTrigger(hashAttack2); }
		//if( Input.GetKeyDown(KeyCode.C) ){  animator.SetTrigger(hashAttack3); }

		// flip sprite
		if (x != 0)
			spriteRenderer.flipX = x < 0;

	}

	//敵に当たった時に死ぬ
	void OnTriggerEnter2D(Collider2D coll) {
		//レイヤー名を取得
	  string layerName = LayerMask.LayerToName(coll.gameObject.layer);
		if (layerName == "Enemy"){
			Debug.Log("エネミーに当たりました");
			Destroy(coll.gameObject);
			//ここにリザルト遷移のメソッド！
			//GameManager.Instance.GameEnd();
		}
	}

}

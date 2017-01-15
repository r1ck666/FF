using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D))]

public class Skill : MonoBehaviour {


	/// <summary>
	///	Spriteが格納されているPath
	/// </summary>
	[SerializeField] string path = "Smash";

	/// <summary>
	///	出現の補正値
	/// </summary>
	[SerializeField] Vector3 fixedPosition;
	public Vector3 FixedPosition {
		get { return fixedPosition; }
	}

	/// <summary>
	///	次のスプライト画像までの時間
	/// </summary>
	[SerializeField] float nextTime;

	/// <summary>
	///	当たり判定のあるスプライト画像の番号の指定
	/// </summary>
	[SerializeField] int startNum = 2;


	/// <summary>
    ///	一時変数
    /// </summary>
	float time = 0;
	int spriteNum = 0;
	Animator animator;
	SpriteRenderer re;
	Sprite[] sprites;
	// Animationの終了時間
	float endTime;


	void Start () {
		animator = GetComponent<Animator>();
		InitializeSprite ();
	}

	// Update is called once per frame
	void Update () {

		UpdateSprite();

	}

	/// <summary>
	///	スプライトを初期化します
	/// </summary>
	void InitializeSprite () {
		time = 0;
		GetComponent<Collider2D>().enabled = false;
		re = GetComponent<SpriteRenderer> ();
		sprites = Resources.LoadAll<Sprite>("Texture/Effects/" + path);
	}

	/// <summary>
	///	スプライトを描画します
	/// </summary>
	void UpdateSprite () {

		time += Time.deltaTime;
		if (time > nextTime) {

			time = 0;
			re.sprite = sprites[spriteNum];

			// 指定枚数目のSpriteでコライダをアクティブにします
			if ( spriteNum == startNum ) SkillStart();

			spriteNum++;
			// 全てのスプライトが表示されたらオブジェクトを破壊します
			if (spriteNum >= sprites.Length ) {
				SkillEnd();
				Destroy(this.gameObject);
			}
		}
	}

	/// <summary>
	///	敵を消す処理(Trigger)
	/// </summary>
	void OnTriggerEnter2D(Collider2D coll) {
		//レイヤー名を取得
	  string layerName = LayerMask.LayerToName(coll.gameObject.layer);
		if (layerName == "Enemy"){
			Debug.Log("押したぜ");

			float x = Random.Range(0.0f, 10.0f);
			float y = Random.Range(0.0f, 10.0f);
			float z = Random.Range(0.0f, 10.0f);
			Vector3 diff = new Vector3(x, y, z);
			diff = diff.normalized;
			coll.gameObject.GetComponent<Rigidbody2D>().AddForce(diff * 20f, ForceMode2D.Impulse);
		}
	}

	/// <summary>
	///	敵を消す処理(Collision)
	/// </summary>



	void SkillStart () {
		GetComponent<Collider2D>().enabled = true;
	}

	void SkillEnd () {
		GetComponent<Collider2D>().enabled = false;
	}
}

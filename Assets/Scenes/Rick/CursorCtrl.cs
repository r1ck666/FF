using UnityEngine;
using System.Collections;

public class CursorCtrl : MonoBehaviour {

	//マウスの位置座標
	Vector3 position;
	//スクリーン座標をワールド座標に変換した位置座標
	public Vector3 cursorPosition;
	//スキルの格納
	[SerializeField] GameObject skill;
	[SerializeField] GameObject[] skillList;
	//スキルの発動位置
	Vector3 skillPosition;
	EffectCtrl effectCtrl;

	void Start ()
	{
		//カーソルをゲーム画面からはみ出さないようにする
		Cursor.lockState = CursorLockMode.Confined;
		//OSカーソルの非表示
		Cursor.visible = false;
		//
		effectCtrl = skill.GetComponent<EffectCtrl>();
	}

	void Update ()
	{
		//Vector3でマウスの位置座標を取得
		position = Input.mousePosition;
		//Z軸修正
		position.z = 5.0f;
		//マウス位置座標をスクリーン座標からワールド座標に変換する
		cursorPosition = Camera.main.ScreenToWorldPoint(position);
		//ワールド座標に変換されたマウス座標を代入
		transform.position = cursorPosition;
		//if (Input.GetButtonDown("Fire1")){
			//SkillStart();
		//}
	}

	void SkillStart () {
		skillPosition = cursorPosition;
		skillPosition.x += effectCtrl.fixedPosition.x;
		skillPosition.y += effectCtrl.fixedPosition.y;
		skillPosition.z = 0f;
		Instantiate(skill, skillPosition, Quaternion.identity);
	}

	void OnTriggerStay2D(Collider2D coll)
	{
		Debug.Log("カーソルがエネミーに当たりました");

		//レイヤー名を取得
		string layerName = LayerMask.LayerToName(coll.gameObject.layer);
		if (layerName == "Enemy")
		{
			if (Input.GetButtonDown("Fire1"))
			{
				Debug.Log("押したぜ");

				float x = Random.Range(0.0f, 10.0f);
				float y = Random.Range(0.0f, 10.0f);
				float z = Random.Range(0.0f, 10.0f);
				Vector3 diff = new Vector3(x, y, z);
				diff = diff.normalized;
				coll.gameObject.GetComponent<Rigidbody2D>().AddForce(diff * 20f, ForceMode2D.Impulse);
			}
		}

	}

}

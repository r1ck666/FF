using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // LoadSceneを使うために必要！！！！！

public class Game : MonoBehaviour {

	GameObject _gameObj;
	public GameObject GameObj { get { return this._gameObj; } set { this._gameObj = value; } }

	void Awake() {
		Global.SetButtonManager();
		_gameObj = transform.gameObject;
	}

	void Start () {
		Fader.instance.BlackIn();
		GameObject btn = transform.Find("Canvas/ResultButton").gameObject;
		Global.ButtonMng.SetAction(btn, () => {
			Fader.instance.BlackOut();              // フェードアウト
			StartCoroutine(DelayMethod(1.2f));      // 1.2秒後に実行する
		});
	}

	void Update () {
		
	}

	private IEnumerator DelayMethod(float waitTime) {
		yield return new WaitForSeconds(waitTime);  // waitTime後に実行する
		SceneManager.LoadScene("Result");             // シーン切り替え
	}
}

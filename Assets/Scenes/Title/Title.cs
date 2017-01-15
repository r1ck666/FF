using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // LoadSceneを使うために必要！！！！！

public class Title : MonoBehaviour {

	GameObject _titleObj;
	public GameObject TitleObj { get { return this._titleObj; } set { this._titleObj = value; } }

	void Awake() {
		Global.SetButtonManager();
		Global.SetSoundManager();
		Global.SetGameManager();
		_titleObj = transform.gameObject;

		// サウンドをロード
		// "bgm01"をロード。キーは"bgm"とする
		Global.SoundMng.LoadSe("Decision", "se_001");
	}

	void Start() {
		Fader.instance.BlackIn();

		GameObject btn = transform.Find("Canvas/StartButton").gameObject;
		Global.ButtonMng.SetAction(btn, () => {
			Global.SoundMng.PlaySe("Decision");        // BGM再生開始
			Fader.instance.BlackOut();              // フェードアウト
			StartCoroutine(DelayMethod(1.2f));      // 1.2秒後に実行する
		});
	}

	void Update () {
		
	}

	private IEnumerator DelayMethod(float waitTime) {
		yield return new WaitForSeconds(waitTime);  // waitTime後に実行する
		SceneManager.LoadScene("Game");             // シーン切り替え
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // LoadSceneを使うために必要！！！！！

public class Result : MonoBehaviour {

	GameObject _resultObj;
	public GameObject ResultObj { get { return this._resultObj; } set { this._resultObj = value; } }

	// Use this for initialization
	void Start () {
		Fader.instance.BlackIn();

		GameObject btn = transform.Find("Canvas/NextButton").gameObject;

		Global.ButtonMng.SetAction(btn, () => {
			Global.SoundMng.PlaySe("Decision");        // BGM再生開始
			Fader.instance.BlackOut();              // フェードアウト
			StartCoroutine(DelayMethod(1.2f));      // 1.2秒後に実行する
		});
	}

	// Update is called once per frame
	void Update () {
		
	}

	private IEnumerator DelayMethod(float waitTime) {
		yield return new WaitForSeconds(waitTime);  // waitTime後に実行する
		SceneManager.LoadScene("Title");             // シーン切り替え
	}

}

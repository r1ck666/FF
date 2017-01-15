using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour {
    //Player系
    public GameObject player;
    private Transform playerTransform;

    private int createGroundPos = 10;

    void Start () {
        playerTransform = player.GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        //ジャンプした時だけカメラの位置が動く（上手く飛び乗れなかった場合でもカメラは上にある）
        //プレイヤーの高さ取得
        float playerWidth = playerTransform.position.x;

		Vector2 sizeMax = new Vector2(transform.position.x + 1.0f, transform.position.y + 0.1f - 1.6f);
		Vector2 sizeMin = new Vector2(transform.position.x - 0.8f, transform.position.y - 0.1f - 1.6f);

		if (playerTransform.position.x > sizeMax.x) {
			Vector3 pos = transform.position;
			pos.x += playerTransform.position.x - sizeMax.x;
			transform.position = pos;
		}

		if (playerTransform.position.x < sizeMin.x)
		{
			Vector3 pos = transform.position;
			pos.x += playerTransform.position.x - sizeMin.x;
			transform.position = pos;
		}

		if (playerTransform.position.y > sizeMax.y)
		{
			Vector3 pos = transform.position;
			pos.y += playerTransform.position.y - sizeMax.y;
			transform.position = pos;
		}
	
		if (playerTransform.position.y < sizeMin.y)
		{
			Vector3 pos = transform.position;
			pos.y += playerTransform.position.y - sizeMin.y;
			transform.position = pos;
		}
		////カメラ
		//float cameraWidth = transform.position.x + 0.5f;

		//      //Lerp(from,to,割合）最小値と最大値の間の値をとる
		//      //第三引数には0.0～1.0をいれ値を決定0.5は真ん中の値取得
		//      float newWidth = Mathf.Lerp(cameraWidth, playerWidth, 0.5f);

		//      float temp = 0.0f;
		//      //現在のカメラの高さよりプレイヤーの高さのほうが高くなったらカメラの高さをnewHeightにする
		//      //但しｘとｚ軸は変更しない
		//      if (playerWidth > cameraWidth)
		//      {
		//          temp = transform.position.y;
		//          transform.position += new Vector3(newWidth, transform.position.y, transform.position.z);
		//      }

		/*   

			if (playerWidth >= createGroundPos)
			{
				CreateGround();
				createGroundPos += 10;
			}


			/*        //プレイヤーの座標に差分を加えたもの（ずっとついてくるタイプ）
					 transform.position = player.transform.position + offset;
					 */

	}
    /*
    void CreateGround()
    {

    }*/
}

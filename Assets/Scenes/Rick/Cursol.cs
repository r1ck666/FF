using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursol : MonoBehaviour {

	Sprite[] sprites;
	SpriteRenderer re;
	float time;
	int spriteNum = 0;

	string path = "uni";
	[SerializeField] float nextTime = 0.5f;

	// Use this for initialization
	void Start () {
		time = 0;
		re = GetComponent<SpriteRenderer> ();
		sprites = Resources.LoadAll<Sprite>("Texture/MouseCursol/" + path);
	}

	// Update is called once per frame
	void Update () {
		UpdateSprite();
	}

	void UpdateSprite() {
		time += Time.deltaTime;
		if (time > nextTime) {
			time = 0;
			re.sprite = sprites[spriteNum];
			spriteNum++;

			if (spriteNum == sprites.Length ) spriteNum = 0;
		}
	}
}

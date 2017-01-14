using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralysis : MonoBehaviour {

   public PlayerController target;
    public GameObject player;
    public Animator anim;

    void OnTriggerEnter2D(Collider2D other)
    {
        this.gameObject.SetActive(false);


        // プレーヤーの動きを止める。
        GameObject obj = other.gameObject;
        target = obj.GetComponent<PlayerController>();
        other.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        anim = target.GetComponent<Animator>();
        target.enabled = false;
        anim.enabled = false;
    
        // 2秒後にReleaseメソッドを呼び出す。
        Invoke("Release", 2.0f);
    }

    void Release()
    {
        target.enabled = true;
        anim.enabled = true;
    }
}

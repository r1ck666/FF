using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*public enum Floor
{
    ACC,
    PARALYSIS
}*/


public class Acc : MonoBehaviour {

 //   public Floor floor = Floor.ACC;

    void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(2000, 0), ForceMode2D.Force);
    }
}

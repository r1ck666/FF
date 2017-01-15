using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Text))]
public class StartButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Text startText {
        get {
            return GetComponent<Text>();
        }
    }

    // オブジェクトの範囲内にマウスポインタが入った際に呼び出されます。
    // this method called by mouse-pointer enter the object.
    public void OnPointerEnter(PointerEventData eventData)
    {
        startText.fontSize = 35;
    }

    // オブジェクトの範囲内からマウスポインタが出た際に呼び出されます。
    // 
    public void OnPointerExit(PointerEventData eventData)
    {
        startText.fontSize = 30;
    }
}

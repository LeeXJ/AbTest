using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardMake : MonoBehaviour
{
    public Text PointLeft;

    private int point = 100;

	// Use this for initialization
	void Start ()
	{
	    PointLeft.text = string.Format("属性点剩余:{0}", point);
	}
	
	// Update is called once per frame
	void Update () {
		
	}


}

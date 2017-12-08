using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class CardMake : MonoBehaviour
{
    public Text PointLeft;
    public Text PointPhy;

    private int point = 100;
    private int pointphy = 50;

    // Use this for initialization
    void Start()
    {
        Refresh();
    }

    private void Refresh()
    {
        PointLeft.text = string.Format("属性点剩余: {0}", point);
        PointPhy.text = string.Format("物理攻击力: {0}", pointphy);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPhysicalAdd()
    {
        point--;
        pointphy++;
        Refresh();
    }

    public void OnPhysicalRemove()
    {
        point++;
        pointphy--;
        Refresh();
    }


}

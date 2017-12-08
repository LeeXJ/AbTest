using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class CardMake : MonoBehaviour
{
    public Text PointLeft;
    public Text PointPhy;
    public Image Img;
    public InputField URLInput;

    private int point = 100;
    private int pointphy = 50;

    // Use this for initialization
    void Start()
    {
        Img.enabled = false;
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

    public void OnLoadImg()
    {
        StartCoroutine(LoadImgFromURL());
    }

    public IEnumerator LoadImgFromURL()
    {
        string url = URLInput.text;
        WWW www = new WWW(url);
        yield return www;

        Texture2D texture = new Texture2D(200, 320);
        texture.LoadImage(www.bytes);
        yield return new WaitForSeconds(0.01f);
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        Img.sprite = sprite;
        yield return new WaitForSeconds(0.01f);
        Resources.UnloadUnusedAssets(); //一定要清理游离资源。
        Img.enabled = true;
    }
}

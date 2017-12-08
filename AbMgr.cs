//using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class AbMgr : MonoBehaviour
{
    private GameObject go;
    public Button ButtonBuild;
    public Button ButtonCompare;

    void Awake()
    {
        ButtonBuild.onClick.AddListener(OnClickBuild);
        ButtonCompare.onClick.AddListener(OnClickCompare);
    }

    private void Start()
    {
#if UNITY_EDITOR
        Resources.UnloadUnusedAssets();
//        AssetDatabase.Refresh();
//        UnityEditor.build
#endif
        //        ab2 = AssetBundle.LoadFromFile("Assets/AssetBundles/share_assets/mats/mat2.mat");
        //        var abname = ab.GetAllAssetNames();
        //        foreach (var name in abname)
        //        {
        //            Debug.Log("===> " + name);
        //        }
    }

    // Update is called once per frame
    private void OnGUI()
    {
        if (GUI.Button(new Rect(0, 0, 100, 20), "Load"))
        {
            var ab = AssetBundle.LoadFromFile("Assets/AssetBundles/assets/prbs/1.prefab");
            var prb = ab.LoadAsset<GameObject>("1");
            go = Instantiate(prb);
            go.GetComponent<MonoBase>().Init(ab, "test");
        }

        if (GUI.Button(new Rect(0, 30, 100, 20), "Delete"))
        {
            Destroy(go);
        }

        if (GUI.Button(new Rect(0, 60, 200, 40), "Compare"))
        {
            Debug.Log("Compare");
            compare();
        }
    }

    private void compare()
    {
        var ab = AssetBundle.LoadFromFile("Assets/AssetBundles/20171207173748/20171207173748");
        Debug.Log(ab.name);
    }

    public void OnClickBuild()
    {
        
    }

    public void OnClickCompare()
    {
        Debug.Log("===> Compare");
    }

    #region  function

    #endregion
}
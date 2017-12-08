using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using UnityEditor;
using UnityEditor.UI;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Experimental.Director;
using Debug = UnityEngine.Debug;

public class AbEditor : EditorWindow
{
    public static AbEditor window;
    public static Stopwatch watch;
    private static float progress = 0;
    [MenuItem("Thz/AbBuild")]
    public static void Build()
    {
        Debug.Log("Build");
        //        window = (AbEditor)EditorWindow.GetWindow(typeof(AbEditor));
        //        window.Show();
        //        progress = 0;
        ClearBundleName();
        SetBundleName();
        DoBuild();
        UnityEngine.Debug.LogError("===> BuildAsset Done!");
    }

    public static void ClearBundleName()
    {
        watch = new Stopwatch();
        watch.Start();
        var bundlenames = AssetDatabase.GetAllAssetBundleNames();
        foreach (var s in bundlenames)
            AssetDatabase.RemoveAssetBundleName(s, true);
        AssetDatabase.Refresh();
    }

    public static void SetBundleName()
    {
        var prbspath = "Assets/prbs/";
        var dir = new DirectoryInfo(prbspath);
        var files = dir.GetFiles("*.prefab");
        foreach (var fileInfo in files)
        {
            var abname = fileInfo.Name;
            var abpath = prbspath + abname;
            var im = AssetImporter.GetAtPath(abpath);
            im.assetBundleName = abpath;
            Debug.Log("===> " + abname);
            var deps = AssetDatabase.GetDependencies(abpath, false);
            foreach (var dep in deps)
            {
                if (dep.EndsWith(".cs"))
                    continue;
                Debug.Log("   ===> deps: " + dep);
                var imdep = AssetImporter.GetAtPath(dep);
                //未被其他资源依赖
                if (string.IsNullOrEmpty(imdep.assetBundleName))
                    imdep.assetBundleName = abpath;
                //被其他资源依赖，打成共享
                else
                    imdep.assetBundleName = "share_" + imdep.assetPath;
            }
        }
    }

    public static void DoBuild()
    {
        DateTime date = DateTime.Now;
        string time = string.Format("{0:yyyyMMddHHmmss}", date);
        var assetBundleDirectory = "Assets/AssetBundles/" + time + "/";
        if (!Directory.Exists(assetBundleDirectory))
            Directory.CreateDirectory(assetBundleDirectory);
        BuildPipeline.BuildAssetBundles(assetBundleDirectory, BuildAssetBundleOptions.ChunkBasedCompression,
            BuildTarget.StandaloneWindows64);
        AssetDatabase.Refresh();
        progress = 1;
    }

    [MenuItem("Thz/AbCompare")]
    public static void AbCompare()
    {
        //ab1 is old
        //ab2 is new
        Debug.LogError("===> AbCompare Start!");
        string abpath = "Assets/AssetBundles";
        string c1 = "20171207173748";
        string c2 = "20171208102526";
        var ab1 = AssetBundle.LoadFromFile(string.Format("{0}/{1}/{2}", abpath, c1, c1));
        var ab2 = AssetBundle.LoadFromFile(string.Format("{0}/{1}/{2}", abpath, c2, c2));

        var manifest1 = ab1.LoadAsset("assetbundlemanifest") as AssetBundleManifest;
        var manifest2 = ab2.LoadAsset("assetbundlemanifest") as AssetBundleManifest;

        var abs1 = manifest1.GetAllAssetBundles();
        var abs2 = manifest2.GetAllAssetBundles();

        List<string> abAdd = new List<string>();
        List<string> abRemove = new List<string>();
        List<string> abUpdate = new List<string>();
        //Find Add
        foreach (var abname in abs2)
        {
            if (!abs1.Contains(abname))
            {
                abAdd.Add(abname);
            }
        }

        //Remove
        foreach (var abname in abs1)
        {
            if (!abs2.Contains(abname))
            {
                abRemove.Add(abname);
            }
        }

        //Update
        foreach (var abname in abs1)
        {
            if (abs2.Contains(abname))
            {
                if (manifest1.GetAssetBundleHash(abname) != manifest2.GetAssetBundleHash(abname))
                {
                    abUpdate.Add(abname);
                }
            }
        }

        LogArray(abAdd.ToArray());
        LogArray(abRemove.ToArray());
        LogArray(abUpdate.ToArray());

        //        LogArray(manifest.GetAllAssetBundles());
        //        var bundles = manifest.GetAllAssetBundles();
        //        for (int i = 0; i < bundles.Length; ++i)
        //        {
        //            Debug.Log(manifest.GetAssetBundleHash(bundles[i]));
        //        }

        //        Debug.Log("===> " + ab2.mainAsset);

        //                var c1 = AssetDatabase.LoadAssetAtPath("AssetBundles/20171207173748/20171207173748.manifest", typeof(AssetBundleManifest));
        //                Debug.Log(c1.name);
        //                AssetBundleManifest c1 = AssetImporter.GetAtPath("AssetBundles/20171207173748/20171207173748") as AssetBundleManifest;
    }

    //    void OnGUI()
    //    {
    //        window.Focus();
    //
    //        BoxHorizontal((v) =>
    //        {
    //            ButtonVertical(r =>
    //            {
    //                if (GUI.Button(r, GUIContent.none))
    //                {
    //                    ClearBundleName();
    //                    SetBundleName();
    //                    DoBuild();
    //                }
    //                GUILayout.Label("Build");
    //            });
    //            ButtonVertical(r =>
    //            {
    //                if (GUI.Button(r, GUIContent.none))
    //                    Debug.Log("Upload");
    //                GUILayout.Label("UpLoad");
    //            });
    //        });
    //
    //        BoxVertical(v =>
    //        {
    //            NodeVertical((r) =>
    //            {
    //                if (null != watch)
    //                {
    //                    EditorGUI.ProgressBar(r, watch.ElapsedMilliseconds, watch.ElapsedMilliseconds + "%");
    //                }
    //                GUILayout.Space(18);
    //            });
    //
    //            if (null != watch)
    //            {
    //                TimeSpan ts = new TimeSpan(0, 0, (int)watch.ElapsedMilliseconds / 1000);
    //                string t = string.Format("{0}:{1}:{2}", (int)ts.TotalHours, ts.Minutes, ts.Seconds);
    //                GUILayout.Label("Build时间: " + t);
    //            }
    //            else
    //            {
    //                GUILayout.Label("Build时间: 0");
    //            }
    //        });
    //    }

    public static void BoxHorizontal(Action<Rect> func)
    {
        Rect r = EditorGUILayout.BeginHorizontal("box");
        func(r);
        EditorGUILayout.EndHorizontal();
    }

    public static void BoxVertical(Action<Rect> func)
    {
        Rect r = EditorGUILayout.BeginVertical("box");
        func(r);
        EditorGUILayout.EndVertical();
    }

    public static void ButtonVertical(Action<Rect> func)
    {
        Rect r = (Rect)EditorGUILayout.BeginVertical("Button");
        func(r);
        EditorGUILayout.EndVertical();
    }

    public static void NodeVertical(Action<Rect> func)
    {
        Rect r = (Rect)EditorGUILayout.BeginVertical("TE NodeBackground");
        func(r);
        EditorGUILayout.EndVertical();
    }

    public static void LogArray(string[] strings)
    {
        for (int i = 0; i < strings.Length; i++)
        {
            Debug.Log("===> " + strings[i]);
        }
    }
}
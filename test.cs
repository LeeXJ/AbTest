using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class test : MonoBehaviour
{
    public GameObject img;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(0, 0, 100, 20), "start"))
        {
            StartCoroutine(animPos());
            StartCoroutine(animAngle());
        }
    }

    private IEnumerator animPos()
    {
        Vector3 sp = img.transform.localPosition;
        Vector3 ep = sp + new Vector3(0, 0, -700);
        yield return Vector3Learp(img.transform, sp, ep, 20);
    }

    private IEnumerator animAngle()
    {
        Quaternion sq = img.transform.localRotation;
        float angle;
        Vector3 axis;
        sq.ToAngleAxis(out angle, out axis);
        Quaternion eq = Quaternion.AngleAxis(angle + 70, axis);
        yield return RotationLearp(img.transform, sq, eq, 20);
        yield return Rota(img.transform, 3, 10000);
    }

    private WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();
    public IEnumerator Vector3Learp(Transform tr, Vector3 s, Vector3 e, int fixedtime)
    {
        float currenttime = 0;
        while (currenttime < fixedtime)
        {
            currenttime += 1;
            tr.localPosition = Vector3.Lerp(s, e, currenttime / fixedtime);
            yield return waitForFixedUpdate;
        }
    }

    public IEnumerator RotationLearp(Transform tr, Quaternion s, Quaternion e, int fixedtime)
    {
        float currenttime = 0;
        while (currenttime < fixedtime)
        {
            currenttime += 1;
            tr.localRotation = Quaternion.Lerp(s, e, currenttime / fixedtime);
            yield return waitForFixedUpdate;
        }
    }

    public IEnumerator Rota(Transform tr, float anglespeed, int fixedtime)
    {
        float currenttime = 0;
        while (currenttime < fixedtime)
        {
            currenttime += 1;
            tr.Rotate(tr.up, anglespeed, Space.World);
            yield return waitForFixedUpdate;
        }
    }
}

using UnityEngine;

public class Abmono : MonoBehaviour
{
    public AssetBundle Ab;

    private void OnDestroy()
    {
        Debug.Log("===> UnLoad");
        Ab.Unload(true);
        Ab = null;
    }
}
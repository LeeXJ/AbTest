using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoBase : MonoBehaviour
{
    private AssetBundle ab;

    public virtual void Init(AssetBundle pAb, string pName = null)
    {
        ab = pAb;
        if (null != pName)
        {
            gameObject.name = pName;
        }
    }

    public virtual void OnDestroy()
    {
        ab.Unload(true);
        ab = null;
    }
}

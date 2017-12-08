using UnityEngine;
public class Login : MonoBase
{
    public override void OnDestroy()
    {
        base.OnDestroy();
        Debug.Log("Destroy");
    }
}
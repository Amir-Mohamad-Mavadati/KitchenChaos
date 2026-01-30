using UnityEngine;

public class LoadingCallBack : MonoBehaviour
{
    private bool IsLoaded = true;
    void Update()
    {
        if (IsLoaded)
        {
            IsLoaded = false;
            Loader.LoadingCallBack();
        }
    }
}

using UnityEngine;

public class UI_Manager : MonoSingleton<UI_Manager>
{
    public T Get<T>()
    {
        return transform.GetComponentInChildren<T>();
    }
}
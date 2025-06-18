using UnityEngine;
using System;

// コピペ用
public abstract class SingletonMonoBehaviour<T> : 
    MonoBehaviour where T :
    MonoBehaviour
{

    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                Type t = typeof(T);

                var found = FindObjectsByType<T>(FindObjectsSortMode.None);
                if (found.Length == 0)
                {
                    Debug.LogError(t + " をアタッチしているGameObjectはありません");
                    return null;
                }
                instance = found[0];
            }

            return instance;
        }
    }


    virtual protected void Awake()
    {
        // 他のゲームオブジェクトにアタッチされているか調べる
        // アタッチされている場合は破棄する。
        CheckInstance();
    }

    protected bool CheckInstance()
    {
        if (instance == null)
        {
            instance = this as T;
            return true;
        }
        else if (Instance == this)
        {
            return true;
        }
        Destroy(gameObject);
        return false;
    }
}

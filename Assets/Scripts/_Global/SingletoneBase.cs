using UnityEngine;

public class SingletoneBase<T> : MonoBehaviour where T : MonoBehaviour
{
    // 프로퍼티
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<T>();
                if (_instance == null)
                {
                    string typeName = typeof(T).FullName;
                    GameObject go = new(typeName);
                    _instance = go.AddComponent<T>();

                }

                DontDestroyOnLoad(_instance);

            }

            return _instance;
        }
    }

    void Awake()
    {
        Init();
    }

    public virtual void Init()
    {

    }

    protected virtual void OnDestroy()
    {
        _instance = null;
    }

    protected virtual void OnApplicationQuit()
    {
        _instance = null;
    }
}


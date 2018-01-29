using System;
using System.IO;
using UnityEngine;

public class BaseDataMgr<T> : MonoBehaviour where T : BaseDataMgr<T>
{
    #region 单例

    private static T instance = null;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GetExist(null);
                if (instance == null)
                    instance = Create(null);
                else
                    instance.Init();
            }
            return instance;
        }
    }

    public static T Create(Transform parent)
    {
        instance = GetExist(parent);
        if (instance != null)
        {
            instance.Init();
            return instance;
        }
        //create it
        Transform obj = new GameObject(typeof(T).Name).transform;
        obj.parent = parent;
        obj.localEulerAngles = Vector3.zero;
        obj.localPosition = Vector3.zero;
        instance = obj.gameObject.AddComponent<T>();
        instance.Init();
        return instance;
    }

    private static T GetExist(Transform parent)
    {
        if (instance) return instance;
        if (parent == null)
        {
            return FindObjectOfType<T>();
        }
        else
        {
            return parent.GetComponentInChildren<T>();
        }
    }

    #endregion


    public EventDispatcher dispatcher;
    protected string file_path;
    protected BaseData data;
    private bool _dirty;

    public bool Inited { private set; get; }

    public virtual void Init()
    {
        if (Inited)
        {
            return;
        }
        DontDestroyOnLoad(this);
        Inited = true;
        dispatcher = new EventDispatcher();
    }

    public virtual void UnInit()
    {
        if (!Inited)
        {
            return;
        }
        Inited = false;
    }

    protected void MarkDirty()
    {
        _dirty = true;
    }

    void Update()
    {
        if(_dirty)
        {
            _dirty = false;
            dispatcher.SendEvent(BaseDataEvent.DATA_CHANGED);
        }
    }

    protected void Read(Type t)
    {
        string filePath = Path.Combine(Application.persistentDataPath, file_path);
        Debug.Log(filePath);
        if (!File.Exists(filePath))
        {
            FileOperate.FileCreate(filePath);
        }
        string content = FileOperate.ReadFile(filePath);
        if (string.IsNullOrEmpty(content))
        {
            data = t.Assembly.CreateInstance(t.ToString()) as BaseData;
        }
        else
        {
            data = JsonUtility.FromJson(content, t) as BaseData;
        }
    }

    public void Save()
    {
        string filePath = Path.Combine(Application.persistentDataPath, file_path);
        string json = JsonUtility.ToJson(data);
        FileOperate.WriteFile(filePath, json);
    }



    #region event

    public static void SendEvent(short type)
    {
        Instance.dispatcher.SendEvent(type);
    }

    public static void SendEvent<T>(short type, T msg)
    {
        Instance.dispatcher.SendEvent<T>(type, msg);
    }

    public static void AddEventHandler(short type, EventHandler handler)
    {
        Instance.dispatcher.AddEventHandler(type, handler);
    }

    public static void AddEventHandler<T>(short type, EventHandler<T> handler)
    {
        Instance.dispatcher.AddEventHandler<T>(type, handler);
    }

    public static void AddOneShotEventHandler(short type, EventHandler handler)
    {
        Instance.dispatcher.AddOneShotEventHandler(type, handler);
    }

    public static void AddOneShotEventHandler<T>(short type, EventHandler<T> handler)
    {
        Instance.dispatcher.AddOneShotEventHandler<T>(type, handler);
    }

    public static void RemoveEventHandler(short type, EventHandler handler)
    {
        Instance.dispatcher.RemoveEventHandler(type, handler);
    }

    public static void RemoveEventHandler<T>(short type, EventHandler<T> handler)
    {
        Instance.dispatcher.RemoveEventHandler<T>(type, handler);
    }

    #endregion

}

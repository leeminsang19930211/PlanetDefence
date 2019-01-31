using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalGameObjectMgr : MonoBehaviour
{
    private static int m_nDays = PlayerPrefs.GetInt("Days", 1);

    public static int nDays
    {
        get
        {
            return m_nDays;
        }
    }

    public static CardInfo SelectedCard = null;
    public static bool bSelected = false;

    private static GlobalGameObjectMgr m_inst = null;

    private Dictionary<string, GameObject> m_gameObjects = new Dictionary<string, GameObject>();

    public static GlobalGameObjectMgr Inst
    {
        get
        {
            if (m_inst == null)
            {
                GameObject container = new GameObject();
                container.name = "GlobalGameObjectMgr";
                m_inst = container.AddComponent<GlobalGameObjectMgr>() as GlobalGameObjectMgr;
                DontDestroyOnLoad(container);
            }

            return m_inst;
        }
    }

    // GameObjectMgr에 등록된 모든 오브젝트는 씬이 변해도 파괴되지 않는다.
    public bool RegisterGameObject(string key, GameObject gameobj, bool active)
    {
        if (m_gameObjects.ContainsKey(key))
            return false;

        gameobj.SetActive(active);

        DontDestroyOnLoad(gameobj);

        m_gameObjects.Add(key, gameobj);

        return true;
    }

    public GameObject FindGameObject(string key)
    {
        GameObject obj = null;

        m_gameObjects.TryGetValue(key,out obj);

        return obj;
    }

    public bool SetGameObectActive(string key, bool active)
    {
        GameObject obj = null;

        m_gameObjects.TryGetValue(key, out obj);

        if (obj == null)
            return false;

        obj.SetActive(active);

        return true;
    }

    public bool DontDestroyOnLoad(string key)
    {
        GameObject obj = null;

        m_gameObjects.TryGetValue(key, out obj);

        if (obj == null)
            return false;

        DontDestroyOnLoad(obj);

        return true;
    }

    public bool MoveGameObjectToScene(string key, string sceneName)
    {
        GameObject obj = null;

        m_gameObjects.TryGetValue(key, out obj);

        if (obj == null)
            return false;

        Scene scene = SceneManager.GetSceneByName(sceneName);

        if (scene == null)
            return false;

        SceneManager.MoveGameObjectToScene(obj, scene);

        return true;
    }
}

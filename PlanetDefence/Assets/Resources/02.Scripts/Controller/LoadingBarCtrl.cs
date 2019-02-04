using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingBarCtrl: MonoBehaviour
{
    private struct LoadInfo
    {
        public string path;
        public string prefabKey;

        public LoadInfo(string _path, string _prefabKey)
        {
            path = _path;
            prefabKey = _prefabKey;
        }
    }
   
    private Image m_progressImage = null;
    private List<ResourceRequest> m_requests = new List<ResourceRequest>();
    private int m_maxRsrc = 3; // 로딩하는 총 리소스 량.CalculateProgress() 에서 쓰인다.

    void Start()
    {
        Image[] images = GetComponentsInChildren<Image>();
        m_progressImage = images[1];  // 첫번째 이미지가 ProgressImage 이다

        StartCoroutine("LoadPrefab", new LoadInfo("03.Prefabs/Earlier/Lobby", "Lobby"));
        StartCoroutine("LoadPrefab", new LoadInfo("03.Prefabs/Earlier/Choice", "Choice"));
        StartCoroutine("LoadPrefab", new LoadInfo("03.Prefabs/Earlier/Battle", "Battle"));
        StartCoroutine("LoadPrefab", new LoadInfo("03.Prefabs/Earlier/Turrets", "Turrets"));
        StartCoroutine("LoadPrefab", new LoadInfo("03.Prefabs/Earlier/SpaceShips", "SpaceShips"));
        StartCoroutine("LoadPrefab", new LoadInfo("03.Prefabs/Earlier/BattleStatic", "BattleStatic"));

        // 로딩바 테스트용으로 속도 늦추기
        //m_requests.Add(Resources.LoadAsync("99.External/Standard Assets"));
    }

    void Update()
    {
        float progress = CalculateProgress();
        m_progressImage.fillAmount = progress;

#if UNITY_EDITOR || UNITY_STANDALONE

        if (progress >= 1f)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                BeforeChaningScene();

                SceneLoader.LoadScene("Lobby");
            }
        }

#else

        if(Input.touchCount > 0)
        {
            if (progress >= 1f)
            {
                SceneLoader.LoadScene("Lobby");
            }
        }
#endif

    }

    private IEnumerator LoadPrefab(LoadInfo info)
    {
        ResourceRequest request = Resources.LoadAsync(info.path);

        while (!request.isDone)
        {
            yield return null;
        }

        PreparePrefab(info.prefabKey, request.asset);

        m_requests.Add(request);
    }

    private bool PreparePrefab(string key, Object prefab)
    {
        GameObject obj = Instantiate(prefab) as GameObject;

        if (obj == null)
            return false;

        GlobalGameObjectMgr.Inst.RegisterGameObject(key, obj, false);

        return true;
    }

    private float CalculateProgress()
    {
        float sum = 0;

        foreach(ResourceRequest request in m_requests)
        {
            sum += request.progress;
        }

        return sum / m_maxRsrc;
    }

    private void BeforeChaningScene()
    {

    }
}

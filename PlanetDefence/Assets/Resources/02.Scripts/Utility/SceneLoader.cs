using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private static string m_curScene = "PreLoading";
    private static string m_prevScene = m_curScene;

    public static string PrevScene
    {
        get
        {
            return m_prevScene;
        }
    }

    // 모든 씬 전환은 이함수를 통해서 한다. 씬전환시 필요한 것들을 여기에 작성하면 된다.
    public static void LoadScene(string nextScene)
    {
        if (m_curScene == nextScene)
            return;

        m_prevScene = m_curScene;

        if (m_curScene == "PreLoading" && nextScene == "Lobby")
        {
            SceneManager.LoadScene("Lobby");          
        }
        else if (m_curScene == "Lobby" && nextScene == "Choice")
        {          
            SceneManager.LoadScene("Choice");            
        }
        else if (m_curScene == "Lobby" && nextScene == "Battle")
        { 
            SceneManager.LoadScene("Battle");
        }
        else if (m_curScene == "Choice" && nextScene == "Battle")
        {            
            SceneManager.LoadScene("Battle");
        }
        else if(m_curScene == "Battle" && nextScene == "Lobby")
        {
            SceneManager.LoadScene("Lobby");                  
        }
        else if(m_curScene == "Battle" && nextScene == "Choice")
        {
            SceneManager.LoadScene("Choice");
        }
        else
        {
            Debug.LogError("The scene change is invalid");
        }

        m_curScene = nextScene;
    }

    public static void OnStartScene()
    {
        if(m_prevScene == "PreLoading")
        {
            GlobalGameObjectMgr.Inst.SetGameObectActive("PreLoading", false);
        }
        else if(m_prevScene == "Lobby")
        {
            GlobalGameObjectMgr.Inst.SetGameObectActive("Lobby", false);
        }
        else if (m_prevScene == "Choice")
        {
            GlobalGameObjectMgr.Inst.SetGameObectActive("Choice", false);

        }
        else if (m_prevScene == "Battle")
        {
            GlobalGameObjectMgr.Inst.SetGameObectActive("Battle", false);
            GlobalGameObjectMgr.Inst.SetGameObectActive("BattleStatic", false);
        }

        if (m_curScene == "PreLoading")
        {
            GlobalGameObjectMgr.Inst.SetGameObectActive("PreLoading", true);
        }
        else if (m_curScene == "Lobby")
        {
            GlobalGameObjectMgr.Inst.SetGameObectActive("Lobby", true);
        }
        else if (m_curScene == "Choice")
        {
            GlobalGameObjectMgr.Inst.SetGameObectActive("Choice", true);

        }
        else if (m_curScene == "Battle")
        {
            GlobalGameObjectMgr.Inst.SetGameObectActive("Battle", true);
            GlobalGameObjectMgr.Inst.SetGameObectActive("BattleStatic", true);
        }
    }
}

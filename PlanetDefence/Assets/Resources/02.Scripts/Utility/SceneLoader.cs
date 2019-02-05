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

            GlobalGameObjectMgr.Inst.SetGameObectActive("Battle", false);
            GlobalGameObjectMgr.Inst.SetGameObectActive("Lobby", true);
        }
        else if (m_curScene == "Lobby" && nextScene == "Choice")
        {          
            SceneManager.LoadScene("Choice");
            GlobalGameObjectMgr.Inst.SetGameObectActive("Lobby", false);
            GlobalGameObjectMgr.Inst.SetGameObectActive("Choice", true);
        }
        else if (m_curScene == "Lobby" && nextScene == "Battle")
        {
            SceneManager.LoadScene("Battle");

            GlobalGameObjectMgr.Inst.SetGameObectActive("Lobby", false);
            GlobalGameObjectMgr.Inst.SetGameObectActive("Battle", true);
            GlobalGameObjectMgr.Inst.SetGameObectActive("BattleStatic", true);

        }
        else if (m_curScene == "Choice" && nextScene == "Battle")
        {            
            SceneManager.LoadScene("Battle");

            GlobalGameObjectMgr.Inst.SetGameObectActive("Choice", false);
            GlobalGameObjectMgr.Inst.SetGameObectActive("Battle", true);
            GlobalGameObjectMgr.Inst.SetGameObectActive("BattleStatic", true);

        }
        else if(m_curScene == "Battle" && nextScene == "Lobby")
        {
            SceneManager.LoadScene("Lobby");

            GlobalGameObjectMgr.Inst.SetGameObectActive("Battle", false);
            GlobalGameObjectMgr.Inst.SetGameObectActive("Lobby", true);
            GlobalGameObjectMgr.Inst.SetGameObectActive("BattleStatic", false);
        }
        else if(m_curScene == "Battle" && nextScene == "Choice")
        {
            SceneManager.LoadScene("Choice");

            GlobalGameObjectMgr.Inst.SetGameObectActive("Battle", false);
            GlobalGameObjectMgr.Inst.SetGameObectActive("Choice", true);
            GlobalGameObjectMgr.Inst.SetGameObectActive("BattleStatic", false);
        }
        else
        {
            Debug.LogError("The scene change is invalid");
        }

        m_curScene = nextScene;
    }
}

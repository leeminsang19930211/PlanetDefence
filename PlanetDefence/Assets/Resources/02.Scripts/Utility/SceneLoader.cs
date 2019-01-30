using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private static string m_curScene = "PreLoading";

    // 모든 씬 전환은 이함수를 통해서 한다. 씬전환시 필요한 것들을 여기에 작성하면 된다.
    public static void LoadScene(string nextScene)
    {
        if (m_curScene == nextScene)
            return;

        if (m_curScene == "PreLoading" && nextScene == "Lobby")
        {
            SceneManager.LoadScene("Lobby");
        }
        else if (m_curScene == "Lobby" && nextScene == "Choice")
        {          
            SceneManager.LoadScene("Choice");
            GlobalGameObjectMgr.Inst.DontDestroyOnLoad("Lobby");
        }
        else if(m_curScene == "Choice" && nextScene == "Battle")
        {            
            SceneManager.LoadScene("Battle");    
            GlobalGameObjectMgr.Inst.DontDestroyOnLoad("Choice");
        }
        else if(m_curScene == "Battle" && nextScene == "Lobby")
        {
            SceneManager.LoadScene("Lobby");
            GlobalGameObjectMgr.Inst.DontDestroyOnLoad("Battle");
        }
        else
        {
            Debug.LogError("The scene change is invalid");
        }

        m_curScene = nextScene;
    }
}

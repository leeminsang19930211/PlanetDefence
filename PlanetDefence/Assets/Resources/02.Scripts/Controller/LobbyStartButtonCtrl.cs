using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyStartButtonCtrl : MonoBehaviour
{
    public Text m_text = null;
  
    public void OnClick()
    {
        if (GlobalGameObjectMgr.Inst.Battle)
        {
            SceneLoader.LoadScene("Battle");
        }
        else
        {
            SceneLoader.LoadScene("Choice");
        }
    }

    public void OnEnable()
    {
        if (GlobalGameObjectMgr.Inst.Battle)
        {
            m_text.text = "계속";
        }
        else
        {
            m_text.text = "시작";
        }
    }
}

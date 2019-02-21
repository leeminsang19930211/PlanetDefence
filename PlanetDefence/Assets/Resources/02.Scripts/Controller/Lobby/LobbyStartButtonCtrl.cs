using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyStartButtonCtrl : MonoBehaviour
{
    public Text m_text = null;

    public int m_fontSize = 0;

    public void OnEnable()
    { 
        if (GlobalGameObjectMgr.Inst.Battle == true)
        {
            m_text.text = "CONTINUE";
        }
        else
        {
            m_text.text = "START";
        }
    }

    public void OnClick()
    {
        if (GlobalGameObjectMgr.Inst.Battle == true)
        {
            m_text.text = "CONTINUE";
            SceneLoader.LoadScene("Battle");
        }
        else
        {
            m_text.text = "START";
            SceneLoader.LoadScene("Choice");
        }

        AudioManager.Inst.playSelectSFX(AudioManager.eSelectSFX.ButtonSFX);
    }

}

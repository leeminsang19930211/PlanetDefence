using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Result_OKButtonCtrl : MonoBehaviour
{
    public Text m_text = null;

    public void OnEnable()
    {
        if (EndingMgr.Inst.Result == EndingMgr.eResult.Clear)
        {
            m_text.text = "행성 방어 성공!";
        }
        else if (EndingMgr.Inst.Result == EndingMgr.eResult.Clear_Last)
        {       
            m_text.text = "축하합니다. 마지막 날까지 \n 행성을 지켜 냈습니다!";
        }
        else
        {
            m_text.text = "행성 방어 실패··.";
        }
    }

    public void OnClick()
    {
        EndingMgr.Inst.ReleaseBattleScene();
        EndingMgr.Inst.PopDownResultPanel();

        if(EndingMgr.Inst.Result == EndingMgr.eResult.Clear)
        {

            SceneLoader.LoadScene("Choice");
        }
        else if (EndingMgr.Inst.Result == EndingMgr.eResult.Clear_Last)
        {
            SceneLoader.LoadScene("Lobby");
        }
        else
        {
            SceneLoader.LoadScene("Lobby");
        }       
    }
}

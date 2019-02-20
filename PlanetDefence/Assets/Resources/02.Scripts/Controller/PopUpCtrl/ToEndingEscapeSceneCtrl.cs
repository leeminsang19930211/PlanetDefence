using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToEndingEscapeSceneCtrl : MonoBehaviour
{
    void ToEndingEscape()
    {       
        EndingMgr.Inst.ReleaseBattleScene();
        BattleGameObjectMgr.Inst.ToEndingEscapeScene();
    }
}

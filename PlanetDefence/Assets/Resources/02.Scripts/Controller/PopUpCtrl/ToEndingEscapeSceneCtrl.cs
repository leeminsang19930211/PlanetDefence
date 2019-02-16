using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToEndingEscapeSceneCtrl : MonoBehaviour
{
    // Start is called before the first frame update
    void ToEndingEscape()
    {
        BattleGameObjectMgr.Inst.ToEndingEscapeScene();
    }

}

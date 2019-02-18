using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyEixtButtonCtrl : MonoBehaviour
{
    public void OnClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }

}

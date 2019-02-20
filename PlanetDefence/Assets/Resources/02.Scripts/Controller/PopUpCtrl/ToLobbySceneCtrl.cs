using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToLobbySceneCtrl : MonoBehaviour
{
    public void ToLobbyScene()
    {
        SceneManager.LoadScene("Lobby");
    }
}

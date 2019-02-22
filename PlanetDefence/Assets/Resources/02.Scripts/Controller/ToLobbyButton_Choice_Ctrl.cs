using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToLobbyButton_Choice_Ctrl : MonoBehaviour
{
    public void OnClick()
    {
        SceneLoader.LoadScene("Lobby");
    }
}

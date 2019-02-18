using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyStartButtonCtrl : MonoBehaviour
{
 
    public void OnClick()
    {
            SceneLoader.LoadScene("Choice");
    }

    public void OnEnable()
    {
       
    }
}

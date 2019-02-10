using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreLoadingInitCtrl : MonoBehaviour
{ 
    private void Awake()
    {
        Screen.SetResolution(Screen.width, Screen.height, true);
    }
}

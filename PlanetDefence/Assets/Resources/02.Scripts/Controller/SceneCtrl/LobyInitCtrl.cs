using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class LobyInitCtrl : MonoBehaviour
{
    private void Awake()
    {
        SceneLoader.OnStartScene();
    }

}

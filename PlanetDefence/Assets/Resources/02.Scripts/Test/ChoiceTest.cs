using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneLoader.LoadScene("Battle");
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            SceneLoader.LoadScene("Battle");
        }
    }

    private void LateUpdate()
    {
       
    }
}

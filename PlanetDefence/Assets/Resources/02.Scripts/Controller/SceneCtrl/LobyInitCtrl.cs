using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class LobyInitCtrl : MonoBehaviour
{
    private void Awake()
    {
        FileStream fs = File.Open(Application.persistentDataPath + "TestData.bin", FileMode.Open);

        if(fs != null)
        {
            BinaryReader wr = new BinaryReader(fs);

            GlobalGameObjectMgr.Inst.CurDay = wr.ReadInt32();

            fs.Close();
        }
    
    }

}

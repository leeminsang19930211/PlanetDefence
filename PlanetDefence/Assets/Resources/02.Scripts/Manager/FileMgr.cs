using UnityEngine;
using System.Collections;

using System.IO;
//using System.Runtime.Serialization.Formatters.Binary;
//using System.Runtime.Serialization;

public class FileMgr : MonoBehaviour
{
    private static FileMgr m_inst = null;
 
#if UNITY_EDITOR
    private string path_playerReset = "Data/PlayerReset.txt";
    private string path_playerData = "Data/PlayerData.bin";
    private string path_globalReset = "Data/GlobalReset.txt";
    private string path_globalData = "Data/GlobalData.bin";
    private string path_turretReset = "Data/TurretReset.txt";
    private string path_turretData = "Data/TurretData.bin";

#else
    private string path_playerReset = Application.persistentDataPath + "PlayerReset.txt";
    private string path_playerData = Application.persistentDataPath + "PlayerData.bin";
    private string path_globalReset = Application.persistentDataPath + "GlobalReset.txt";
    private string path_globalData = Application.persistentDataPath + "GlobalData.bin";
    private string path_turretReset = Application.persistentDataPath + "TurretReset.txt";
    private string path_turretData = Application.persistentDataPath + "TurretData.bin";
#endif




    public static FileMgr Inst
    {
        get
        {
            if (m_inst == null)
            {
                GameObject container = new GameObject();
                container.name = "FileMgr";
                m_inst = container.AddComponent<FileMgr>() as FileMgr;
                DontDestroyOnLoad(container);
            }

            return m_inst;
        }
    }

    public bool PlayerReset { get { return CheckReset(path_playerReset); } }
    public bool GlobalReset { get { return CheckReset(path_globalReset);  } }
    public bool TurretReset { get { return CheckReset(path_turretReset); } }

    public void ResetPlayerData()
    {
        RecordReset(path_playerReset, true);
    }

    public void ResetGlobalData()
    {
        RecordReset(path_globalReset, true);
    }

    public void ResetTurretData()
    {
        RecordReset(path_turretReset, true);
    }

    public void SaveGlobaData()
    {
        RecordReset(path_globalReset, false);
        GlobalGameObjectMgr.Inst.SaveData(path_globalData);
    }

    public void LoadGlobalData()
    {
        if (CheckReset(path_globalReset) == true)
        {
            return;
        }

        GlobalGameObjectMgr.Inst.LoadData(path_globalData);
    }

    public void SaveTurretData()
    {
        RecordReset(path_turretReset, false);
        TurretMgr.Inst.SaveData(path_turretData);
    }

    public void LoadTurretData()
    {
        if (CheckReset(path_turretReset) ==true)
        {
            return;
        }

        TurretMgr.Inst.LoadData(path_turretData);
    }

    public void SavePlayerData()
    {
        RecordReset(path_playerReset, false);
        Player.Inst.SaveData(path_playerData);
    }

    public void LoadPlayerData()
    {
        if (CheckReset(path_playerReset) == true)
        {
            return;
        }

        Player.Inst.LoadData(path_playerData);
    }

    private void RecordReset(string path, bool reset)
    {
        FileStream fileStream = new FileStream(path, FileMode.Create);

        if(fileStream == null)
        {
            Debug.LogError("Open file to record reset failed");
            return;
        }

        StreamWriter streamWriter = new StreamWriter(fileStream);

        if(reset )
        {
            streamWriter.Write("true");
        }
        else
        {
            streamWriter.Write("false");
        }

        streamWriter.Dispose();

        fileStream.Close();
    }

    private bool CheckReset(string path)
    {
        try
        {
            FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

            StreamReader streamReader = new StreamReader(fileStream);

            string reset = streamReader.ReadLine();

            if (reset == "false")
            {
                return false;
            }
            else if (reset == "true")
            {
                return true;
            }

        }
        catch(FileNotFoundException e)
        {
            e.GetType();  // 안썻다는 워닝 경고때문에 해놓음

            return true;
        }

        

        return false;
    }
}

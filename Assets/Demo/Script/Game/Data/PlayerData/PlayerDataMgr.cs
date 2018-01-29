using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerDataMgr:BaseDataMgr<PlayerDataMgr>
{
    private PlayerData mPlayerData;
    public PlayerData playerData
    {
        get {
            if (mPlayerData == null)
                mPlayerData = data as PlayerData;
            return mPlayerData;
        }
    }

    private const string PLAYER_DATA_FILE_PATH = "GameData.txt";

    public override void Init()
    {
        base.Init();
        file_path = PLAYER_DATA_FILE_PATH;
        //init player data
        ReadData();
    }

    public void ReadData()
    {
        Read(typeof(PlayerData));
    }
    
}

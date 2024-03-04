using System;
using System.Collections.Generic;
using UnityEngine;

namespace GM
{
    [Serializable]
    public class GameData
    {
        public string playerName;
        public int unlockedLevels;
        public int highScore;
    }

    [Serializable]
    public class LevelData
    {
        public string level;
        public int bulletCount;
        public int LevelTime;
        public string levelmode;
        public List<GameObjectsData> gameObjectsdata = new List<GameObjectsData>();

    }

    [Serializable]
    public class GameObjectsData
    {
        public string uniqueID;
        public UnityEngine.Vector3 Position;
        public UnityEngine.Quaternion Rotation;
    }


    [Serializable]
    public class SettingsData
    {
        public float volume;
        public int quality;
        public bool isFullScreen;
    }

    //[Serializable]
    //public class Vec3Ser
    //{
    //    public float x;
    //    public float y;
    //    public float z;

    //    public   Vec3Ser(UnityEngine.Vector3 v)
    //    {
    //        this.x =v.x;
    //        this.y = v.y;
    //        this.z = v.z;
    //    }

    //    public UnityEngine.Vector3 GetVector()
    //    {
    //        return new UnityEngine.Vector3(x,y,z);
    //    }
    //}

}
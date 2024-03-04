using UnityEngine;
using UnityEditor;
using System.Collections.Generic;


namespace GM
{
    public class LevelSaveLoadData : Singleton<LevelSaveLoadData>
{

   //[SerializeField] string CurrentLevel;
   //[SerializeField] string levelMode;
    [SerializeField] ObjectPrefabs objectPrefabs;

    [SerializeField] List<GameObject> instantiatedLevelObjects = new List<GameObject>();

    [SerializeField]
    LevelData LevelData;

        
   public void  save()
    {

        instantiatedLevelObjects.Clear();
        //LevelData = new LevelData();
        //LevelData.level = CurrentLevel;
        //LevelData.bulletCount = 30;
        //LevelData.levelmode = levelMode;
        Instantiable[] gameObjects =GameObject.FindObjectsOfType<Instantiable>(true);

        LevelData.gameObjectsdata.Clear();
        foreach (Instantiable obj in gameObjects)
        {
            instantiatedLevelObjects.Add(obj.gameObject);
            LevelData.gameObjectsdata.Add(new GameObjectsData { uniqueID = obj.objectid, Position = obj.transform.position,Rotation=obj.transform.rotation }); ;
        }
        SaveManager.Instance.SaveLevel(LevelData);
        
    }

    public LevelData load()
    {
        foreach(GameObject go in instantiatedLevelObjects)
        {
            DestroyImmediate(go);
        }
        instantiatedLevelObjects.Clear();
        LevelData = SaveManager.Instance.LoadLevel(int.Parse(LevelData.level));
        foreach (GameObjectsData obj in LevelData.gameObjectsdata)
        {
            Instantiable li = objectPrefabs.objects.Find(x => obj.uniqueID==x.objectid);
            GameObject levelObject=Instantiate(li.gameObject, obj.Position, obj.Rotation);
            instantiatedLevelObjects.Add(levelObject);
        }
        return LevelData;
    }
    public void OnDestroy()
    {
        foreach (GameObject go in instantiatedLevelObjects)
        {
            DestroyImmediate(go, true);
        }
    }

}
}
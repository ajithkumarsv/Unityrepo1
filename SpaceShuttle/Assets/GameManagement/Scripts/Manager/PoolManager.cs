using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GM
{
    public class PoolManager : Singleton<PoolManager>
    {
        [System.Serializable]
        public class Pool
        {
            public string tag;
            public GameObject prefab;
            public int size;
        }



        public List<Pool> pools;
        public Dictionary<string, Queue<GameObject>> poolDictionary;

        private void Awake()
        {

            poolDictionary = new Dictionary<string, Queue<GameObject>>();

            foreach (Pool pool in pools)
            {
                Queue<GameObject> objectPool = new Queue<GameObject>();

                for (int i = 0; i < pool.size; i++)
                {
                    GameObject obj = Instantiate(pool.prefab);
                    obj.transform.parent = transform;
                    obj.SetActive(false);
                    objectPool.Enqueue(obj);
                }

                poolDictionary.Add(pool.tag, objectPool);
            }
        }

        public void ReturnObjectTime(string tag, GameObject obj, float time)
        {
            StartCoroutine(ReturnObjectTimeCoroutine(tag, obj, time));
        }
        IEnumerator ReturnObjectTimeCoroutine(string tag, GameObject obj, float time)
        {
            yield return new WaitForSeconds(time);
            if (obj != null)
            {
                ReturnObject(tag, obj);
            }
        }

        public void ReturnObject(string tag, GameObject gameObject)
        {
            if (poolDictionary.ContainsKey(tag))
            {
                poolDictionary[tag].Enqueue(gameObject);
                gameObject.SetActive(false);
                gameObject.transform.parent = transform;
            }
        }

        public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
        {
            if (!poolDictionary.ContainsKey(tag))
            {
                Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
                return null;
            }

            if (poolDictionary[tag].Count == 0)
            {
                foreach (Pool pool in pools)
                {

                    if (pool.tag == tag)
                    {
                        GameObject obj = Instantiate(pool.prefab);
                        obj.transform.parent = transform;
                        obj.SetActive(false);
                        poolDictionary[tag].Enqueue(obj);
                    }

                }

            }
            GameObject objectToSpawn = poolDictionary[tag].Dequeue();

            objectToSpawn.SetActive(true);
            objectToSpawn.transform.position = position;
            objectToSpawn.transform.rotation = rotation;

            //poolDictionary[tag].Enqueue(objectToSpawn);

            return objectToSpawn;
        }
    }
}
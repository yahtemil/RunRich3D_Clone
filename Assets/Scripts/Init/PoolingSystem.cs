using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class PoolingSystem : Singleton<PoolingSystem>
{
    public List<SourceObjects> SourceObjects = new List<SourceObjects>();

    public int DefaultCount = 10;

    private void Start()
    {
        InitilizePool();
    }

    public void InitilizePool()
    {
        InitilizeGameObjects();
    }

    private void InitilizeGameObjects()
    {
        for (int i = 0; i < SourceObjects.Count; i++)
        {
            int copyNumber = DefaultCount;
            if (SourceObjects[i].MinNumberOfObject != 0)
                copyNumber = SourceObjects[i].MinNumberOfObject;

            for (int j = 0; j < copyNumber; j++)
            {
                GameObject go = Instantiate(SourceObjects[i].SourcePrefab, transform);
                go.SetActive(false);

                SourceObjects[i].QueueClones.Enqueue(go);
            }
        }
    }

    public GameObject QueueInstantiateAPS(string Id)
    {
        GameObject selectObject = null;
        for (int i = 0; i < SourceObjects.Count; i++)
        {
            if (string.Equals(SourceObjects[i].ID, Id))
            {
                selectObject = SourceObjects[i].QueueClones.Dequeue();
                SourceObjects[i].QueueClones.Enqueue(selectObject);

            }
        }
        return selectObject;
    }
}

[System.Serializable]
public class SourceObjects
{
    public string ID;

    public GameObject SourcePrefab;
    //If 0 will use the global object count
    public int MinNumberOfObject = 0;

    [ReadOnly]
    public Queue<GameObject> QueueClones = new Queue<GameObject>();
}

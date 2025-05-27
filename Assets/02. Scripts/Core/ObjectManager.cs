using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pool
{
    public ObjectType Type;
    public int Count;
    public GameObject Prefab;
    public Transform Container;
    public Queue<GameObject> Queue = new();
}

public class ObjectManager : Singleton<ObjectManager>
{
    #region Variables
    [Header("오브젝트 풀 목록")]
    [SerializeField] private List<Pool> m_pool_list;
    #endregion Variables

    #region Helper Methods
    public void Initialize()
    {
        for (int i = 0; i < m_pool_list.Count; i++)
        {
            for (int j = 0; j < m_pool_list[i].Count; i++)
            {
                m_pool_list[i].Queue.Enqueue(CreateNewObject(m_pool_list[i]));
            }
        }
    }

    private GameObject CreateNewObject(Pool pool)
    {
        var new_obj = Instantiate(pool.Prefab, pool.Container);
        new_obj.SetActive(false);

        return new_obj;
    }

    private Pool GetPool(ObjectType type)
    {
        for (int i = 0; i < m_pool_list.Count; i++)
        {
            if (m_pool_list[i].Type == type)
            {
                return m_pool_list[i];
            }
        }

        return null;
    }

    public GameObject GetObject(ObjectType type)
    {
        var pool = GetPool(type);

        GameObject obj;
        if (pool.Queue.Count > 0)
        {
            obj = pool.Queue.Dequeue();
        }
        else
        {
            obj = CreateNewObject(pool);
        }
        obj.SetActive(true);

        return obj;
    }

    public void ReturnObject(GameObject obj, ObjectType type)
    {
        if (!obj)
        {
            Destroy(obj);
            return;
        }

        var pool = GetPool(type);

        if (pool.Queue.Count < pool.Count)
        {
            pool.Queue.Enqueue(obj);
            obj.SetActive(false);
        }
        else
        {
            Destroy(obj);
        }
    }
    #endregion Helper Methods
}

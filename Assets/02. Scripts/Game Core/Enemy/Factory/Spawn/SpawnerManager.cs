using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    [Header("스포너의 부모 트랜스폼")]
    [SerializeField] private Transform m_spawner_root;

    private Spawner[] m_spawner_list;

    private void Awake()
    {
        m_spawner_list = m_spawner_root.GetComponentsInChildren<Spawner>();
    }

    public Spawner GetSpawner(int id)
    {
        for (int i = 0; i < m_spawner_list.Length; i++)
        {
            if (m_spawner_list[i].ID == id)
            {
                return m_spawner_list[i];
            }
        }

        return null;
    }

    public void Updates(int id, int count)
    {
        var spawner = GetSpawner(id);
        spawner.Updates(count);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    #region Variables
    [Header("스포너의 고유한 ID")]
    [SerializeField] private int m_id;

    [Header("스폰될 몬스터의 목록")]
    [SerializeField] private List<Enemy> m_enemy_list;

    [Header("최대 몬스터의 수")]
    [SerializeField] private int m_max_count;

    [Header("스폰 대기 시간")]
    [SerializeField] private float m_spawn_interval = 5f;

    [Header("몬스터 레이어")]
    [SerializeField] private LayerMask m_enemy_layer;

    private int m_enemy_count;
    #endregion Variables

    #region Properties
    public int Count
    {
        get => m_enemy_count;
        set => m_enemy_count = value;
    }

    public int ID { get => m_id; }
    #endregion Properties

    private void Awake()
    {
        StartCoroutine(Co_SpawnEnemy());
    }

    #region Helper Methods
    private IEnumerator Co_SpawnEnemy()
    {
        float elasped_time = 0f;

        while (true)
        {
            yield return new WaitUntil(() => m_enemy_count < m_max_count);

            while (elasped_time <= m_spawn_interval)
            {
                yield return new WaitUntil(() => GameManager.Instance.Current == GameEventType.PLAYING);

                elasped_time += Time.deltaTime;
                yield return null;
            }

            CreateEnemy();

            m_enemy_count++;

            elasped_time = 0f;
        }
    }

    public void Updates(int count)
    {
        m_enemy_count += count;
    }

    private Enemy SelectRandomEnemy()
    {
        if (m_enemy_list.Count == 1)
        {
            return m_enemy_list[0];
        }
        return m_enemy_list[Random.Range(0, m_enemy_list.Count)];
    }

    private void CreateEnemy()
    {
        var scriptable_object = SelectRandomEnemy();

        var enemy_ctrl = EnemyFactoryManager.Instance.Create(scriptable_object.ID);
        enemy_ctrl.transform.position = transform.position;
        enemy_ctrl.Initialize(m_id);
    }
    #endregion Helper Methods
}

using System.Collections.Generic;
using UnityEngine;

public class GridMap : MonoBehaviour
{
    #region Variables
    [Header("맵의 크기")]
    [SerializeField] private Vector2 m_map_size;

    [Header("노드의 크기")]
    [SerializeField] private float m_node_size;
    private Node[,] m_grid;

    private int m_row_count;
    private int m_col_count;

    [Header("장애물의 레이어")]
    [SerializeField] LayerMask m_obstacle_layer;

    [Header("예상 경로")]
    [SerializeField] private List<Node> m_path_list;
    #endregion Variables

    #region Properties
    public List<Node> Path
    {
        get => m_path_list;
        set => m_path_list = value;
    }
    #endregion Properties

    private void Awake()
    {
        Initialize();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, m_map_size);
        if (m_grid != null)
        {
            foreach (Node node in m_grid)
            {
                Gizmos.color = node.CanWalk ? Color.green : Color.red;
                if (Path != null)
                {
                    if (Path.Contains(node))
                    {
                        Gizmos.color = Color.blue;
                    }
                }
                Gizmos.DrawCube(node.World, Vector2.one * (m_node_size / 2));
            }
        }
    }

    #region Helper Methods
    private void Initialize()
    {
        m_col_count = Mathf.CeilToInt(m_map_size.x / m_node_size);
        m_row_count = Mathf.CeilToInt(m_map_size.y / m_node_size);

        m_grid = new Node[m_col_count, m_row_count];
        for (int col = 0; col < m_col_count; col++)
        {
            for (int row = 0; row < m_row_count; row++)
            {
                Vector2 top_left_offset = (Vector2)transform.position + new Vector2(-m_map_size.x, m_map_size.y) / 2f;
                Vector2 position = top_left_offset + new Vector2((col + 0.5f) * m_node_size, -(row + 0.5f) * m_node_size);

                var hit = Physics2D.OverlapBox(position, new Vector2(m_node_size, m_node_size), 0, m_obstacle_layer);
                m_grid[col, row] = new Node(hit == null, position, new Vector2(col, row));
            }
        }
    }

    public List<Node> GetNeighborNode(Node node)
    {
        var node_list = new List<Node>();
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                if (i == 0 && j == 0)
                {
                    continue;
                }

                int new_x = (int)node.Local.x + i;
                int new_y = (int)node.Local.y + j;

                if (new_x >= 0 && new_y >= 0 && new_x < m_col_count && new_y < m_row_count)
                {
                    node_list.Add(m_grid[new_x, new_y]);
                }
            }
        }

        return node_list;
    }

    public Node GetNode(Vector3 position)
    {
        Vector2 top_left_offset = (Vector2)transform.position + new Vector2(-m_map_size.x, m_map_size.y) / 2f;
        Vector2 local_pos = (Vector2)position - top_left_offset;

        int pos_x = Mathf.RoundToInt(local_pos.x / m_node_size);
        int pos_y = Mathf.RoundToInt(-local_pos.y / m_node_size);

        if (pos_x >= 0 && pos_y >= 0 && pos_x < m_col_count && pos_y < m_row_count)
        {
            return m_grid[pos_x, pos_y];
        }
        else
        {
            return null;
        }
    }
    #endregion Helper Methods
}

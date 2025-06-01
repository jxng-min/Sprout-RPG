using System.Collections.Generic;
using UnityEngine;

public class GridMap : MonoBehaviour
{
    #region Variables
    [Header("맵의 크기")]
    [SerializeField] private Vector2 m_map_size;

    [Header("노드의 크기")]
    [SerializeField] private float m_node_size;
    private Node[,] m_nodes;

    private int m_x_node_count;
    private int m_y_node_count;

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
        m_x_node_count = Mathf.CeilToInt(m_map_size.x / m_node_size);
        m_y_node_count = Mathf.CeilToInt(m_map_size.y / m_node_size);

        m_nodes = new Node[m_x_node_count, m_y_node_count];
        for (int i = 0; i < m_x_node_count; i++)
        {
            for (int j = 0; j < m_y_node_count; j++)
            {
                Vector2 top_left_offset = (Vector2)transform.position + new Vector2(-m_map_size.x, m_map_size.y) / 2f;
                Vector2 position = top_left_offset + new Vector2((i + 0.5f) * m_node_size, -(j + 0.5f) * m_node_size);

                var hit = Physics2D.OverlapBox(position, new Vector2(m_node_size, m_node_size), 0, m_obstacle_layer);
                m_nodes[i, j] = new Node(hit == null, position, i, j);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(m_map_size.x, m_map_size.y, 1));
        if (m_nodes != null)
        {
            foreach (Node node in m_nodes)
            {
                Gizmos.color = node.CanWalk ? Color.green : Color.red;
                if (Path != null)
                {
                    if (Path.Contains(node))
                    {
                        Gizmos.color = Color.blue;
                    }
                }
                Gizmos.DrawCube(node.Position, Vector3.one * (m_node_size / 2));
            }
        }
    }

    #region Helper Methods
    public List<Node> SearchNeighborNode(Node node)
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

                int new_x = node.X + i;
                int new_y = node.Y + j;

                if (new_x >= 0 && new_y >= 0 && new_x < m_x_node_count && new_y < m_y_node_count)
                {
                    node_list.Add(m_nodes[new_x, new_y]);
                }
            }
        }

        return node_list;
    }

    public Node GetNode(Vector3 position)
    {
        int pos_x = Mathf.RoundToInt(position.x / m_node_size);
        int pos_y = Mathf.RoundToInt(position.y / m_node_size);

        if (pos_x >= 0 && pos_y >= 0 && pos_x < m_x_node_count && pos_y < m_y_node_count)
        {
            return m_nodes[pos_x, pos_y];
        }
        else
        {
            return null;
        }
    }
    #endregion Helper Methods
}

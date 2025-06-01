using System.Collections.Generic;
using UnityEditor.TerrainTools;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    #region Variables
    [Header("길찾기를 위한 그리드 맵")]
    [SerializeField] GridMap m_grid_map;
    #endregion Variables

    public List<Node> Pathfind(Vector3 start_pos, Vector3 end_pos)
    {
        var open_list = new List<Node>();
        var closed_list = new HashSet<Node>();

        var start_node = m_grid_map.GetNode(start_pos);
        var end_node = m_grid_map.GetNode(end_pos);

        open_list.Add(start_node);
        while (open_list.Count > 0)
        {
            var current_node = open_list[0];

            for (int i = 0; i < open_list.Count; i++)
            {
                if (open_list[i].FCost <= current_node.FCost)
                {
                    current_node = open_list[i];
                }
            }

            open_list.Remove(current_node);
            closed_list.Add(current_node);
            if (current_node == end_node)
            {
                return BackTracking(start_node, current_node);
            }

            foreach (var neighbor_node in m_grid_map.SearchNeighborNode(current_node))
            {
                if (neighbor_node.CanWalk && !closed_list.Contains(neighbor_node))
                {
                    int x = current_node.X - neighbor_node.X;
                    int y = current_node.Y - neighbor_node.Y;

                    int new_cost = current_node.GCost + GetDistance(neighbor_node, current_node);
                    if (new_cost < neighbor_node.GCost || !open_list.Contains(neighbor_node))
                    {
                        neighbor_node.GCost = new_cost;
                        neighbor_node.HCost = GetDistance(neighbor_node, end_node);
                        neighbor_node.Parent = current_node;

                        if (!open_list.Contains(neighbor_node))
                        {
                            open_list.Add(neighbor_node);
                        }
                    }
                }
            }
        }

        return null;
    }

    private List<Node> BackTracking(Node start_node, Node end_node)
    {
        var path = new List<Node>();

        var current_node = end_node;
        while (current_node != start_node)
        {
            path.Add(current_node);
            current_node = current_node.Parent;
        }

        path.Reverse();
        m_grid_map.Path = path;

        return path;
    }

    private int GetDistance(Node arg1, Node arg2)
    {
        int x = Mathf.Abs(arg1.X - arg2.X);
        int y = Mathf.Abs(arg1.Y - arg2.Y);

        if (x > y)
        {
            return 14 * y + 10 * (x - y);
        }
        return 14 * x + 10 * (y - x);
    }
}

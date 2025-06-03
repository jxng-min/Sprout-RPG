using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
	#region Variables
    private const int DIAGNOL = 14;
    private const int STRAIGHT = 10;
    
    [Header("그리드 맵 컴포넌트")]
    [SerializeField] GridMap m_grid_map;
    #endregion Variables

    private void Awake()
    {
        m_grid_map = FindFirstObjectByType<GridMap>();
    }
    
    #region Helper Methods
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

            for (int i = 1; i < open_list.Count; i++)
            {
                if (open_list[i].F <= current_node.F)
                {
                    current_node = open_list[i];
                }
            }

            open_list.Remove(current_node);
            closed_list.Add(current_node);

            if (current_node == end_node)
            {
                return BackTracking(start_node, end_node);
            }

            foreach (var node in m_grid_map.GetNeighborNode(current_node))
            {
                if (node.CanWalk && !closed_list.Contains(node))
                {
                    int x = (int)current_node.Local.x - (int)node.Local.x;
                    int y = (int)current_node.Local.y - (int)node.Local.y;

                    int new_cost = current_node.G + GetManhattan(node, current_node);
                    if (new_cost < node.G || !open_list.Contains(node))
                    {
                        node.G = new_cost;
                        node.H = GetManhattan(node, end_node);
                        node.Parent = current_node;

                        if (!open_list.Contains(node))
                        {
                            open_list.Add(node);
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
        while(current_node != start_node)
        {
        	path.Add(current_node);
            current_node = current_node.Parent;
        }
        
        path.Reverse();
        m_grid_map.Path = path;
        
        return path;
    }
    
    private int GetManhattan(Node arg1, Node arg2)
    {
    	int x = Mathf.Abs((int)arg2.Local.x - (int)arg1.Local.x);
        int y = Mathf.Abs((int)arg2.Local.y - (int)arg1.Local.y);
        
        if(x > y)
        {
        	return DIAGNOL * y + STRAIGHT * (x - y);
        }
        return DIAGNOL * x + STRAIGHT * (y - x);
    }
    #endregion Helper Methods
}
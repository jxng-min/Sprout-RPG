using UnityEngine;

public class Node
{
    #region Variables
    private bool m_can_walk;
    private Vector3 m_position;
    private int m_x;
    private int m_y;
    private int m_g_cost;
    private int m_h_cost;
    private Node m_parent;

    #endregion Variables

    #region Properties
    public bool CanWalk
    {
        get => m_can_walk;
        set => m_can_walk = value;
    }

    public Vector3 Position
    {
        get => m_position;
        set => m_position = value;
    }

    public int X
    {
        get => m_x;
        set => m_x = value;
    }

    public int Y
    {
        get => m_y;
        set => m_y = value;
    }

    public int GCost
    {
        get => m_g_cost;
        set => m_g_cost = value;
    }

    public int HCost
    {
        get => m_h_cost;
        set => m_h_cost = value;
    }

    public int FCost
    {
        get => GCost + HCost;
    }

    public Node Parent
    {
        get => m_parent;
        set => m_parent = value;
    }
    #endregion Properties

    public Node(bool can_walk, Vector3 pos, int x, int y)
    {
        m_can_walk = can_walk;
        m_position = pos;
        m_x = x;
        m_y = y;
    }
}
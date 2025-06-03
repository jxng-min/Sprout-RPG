using UnityEngine;

public class Node
{
    #region Variables
    private int m_g_cost;
    private int m_h_cost;

    private Node m_parent_node;

    private Vector2 m_local_position;
    private Vector2 m_world_position;

    private bool m_can_walk;
    #endregion Variables

    #region Properties
    public int G
    {
        get => m_g_cost;
        set => m_g_cost = value;
    }

    public int H
    {
        get => m_h_cost;
        set => m_h_cost = value;
    }

    public int F
    {
        get => G + H;
    }

    public Node Parent
    {
        get => m_parent_node;
        set => m_parent_node = value;
    }

    public Vector2 World
    {
        get => m_world_position;
        set => m_world_position = value;
    }

    public Vector2 Local
    {
        get => m_local_position;
        set => m_local_position = value;
    }

    public bool CanWalk
    {
        get => m_can_walk;
        set => m_can_walk = value;
    }
    #endregion Properties

    public Node(bool can_walk, Vector2 world_position, Vector2 local_position)
    {
        m_can_walk = can_walk;

        m_world_position = world_position;
        m_local_position = local_position;
    }
}
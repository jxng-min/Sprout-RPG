using System.Collections.Generic;
using UnityEngine;

public class Craftman : NPC
{
    #region Variables
    [Header("제작할 수 있는 레시피의 목록")]
    [SerializeField] protected List<Receipe> m_receipe_list;

    [Header("제작 UI 컴포넌트")]
    [SerializeField] protected Crafter m_crafter;
    #endregion Variables

    #region Helper Methods
    public override void Interaction()
    {
        Rotation();

        m_crafter.OpenUI(m_receipe_list);
    }
    #endregion Helper Methods
}

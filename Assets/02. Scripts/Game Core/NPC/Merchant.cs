using System.Collections.Generic;
using UnityEngine;

public class Merchant : NPC
{
    #region Variables
    [Space(50)]
    [Header("상점에서 판매할 아이템의 목록")]
    [SerializeField] protected List<Sale> m_sale_list;

    [Header("상점 UI 컴포넌트")]
    [SerializeField] protected Shop m_shop_ui;
    #endregion Variables

    #region Helper Methods
    public override void Interaction()
    {
        Rotation();

        m_shop_ui.OpenUI(m_sale_list);
    }
    #endregion Helper Methods
}

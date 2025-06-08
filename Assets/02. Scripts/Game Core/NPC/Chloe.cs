using UnityEngine;

public class Chloe : Merchant
{
    #region Helper Methods
    public override void Interaction()
    {
        if (Dialoguer.IsActive)
        {
            return;
        }

        Rotation();

        m_dialoguer.EndAction += Action;
        m_dialoguer.StartDialogue(0);
    }

    private void Action()
    {
        m_shop_ui.OpenUI(m_sale_list);
    }
    #endregion Helper Methods
}

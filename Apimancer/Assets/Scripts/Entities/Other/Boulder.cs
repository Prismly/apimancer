using UnityEngine;
using System.Collections;

public class Boulder : Other
{
    private int maxHealth = 99;
    private int health = 99;

    public override IEnumerator DetermineMovement()
    {
        // do movement
        return null;
    }

    public override int MaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = value; }
    }

    public override int Health
    {
        get { return health; }
        set { health = value; }
    }

    public override void OnDeath()
    {
        Cell c = CellManager.Instance.GetCell(loc);
        Transform t = c.GetComponent<Transform>();
        Transform st = t.GetChild(0);
        st.localPosition = new Vector3(0, 0, -0.01f);
    }
}

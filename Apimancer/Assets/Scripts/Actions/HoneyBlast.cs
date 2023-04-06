using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoneyBlast : SpellAction
{
<<<<<<< Updated upstream
    public HoneyBlast(Unit from, uint range, uint cost)
        :base(from, range, cost)
=======
    public HoneyBlast(Unit from, uint range, short cost)
        : base(from, range, cost)
>>>>>>> Stashed changes
    {
    }

    public override bool Execute(Cell cell)
    {
        if (!Validate(cell))
            return false;

        if (!cell.IsOccupied)
            cell.GetComponent<SpriteRenderer>().color += new Color(0.3f, 0.3f, 0);

        //Unit target = cell.Occupant as Unit;
        //target.SetSticky(true);
        return true;
    }
}

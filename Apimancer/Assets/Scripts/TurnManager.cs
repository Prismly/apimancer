using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TurnManager
{
    private static bool allowPlayerActions;

    public static void TogglePlayerActions(bool newVal)
    {
        allowPlayerActions = newVal;
    }
}

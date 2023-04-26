using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public static class OptionAddon
{
    public enum AddonID
    {
        DOT_BLK,
        DOT_YLW,
        ACT_ATK,
        ACT_MOV,
        ACT_RNG,
        ACT_TRG,
        HP_ALLY,
        HP_ENEM,
        HP_SHLD,
        NECTAR,
        BURNED,
        WATERY,
        POISON,
        STAR
    }

    public static int iconCount = 14;
    public static string[] flagList;

    static OptionAddon()
    {
        flagList = new string[iconCount];
        // Black Dot
        flagList[0] = "[X]";
        // Yellow Dot
        flagList[1] = "[O]";
        // Attack Symbol
        flagList[2] = "[ATK]";
        // Movement Symbol
        flagList[3] = "[MOV]";
        // Range Symbol
        flagList[4] = "[RNG]";
        // Targets Symbol
        flagList[5] = "[TRG]";
        // Ally HP Symbol
        flagList[6] = "[HPA]";
        // Enemy HP Symbol
        flagList[7] = "[HPE]";
        // Shielded HP Symbol
        flagList[8] = "[HPS]";
        // Nectar Symbol
        flagList[9] = "[NCT]";
        // Burned Symbol
        flagList[10] = "[BRN]";
        // Wet Symbol
        flagList[11] = "[WTR]";
        // Poison Symbol
        flagList[12] = "[PSN]";
        // Star Symbol
        flagList[13] = "[STR]";
    }

    // Replaces all instances of the 'flag' string with the corresponding sprite (in text form).
    public static string Mutate(string original)
    {
        for (int i = 0; i < iconCount; i++)
        {
            original = original.Replace(flagList[i], IdToString(i));
        }
        return original;
    }

    public static string IdToString(int id)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("<sprite=");
        sb.Append(id);
        sb.Append(">");
        return sb.ToString();
    }
}

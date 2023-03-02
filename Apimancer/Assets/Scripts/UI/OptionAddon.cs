using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class OptionAddon
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
        HP_SHLD
    }

    private AddonID id;
    public string flag;

    public OptionAddon(AddonID id, string flag)
    {
        this.id = id;
        this.flag = flag;
    }

    // Replaces all instances of the 'flag' string with the corresponding sprite (in text form).
    public string Mutate(string original)
    {
        return original.Replace(flag, ToString());
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("<sprite=");
        sb.Append((int)id);
        sb.Append(">");
        return sb.ToString();
    }
}

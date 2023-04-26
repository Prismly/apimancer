using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderCell : Cell
{
    [SerializeField] private List<GameObject> _boulderPrefabs;

    private CellType type = CellType.BOULDER;
    public override CellType Type
    {
        get { return type; }
        set { type = value; }
    }

    public new void Start()
    {
        base.Start();
        short i = (short)Random.Range(0, _boulderPrefabs.Count);
        Boulder b = Instantiate(_boulderPrefabs[0]).GetComponent<Boulder>();
        b.setLocation(this);
        GameManager.Instance.Units[Unit.Faction.OTHER].Add(b);
    }
}

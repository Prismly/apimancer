using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerCell : Cell
{
    [SerializeField] private List<GameObject> _flowerPrefabs;

    private CellType type = CellType.FLOWER;
    public override CellType Type
    {
        get { return type; }
        set { type = value; }
    }

    public new void Start()
    {
        base.Start();
        short i = (short)Random.Range(0, 3);
        Flower f = Instantiate(_flowerPrefabs[i]).GetComponent<Flower>();
        f.setLocation(this);
        GameManager.Instance.Units[Unit.Faction.RESOURCE].Add(f);
    }
}

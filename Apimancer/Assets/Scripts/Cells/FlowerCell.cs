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
        float r = (float)Random.Range(-1, 2);
        Flower f = Instantiate(_flowerPrefabs[i]).GetComponent<Flower>();
        f.setLocation(this);
        f.GetComponent<Transform>().Rotate(0f, r * 30f, 0f);
        GameManager.Instance.Units[Unit.Faction.RESOURCE].Add(f);
    }
}

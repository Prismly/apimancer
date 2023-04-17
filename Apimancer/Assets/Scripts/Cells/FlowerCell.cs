using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerCell : Cell
{
    [SerializeField] private List<GameObject> _flowerPrefabs;
    private int _flowerSpawnRate = 4;

    private CellType type = CellType.FLOWER;

    private int _turnsEmpty = 0;
    public override CellType Type
    {
        get { return type; }
        set { type = value; }
    }

    public new void Start()
    {
        base.Start();
        SpawnFlower();
    }

    public override void OnEndTurn() {
        if (IsOccupied)
        {
            _turnsEmpty = 0;
            return;
        }
        if (_turnsEmpty >= _flowerSpawnRate)
        {
            SpawnFlower();
            _turnsEmpty = 0;
            return;
        }
        _turnsEmpty++;
    }

    private void SpawnFlower()
    {
        short i = (short)Random.Range(0, 3);
        float r = (float)Random.Range(-1, 2);
        Flower f = Instantiate(_flowerPrefabs[i]).GetComponent<Flower>();
        f.setLocation(this);
        f.GetComponent<Transform>().Rotate(0f, r * 30f, 0f);
        GameManager.Instance.Units[Unit.Faction.RESOURCE].Add(f);
    }
}

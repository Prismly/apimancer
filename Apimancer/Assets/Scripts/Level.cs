using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private List<Vector2Int> _spawnLocations;

    public void SpawnWizards(List<Wizard> wizards)
    {
        int wizardCount = Mathf.Min(wizards.Count, _spawnLocations.Count);
        for (int i = 0; i < wizardCount; i++)
        {
            wizards[i].setLocation(_spawnLocations[i]);
        }
    }

    public int getMaxWizards()
    {
        return _spawnLocations.Count;
    }
}

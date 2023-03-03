using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Wizard : Unit
{
    // private int actionPoints = 0;
    // private int actionPointsMax = 99;

    // private List<ActionCommand> summonList = new List<ActionCommand>();
    // private List<ActionCommand> spellsList = new List<ActionCommand>();

    public Color Color;
    public bool IsTurn {get; protected set;}

    // private void Start()
    // {
    //     Action Command Constructor is NAME, COST, MOVE, RANGE, TARGS
    //     NOTE: These are temporary! There will be a script for each 
    //     summonList.Add(new ActionCommand("Summon Bee", 1, 0, 3, 1));
    //     summonList.Add(new ActionCommand("Sweeping Summon", 5, 0, 1, 3));
    //     summonList.Add(new ActionCommand("Summon Beast", 10, 0, ));

    //     spellsList.Add(new ActionCommand("Move", 1));
    //     spellsList.Add(new ActionCommand("Harvest", 5));
    //     spellsList.Add(new ActionCommand("Pheromone Lure", 5));
    // }

    public abstract void BeginTurn();
    public abstract void EndTurn();
}

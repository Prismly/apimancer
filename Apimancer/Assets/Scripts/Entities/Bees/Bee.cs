using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Bee : Unit
{
    private List<GameObject> arrows = new List<GameObject>();

    public void OnSpawn()
    {
        PlaySound(Sounds.Summon);
    }

    public void BeginTurn()
    {
        Cell prevCell = GetCell();
        List<Cell> cells = DetermineTarget().Item3;
        if (cells == null)  {
            return;
        }
        foreach (Cell c in cells)
        {
            Vector3 dist = c.transform.position - prevCell.transform.position;
            if (dist.Equals(Vector3.zero))
                continue;
            GameObject arrow = null;
            Quaternion rotation = Quaternion.identity;

            switch (dist)
            {
                // HEAD right
                case Vector3 v when c.Equals(cells.Last()) && v.y == 0 && v.x > 0:
                    arrow = UIManager.Instance.HEAD_HOR;
                    break;
                // HEAD left
                case Vector3 v when c.Equals(cells.Last()) && v.y == 0 && v.x < 0:
                    arrow = UIManager.Instance.HEAD_HOR;
                    rotation = Quaternion.Euler(0, 0, 180);
                    break;
                // BODY right
                case Vector3 v when v.y == 0 && v.x > 0:
                    arrow = UIManager.Instance.BODY_HOR;
                    break;
                // BDOY left
                case Vector3 v when v.y == 0 && v.x < 0:
                    arrow = UIManager.Instance.BODY_HOR;
                    rotation = Quaternion.Euler(0, 0, 180);
                    break;
                // HEAD up right
                case Vector3 v when c.Equals(cells.Last()) && v.x > 0 && v.y > 0:
                    arrow = UIManager.Instance.HEAD_DIAG;
                    break;
                // HEAD up left
                case Vector3 v when c.Equals(cells.Last()) && v.x < 0 && v.y > 0:
                    arrow = UIManager.Instance.HEAD_DIAG;
                    rotation = Quaternion.Euler(0, 0, 70);
                    break;
                // HEAD down left
                case Vector3 v when c.Equals(cells.Last()) && v.x < 0 && v.y < 0:
                    arrow = UIManager.Instance.HEAD_DIAG;
                    rotation = Quaternion.Euler(0, 0, 180);
                    break;
                // HEAD down right
                case Vector3 v when c.Equals(cells.Last()) && v.x > 0 && v.y < 0:
                    arrow = UIManager.Instance.HEAD_DIAG;
                    rotation = Quaternion.Euler(0, 0, 270);
                    break;
                // BODY up right
                case Vector3 v when v.x > 0 && v.y > 0:
                    arrow = UIManager.Instance.BODY_CURVE;
                    break;
                // BODY up left
                case Vector3 v when v.x < 0 && v.y > 0:
                    arrow = UIManager.Instance.BODY_CURVE;
                    rotation = Quaternion.Euler(0, 180, 0);
                    break;
                // BODY down left
                case Vector3 v when v.x < 0 && v.y < 0:
                    arrow = UIManager.Instance.BODY_CURVE;
                    rotation = Quaternion.Euler(0, 0, 180);
                    break;
                // BODY down right
                case Vector3 v when v.x > 0 && v.y < 0:
                    arrow = UIManager.Instance.BODY_CURVE;
                    rotation = Quaternion.Euler(180, 0, 0);
                    break;
                default:
                    break;
            }

            GameObject addedArrow = Instantiate(arrow, Vector3.Lerp(prevCell.transform.position, c.transform.position, 0.5f), rotation);
            addedArrow.SetActive(false);
            arrows.Add(addedArrow);
            prevCell = c;
        }
    }

    public void EndTurn()
    {
        foreach (GameObject arrow in arrows)
            Destroy(arrow);
        arrows.Clear();
    }

    private Wizard commander;

    public Wizard GetCommander()
    {
        return commander;
    }
    public void SetCommander(Wizard w)
    {
        commander = w;
    }

    public override void OnHover()
    {
        base.OnHover();

        foreach (GameObject arrow in arrows)
            arrow.SetActive(true);
    }

    public override void OnUnhover()
    {
        base.OnUnhover();

        foreach (GameObject arrow in arrows)
            arrow.SetActive(false);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectionManager
{
    public static bool canInteract = true;

    private static SelectionManager _instance;
    public static SelectionManager Instance
    {
        get
        { 
            if (_instance == null)
            {
                _instance = new SelectionManager();
            }
            return _instance;
        }
        private set
        {
            _instance = value;
        }
    }

    public HashSet<Selectable> Selected = new HashSet<Selectable>();
    public HashSet<Selectable> Hovered = new HashSet<Selectable>();
    public Selectable Focused;
    public Selectable FocusedProspect;
    public Selectable OneSelected;
    
    private int _selectionIndex = 0;

    public void SelectOne()
    {
        Focused = null;
        if (Hovered.Count <= 0)
        {
            Focused = null;
            DeselectAll();
            return;
        }
        _selectionIndex %= Hovered.Count;
        Selectable toSelect = null;
        int index = 0;
        foreach (Selectable h in Hovered)
        {
            if (_selectionIndex == index++)
            {
                toSelect = h;
                break;
            }
        }
        // if (FocusedProspect != null && FocusedProspect.isSelected)
        // {
        //     Focused = FocusedProspect;
        // }
        if (toSelect.isSelected)
        {
            Focused = toSelect;
        }
        else
        {
            Focused = null;
        }
        Selectable lastSelected = OneSelected;
        DeselectAll();
        OneSelected = lastSelected;
        Select(toSelect);
        _selectionIndex++;
        OneSelected = toSelect;
    }

    public void SelectAnother()
    {
        Focused = null;
        if (Hovered.Count <= 0)
        {
            return;
        }
        _selectionIndex %= Hovered.Count;
        Selectable toSelect = null;
        int index = 0;
        foreach (Selectable h in Hovered)
        {
            if (_selectionIndex == index++)
            {
                toSelect = h;
                break;
            }
        }
        if (FocusedProspect != null && FocusedProspect.isSelected)
        {
            Focused = FocusedProspect;
        }
        Select(toSelect);
        _selectionIndex++;
    }

    public void SelectHovered()
    {
        Focused = null;
        foreach (Selectable s in Hovered)
        {
            Select(s);
        }
    }

    public void SelectMoreHovered()
    {
        Focused = null;
        DeselectAll();

        foreach (Selectable s in Hovered)
        {
            Select(s);
        }
    }

    public void Select(Selectable toSelect)
    {
        if (toSelect == null) return;
        toSelect.Select();
        Selected.Add(toSelect);
    }
    public void Deselect(Selectable toDeselect)
    {
        if (toDeselect == null) return;
        toDeselect.Deselect();
        Selected.Remove(toDeselect);
    }

    public void DeselectAll()
    {
        List<Selectable> toDeselect = new List<Selectable>();
        foreach (Selectable s in Selected)
        {
            toDeselect.Add(s);
        }
        OneSelected = null;
        foreach (Selectable s in toDeselect)
        {
            s.Deselect();
        }
        Selected.Clear();
    }

    public void Hover(Selectable toHover)
    {
        if (toHover == null) return;
        toHover.Hover();
        Hovered.Add(toHover);
    }
    public void Unhover(Selectable toUnhover)
    {
        if (toUnhover == null) return;
        toUnhover.Unhover();
        Hovered.Remove(toUnhover);
    }

    public void UnhoverAll()
    {
        List<Selectable> toUnhover = new List<Selectable>();
        foreach (Selectable s in Hovered)
        {
            toUnhover.Add(s);
        }
        foreach (Selectable s in toUnhover)
        {
            Unhover(s);
        }
        Hovered.Clear();
    }

    public void Assign(Task task, bool keepExisting)
    {
        foreach (Selectable s in Selected)
        {
            s.Assign(task, keepExisting);
        }
    }
}

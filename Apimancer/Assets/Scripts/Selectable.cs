using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Task
{
    public Selectable target;
    public Vector2 destination;
    public Vector2 direction;
    public bool halt;
    public bool repeat;
}

public class Selectable : MonoBehaviour
{
    [Header("SELECTABLE")]

    [SerializeField] private SpriteRenderer _selectionRenderer;
    [SerializeField] private Color _colorHover;
    [SerializeField] private Color _colorSelect;
    
    public Selectable Root;
    public List<Selectable> Nodes = new List<Selectable>();

    public Task CurrentTask = new Task();
    public Queue<Task> Tasks = new Queue<Task>();

    public bool isRoot {get; private set;}
    public bool isReady {get; private set;} = false;
    public bool isHovered {get; private set;} = false;
    public bool isSelected {get; private set;} = false;

    private void Start()
    {
        if (Root == null)
        {
            Root = this;
            isRoot = true;
        }

        CurrentTask.target = null;
        CurrentTask.destination = Root.transform.position;
        CurrentTask.direction = Root.transform.up;
        CurrentTask.halt = true;
        CurrentTask.repeat = false;

        StartTask();
    }

    public virtual bool Notify()
    {
        foreach (Selectable node in Nodes)
        {
            if (!node.isReady)
            {
                return false;
            }
        }
        if (RequestTask())
        {
            StartTask();
        }
        return true;
    }

    public virtual void StartTask()
    {
        if (CurrentTask.direction == Vector2.zero)
        {
            if (Tasks.Count > 0)
            {
                CurrentTask.direction = (Tasks.Peek().destination - CurrentTask.destination).normalized;
            }
            else
            {
                CurrentTask.direction = (CurrentTask.destination - (Vector2)this.transform.position).normalized;
            }
        }
    }

    public void Assign(Task task, bool keepExisting)
    {
        if (!keepExisting)
        {
            Tasks.Clear();
            isReady = true;
        }

        if (isReady)
        {
            isReady = false;
            CurrentTask = task;
            StartTask();
        }
        else
        {
            Tasks.Enqueue(task);
        }
    }

    public bool RequestTask()
    {
        if (Tasks.Count <= 0)
        {
            isReady = true;
            return false;
        }
        if (CurrentTask.repeat)
        {
            Tasks.Enqueue(CurrentTask);
        }
        CurrentTask = Tasks.Dequeue();
        return true;
    }

    public void Select()
    {
        // Debug.Log("Selected");
        isSelected = true;

        if (_selectionRenderer != null)
        {
            _selectionRenderer.enabled = true;
            _selectionRenderer.color = _colorSelect;
        }

        if (Nodes.Count != 0)
        {
            KillDeadNodes();
            foreach (Selectable node in Nodes)
            {
                node.Select();
            }
        }
        OnSelect();
    }
    public void Deselect()
    {
        // Debug.Log("Deselected");
        isSelected = false;

        if (_selectionRenderer != null)
        {
            if (isHovered)
            {
                _selectionRenderer.color = _colorHover;
            }
            else
            {
                _selectionRenderer.enabled = false;
            }
        }

        if (Nodes.Count != 0)
        {
            KillDeadNodes();
            foreach (Selectable node in Nodes)
            {
                node.Deselect();
            }
        }
        OnDeselect();
    }

    public void Hover()
    {
        // Debug.Log("Hovered");
        isHovered = true;

        if (_selectionRenderer != null)
        {
            _selectionRenderer.enabled = true;
            _selectionRenderer.color = _colorHover;
        }
        
        if (Nodes.Count != 0)
        {
            KillDeadNodes();
            foreach (Selectable node in Nodes)
            {
                node.Hover();
            }
        }
        OnHover();
    }
    public void Unhover()
    {
        // Debug.Log("Unovered");
        isHovered = false;

        if (_selectionRenderer != null)
        {
            if (isSelected)
            {
                _selectionRenderer.enabled = true;
                _selectionRenderer.color = _colorSelect;
            }
            else
            {
                _selectionRenderer.enabled = false;
            }
        }

        if (Nodes.Count != 0)
        {
            KillDeadNodes();
            foreach (Selectable node in Nodes)
            {
                node.Unhover();
            }
        }
        OnUnhover();
    }
    
    private void OnMouseEnter()
    {
        SelectionManager.Instance.FocusedProspect = this;
        SelectionManager.Instance.Hover(Root);
    }

    private void OnMouseExit()
    {
        SelectionManager.Instance.Unhover(Root);
    }

    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     // Debug.Log("Triggered");
    //     if (other.tag == "SelectionBox")
    //     {
    //         SelectionManager.Instance.FocusedProspect = this;
    //         SelectionManager.Instance.Hover(Root);
    //     }
    // }
    // private void OnTriggerExit2D(Collider2D other)
    // {
    //     // Debug.Log("Untriggered");
    //     if (other.tag == "SelectionBox")
    //     {
    //         SelectionManager.Instance.Unhover(Root);
    //     }
    // }

    public void KillDeadNodes()
    {
        List<Selectable> deadNodes = new List<Selectable>();

        foreach (Selectable node in Nodes)
        {
            if (node == null)
            {
                deadNodes.Add(node);
            }
        }

        foreach (Selectable node in deadNodes)
        {
            Nodes.Remove(node);
        }

        if (!isRoot && Nodes.Count <= 0)
        {
            Destroy(this);
        }
    }

    private void OnDisable()
    {
        if (!isRoot) return;

        if (isSelected) SelectionManager.Instance.Selected.Remove(this);
        if (isHovered) SelectionManager.Instance.Hovered.Remove(this);
    }

    public virtual void OnHover(){}
    public virtual void OnUnhover(){}
    public virtual void OnSelect(){}
    public virtual void OnDeselect(){}
}

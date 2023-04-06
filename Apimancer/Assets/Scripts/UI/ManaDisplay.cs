using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ManaDisplay : MonoBehaviour
{
    [SerializeField] private float cycleTime;
    [SerializeField] private float cycleAmp;
    private float cycleProg = 0f;
    private bool isFlashing = false;
    TextMeshProUGUI tmp;
    private void Start()
    {
        tmp = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    public void ToggleFlashing(bool turnOn)
    {
        isFlashing = turnOn;
    }

    private void Update()
    {
        if (isFlashing)
        {
            float cycleSpeed = 2 * Mathf.PI / cycleTime;
            cycleProg += cycleSpeed * Time.deltaTime;

            if (cycleProg > 2 * Mathf.PI)
            {
                cycleProg -= 2 * Mathf.PI;
            }

            Debug.Log((cycleProg / (2 * Mathf.PI) / 2) + 0.5f);
            tmp.color = new Color(0, 0, 0, Mathf.Sin(cycleProg) / 2 + 0.5f);
        }
    }

    public void UpdateText()
    {
        //tmp.text = GameManager.Instance.mana.ToString() + " <sprite=13>";
    }
}

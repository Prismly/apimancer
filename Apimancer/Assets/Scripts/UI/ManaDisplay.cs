using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ManaDisplay : MonoBehaviour
{
    [SerializeField] private float cycleTime;
    [SerializeField] private float cycleAmp;
    private int manaLevels = 8;
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
        UpdateDisplay();

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

    public void UpdateDisplay()
    {
        Wizard playerWiz = GameManager.Instance.Wizards[0];
        int currentMana = playerWiz.GetMana();
        int maxMana = playerWiz.GetMaxMana();
        int manaStep = maxMana / manaLevels;

        for (int i = 0; i < manaLevels; i++)
        {
            int threshold = (i * manaStep) + manaStep;
            if (currentMana < threshold || i == manaLevels - 1)
            {
                GetComponent<Animator>().SetInteger("Mana Level", i);
                break;
            }
        }

        tmp.text = currentMana + " &";
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelBtn : MonoBehaviour
{
    [SerializeField] private string level;

    public void OpenLevel()
    {
        SceneManager.LoadScene(level);
    }
}

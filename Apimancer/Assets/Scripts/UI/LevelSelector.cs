using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelSelector : MonoBehaviour
{
    [SerializeField] private string level;

    public void OpenLevel()
    {
        if (level == "Quit")
            Application.Quit();

        SceneManager.LoadScene(level);
    }
}

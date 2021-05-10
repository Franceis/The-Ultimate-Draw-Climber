using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private GameObject restartWindow;

    private void Awake()
    {
        if(Instance == null) Instance = this; //Manage the GameManager instance for having one
    }

    private void Start()
    {
        restartWindow.SetActive(false);

        Time.timeScale = 1f;
    }
    public void RestarWindowOn()
    {
        restartWindow.SetActive(true);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene("SampleScene");
    }
}


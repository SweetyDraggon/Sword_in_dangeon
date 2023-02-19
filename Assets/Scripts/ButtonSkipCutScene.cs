using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonSkipCutScene : MonoBehaviour
{
    [SerializeField] private GameObject button;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("CUTCANSKIP"))
        {
            button.SetActive(true);
        }
    }

    private void Start()
    {
        PlayerPrefs.SetInt("CUTCANSKIP", 1);
    }

    public void LoadGameAndSkip()
    {
        Debug.Log("Load");
        SceneManager.LoadScene(("Main"));
    }
    
}

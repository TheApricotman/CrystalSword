using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, ISaveable  
{
    private int levelNumber;

    private void Start()
    {
        levelNumber = SceneManager.GetActiveScene().buildIndex;
        if(levelNumber < 1)
        {
            levelNumber++;
        }
    }

    public object SaveState()
    {
        return new SaveData
        {
            levelNumber = levelNumber
        };
    }

    public void LoadState(object state)
    {
        var saveData = (SaveData)state;
        saveData.levelNumber = levelNumber;
        SceneManager.LoadScene(levelNumber);
    }

    [Serializable]
    private struct SaveData
    {
        public int levelNumber;
    }
}

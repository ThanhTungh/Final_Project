using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public void clickButtonPlay()
    {
        SceneManager.LoadScene("Main");
    }
}
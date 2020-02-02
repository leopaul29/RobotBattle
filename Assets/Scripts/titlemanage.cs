using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public class titlemanage : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.Joystick1Button1) || Input.GetKeyDown(KeyCode.L))
        {
            OnStartButtonClicked();
        }
    }

    public void OnStartButtonClicked()
    {
        SceneManager.LoadScene("Battle");
    }

}

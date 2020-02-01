using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class titlemanage : MonoBehaviour
{

    public void OnStartButtonClicked()
    {
        SceneManager.LoadScene("SampleScene");
    }

}

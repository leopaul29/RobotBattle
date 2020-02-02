using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultManage : MonoBehaviour
{
    public void OnStartButtonClicked()
    {
        SceneManager.LoadScene("Menu 3D");//Menu 3Dへ遷移
    }
}

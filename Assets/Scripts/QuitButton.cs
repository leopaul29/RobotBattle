using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitButton : MonoBehaviour
{

    private GameObject window;


    public void OnClick()
    {
        window = this.transform.parent.gameObject;
        window.SetActive(false);
    }
}

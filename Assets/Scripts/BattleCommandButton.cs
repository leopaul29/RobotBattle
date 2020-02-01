using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleCommandButton : MonoBehaviour
{
    public Button ButtonObject;
    private Image ImageObject;

    void Start()
    {
        ButtonObject = GetComponent<Button>();
        ImageObject = GetComponent<Image>();
    }

    public void SetColor(Color color)
    {
        ImageObject.color = color;
    }
}

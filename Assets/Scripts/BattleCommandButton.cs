using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BattleCommandButton : MonoBehaviour
{
    [HideInInspector] public Button ButtonObject;
    private Image ImageObject;
    private Text DamageText;
    void Start()
    {
        ButtonObject = GetComponent<Button>();
        ImageObject = GetComponent<Image>();
        DamageText = GetComponentsInChildren<Text>().ToList().FirstOrDefault(x => x.name == "DamageText");
    }

    public void SetColor(Color color)
    {
        ImageObject.color = color;
    }

    public void SetDamageText(string text)
    {
        DamageText.text = text;
    }
}

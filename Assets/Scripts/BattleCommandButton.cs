using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BattleCommandButton : MonoBehaviour
{
    private Button _buttonObject;

    [HideInInspector]
    public Button ButtonObject
    {
        get
        {
            if (_buttonObject == null)
            {
                _buttonObject = GetComponent<Button>();
            }
            return _buttonObject;
        }
    }

    private Image ImageObject;
    private Text DamageText;
    void Start()
    {
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

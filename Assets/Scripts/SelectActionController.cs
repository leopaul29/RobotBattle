using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectActionController : MonoBehaviour
{
    // List of possible action de move to
    public List<NodeChoice> NodeChoices;

    // List of button on the GUI
    public List<GameObject> ActionButtons;

    // Button Color to update
    public Color NotSelectedButtonColor;
    public Color SelectedButtonColor;
    public Color DisableButtonColor;

    public bool playerTurn = false;

    private int currentIndexChoice = 0;
    private int previousIndexChoice = -1;

    // Start is called before the first frame update
    void Start()
    {
        StartTurn();
    }

    public void StartTurn()
    {
        playerTurn = true;
        foreach (GameObject actionButton in ActionButtons)
        {
            actionButton.GetComponent<Image>().color = NotSelectedButtonColor;
        }
        SelectAction(0);
    }

    public void EndTurn()
    {
        playerTurn = false;
        foreach (GameObject actionButton in ActionButtons)
        {
            actionButton.GetComponent<Image>().color = DisableButtonColor;
        }
        currentIndexChoice = 0;
        previousIndexChoice = -1;
    }

    void SelectAction(NodeChoice.Choices nextNodeChoice)
    {
        if (!playerTurn)
        {
            return;
        }
        int nextActionButtonIndex = NodeChoice.ActionButtonIndex(nextNodeChoice);

        if (nextActionButtonIndex < 0)
        {
            return;
        }

        previousIndexChoice = currentIndexChoice;
        currentIndexChoice = nextActionButtonIndex;

        if (previousIndexChoice != -1)
        {
            ActionButtons[previousIndexChoice].GetComponent<Image>().color = NotSelectedButtonColor;
        }

        ActionButtons[nextActionButtonIndex].GetComponent<Image>().color = SelectedButtonColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            StartTurn();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            EndTurn();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            NodeChoice currentNode = NodeChoices[currentIndexChoice];
            NodeChoice.Choices nextNodeChoice = currentNode.UpChoice;
            SelectAction(nextNodeChoice);

        } 
        else if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            NodeChoice currentNode = NodeChoices[currentIndexChoice];
            NodeChoice.Choices nextNodeChoice = currentNode.DownChoice;
            SelectAction(nextNodeChoice);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            NodeChoice currentNode = NodeChoices[currentIndexChoice];
            NodeChoice.Choices nextNodeChoice = currentNode.RightChoice;
            SelectAction(nextNodeChoice);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            NodeChoice currentNode = NodeChoices[currentIndexChoice];
            NodeChoice.Choices nextNodeChoice = currentNode.LeftChoice;
            SelectAction(nextNodeChoice);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // if (playerTurn == false) DO NOTHING
            // TODO: bind keyboard action to trigger the action commande.
            //ActionButtons[nextActionButtonIndex].GetComponent<Button>().TriggerClickAction
        }
    }
}

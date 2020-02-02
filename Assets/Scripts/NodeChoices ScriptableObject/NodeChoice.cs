using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NodeChoice", menuName = "ScriptableObject/NodeChoice")]
public class NodeChoice : ScriptableObject {

    public enum Choices
    {
        AttackRightArm,
        AttackLeftArm,
        RepairRightArm,
        RepairLeftArm,
        RepairBody,
        None
    }

    public Choices CurrentChoice;
    public Choices UpChoice;
    public Choices DownChoice;
    public Choices RightChoice;
    public Choices LeftChoice;

    public static int ActionButtonIndex(NodeChoice.Choices nextNodeChoice)
    {
        switch (nextNodeChoice)
        {
            case NodeChoice.Choices.AttackRightArm:
                return 0;
            case NodeChoice.Choices.AttackLeftArm:
                return 1;
            case NodeChoice.Choices.RepairRightArm:
                return 2;
            case NodeChoice.Choices.RepairLeftArm:
                return 3;
            case NodeChoice.Choices.RepairBody:
                return 4;
            case NodeChoice.Choices.None:
                return -1;
            default:
                return -1;
        }
    }
}

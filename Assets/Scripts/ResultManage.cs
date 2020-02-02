using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultManage : MonoBehaviour
{
    //playerのWindow
	private GameObject window1;//player1
	private GameObject window2;//player2

	//勝者
	private int winner;

	//結果Text
	public Text player1resultText;
	public Text repairs1;
	public Text criticals1;
	public Text fails1;

    public Text player2resultText;
	public Text repairs2;
	public Text criticals2;
	public Text fails2;

	void Start()
	{
		winner = BattleManager.WinPlayer;

		window1 = GameObject.Find("Window1");
		window2 = GameObject.Find("Window2");

		if (winner == 1)
		{
			player1resultText.text = "WIN!!";
			player1resultText.color = new Color(255f / 255f, 0f / 255f, 0f / 255f);

			player2resultText.text = "LOSE...";
			player2resultText.color = new Color(0f / 255f, 245f / 255f, 255f / 255f);
		}
		else if (winner == 2)
		{
			player1resultText.text = "LOSE...";
			player1resultText.color = new Color(0f / 255f, 245f / 255f, 255f / 255f);

			player2resultText.text = "WIN!!";
			player2resultText.color = new Color(255f / 255f, 0f / 255f, 0f / 255f);
		}
		else
		{
			player1resultText.text = "ERROR";
			player1resultText.color = new Color(0f / 255f, 255f / 255f, 0f / 255f);
			player2resultText.text = "ERROR";
			player2resultText.color = new Color(0f / 255f, 255f / 255f, 0f / 255f);
		}

		repairs1.text = "Repairs: ";
		criticals1.text = "Criticals: ";
		fails1.text = "Fails: ";

		repairs2.text = "Repairs: ";
		criticals2.text = "Criticals: ";
		fails2.text = "Fails: ";

	}
    void Update()
    {
		if (Input.GetKeyDown("joystick button 1"))
		{
			window1.SetActive(false);

		} else if (Input.GetKeyDown(KeyCode.L))
        {
			window2.SetActive(false);
        }


		if ( (window1.activeSelf == false) && (window2.activeSelf == false))
        {
			SceneManager.LoadScene("Menu 3D");//Menu 3Dへ遷移
		}

	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
	public Text player2resultText;

	void Start()
	{
		winner = BattleManager.WinPlayer;

		window1 = GameObject.Find("Window1");
		window2 = GameObject.Find("Window2");

		//結果の反映(trueはwin, falseはLose)
		//resultFlag1 = true;
		//resultFlag2 = false;

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

	}
    void Update()
    {
        if ( (window1.activeSelf == false) && (window2.activeSelf == false))
        {
			SceneManager.LoadScene("Menu 3D");//Menu 3Dへ遷移
		}
	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultManage : MonoBehaviour
{
    public void OntButtonClicked()
    {
        SceneManager.LoadScene("Menu 3D");//Menu 3Dへ遷移
    }

	//結果判定用flag
	bool resultFlag1;
	bool resultFlag2;

	//結果Text
	public Text player1resultText;
	public Text player2resultText;


	void Start()
	{
		//結果の反映
		resultFlag1 = true;
		resultFlag2 = false;
		if (resultFlag1 == true && resultFlag2 == false)
		{
			player1resultText.text = "WIN!!";
			player1resultText.color = new Color(255f / 255f, 0f / 255f, 0f / 255f);

			player2resultText.text = "LOSE...";
			player2resultText.color = new Color(0f / 255f, 245f / 255f, 255f / 255f);
		}
		else if (resultFlag1 == false && resultFlag2 == true)
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


}

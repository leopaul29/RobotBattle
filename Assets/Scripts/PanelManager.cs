using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PanelManager : MonoBehaviour {

	public Animator initiallyOpen;

	private int m_OpenParameterId;
	private Animator m_Open;
	private GameObject m_PreviouslySelected;

	const string k_OpenTransitionName = "Open";
	const string k_ClosedStateName = "Closed";

    //結果判定用flag
	bool resultFlag1;
	bool resultFlag2;

    //結果Text
	public Text player1resultText;
	public Text player2resultText;


	void Start()
    {
	    if (SceneManager.GetActiveScene().name != "Result")
	    {
		    return;
	    }
	    
        //結果の反映
		resultFlag1 = true;
		resultFlag2 = false;
        if(resultFlag1 == true && resultFlag2 == false)
        {
			player1resultText.text = "WIN!!";
			player1resultText.color = new Color(255f / 255f, 0f / 255f, 0f / 255f);

            player2resultText.text = "LOSE...";
			player2resultText.color = new Color(0f / 255f, 245f / 255f, 255f / 255f);
        }else if(resultFlag1 == false && resultFlag2 == true){
			player1resultText.text = "LOSE...";
			player1resultText.color = new Color(0f / 255f, 245f / 255f, 255f / 255f);

			player2resultText.text = "WIN!!";
			player2resultText.color = new Color(255f / 255f, 0f / 255f, 0f / 255f);
		}else {
			player1resultText.text = "ERROR";
			player1resultText.color = new Color(0f / 255f, 255f / 255f, 0f / 255f);
			player2resultText.text = "ERROR";
			player2resultText.color = new Color(0f / 255f, 255f / 255f, 0f / 255f);
		}

    }

    public void OnEnable()
	{
		m_OpenParameterId = Animator.StringToHash (k_OpenTransitionName);

		if (initiallyOpen == null)
			return;

		OpenPanel(initiallyOpen);
	}

	public void OpenPanel (Animator anim)
	{
		if (m_Open == anim)
			return;

		anim.gameObject.SetActive(true);
		var newPreviouslySelected = EventSystem.current.currentSelectedGameObject;

		anim.transform.SetAsLastSibling();

		CloseCurrent();

		m_PreviouslySelected = newPreviouslySelected;

		m_Open = anim;
		m_Open.SetBool(m_OpenParameterId, true);

		GameObject go = FindFirstEnabledSelectable(anim.gameObject);

		SetSelected(go);
	}

	static GameObject FindFirstEnabledSelectable (GameObject gameObject)
	{
		GameObject go = null;
		var selectables = gameObject.GetComponentsInChildren<Selectable> (true);
		foreach (var selectable in selectables) {
			if (selectable.IsActive () && selectable.IsInteractable ()) {
				go = selectable.gameObject;
				break;
			}
		}
		return go;
	}

	public void CloseCurrent()
	{
		if (m_Open == null)
			return;

		m_Open.SetBool(m_OpenParameterId, false);
		SetSelected(m_PreviouslySelected);
		StartCoroutine(DisablePanelDeleyed(m_Open));
		m_Open = null;
	}

	IEnumerator DisablePanelDeleyed(Animator anim)
	{
		bool closedStateReached = false;
		bool wantToClose = true;
		while (!closedStateReached && wantToClose)
		{
			if (!anim.IsInTransition(0))
				closedStateReached = anim.GetCurrentAnimatorStateInfo(0).IsName(k_ClosedStateName);

			wantToClose = !anim.GetBool(m_OpenParameterId);

			yield return new WaitForEndOfFrame();
		}

		if (wantToClose)
			anim.gameObject.SetActive(false);
	}

	private void SetSelected(GameObject go)
	{
		EventSystem.current.SetSelectedGameObject(go);
	}
}

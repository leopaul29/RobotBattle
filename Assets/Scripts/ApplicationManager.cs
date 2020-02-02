using System;
using UnityEngine;
using System.Collections;

public class ApplicationManager : MonoBehaviour
{

	[SerializeField] private LevelLoader _levelLoader;

	public void Quit () 
	{
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit();
		#endif
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Joystick1Button1) || Input.GetKeyDown(KeyCode.L))
		{
			if (_levelLoader != null)
			{
				_levelLoader.LoadMenu();
			}
		}
	}
}

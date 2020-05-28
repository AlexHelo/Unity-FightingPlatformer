using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSceneManager : MonoBehaviour
{
	public void StartButton()
	{
		Debug.Log("start");
		SceneManager.LoadScene("firstLevel");
	}
	public void ExitButton()
	{
		Debug.Log("exit");
		Application.Quit();
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuSceneManager : MonoBehaviour
{
	public GameObject img;
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
	public void OpenControllers()
	{
		Debug.Log("open");
		img.SetActive(true);
	}
	public void CloseControllers()
	{
		Debug.Log("close");
		img.SetActive(false);
	}
}

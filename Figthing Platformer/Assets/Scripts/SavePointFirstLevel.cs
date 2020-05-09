using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SavePointFirstLevel : MonoBehaviour
{
	public GameObject chP;
	public CheckpointCheck cc;
    // Start is called before the first frame update
    void Start()
    {
		cc = GameObject.FindGameObjectWithTag("CC").GetComponent<CheckpointCheck>();
	}

	// Update is called once per frame
	void Update()
    {
        
    }
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			Destroy(chP);
			SceneManager.LoadScene("RuinsScene");
		}
	}
}

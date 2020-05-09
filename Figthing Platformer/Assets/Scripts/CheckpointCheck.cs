using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointCheck : MonoBehaviour
{
    private static CheckpointCheck instance;
    public Vector2 lastCheckpoint;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
		if(SceneManager.GetActiveScene().name != "firstLevel")
		{
			Destroy(gameObject);
		}
    }
}

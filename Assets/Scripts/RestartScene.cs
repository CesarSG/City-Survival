using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class RestartScene : MonoBehaviour {

    public Button restartButton;
    UnityEvent RestartWithR = new UnityEvent();



	// Use this for initialization
	void Start () {
        restartButton = GetComponent<Button>();
        restartButton.image.gameObject.SetActive(false);
        restartButton.onClick.AddListener(TaskOnClick);
        RestartWithR.AddListener(RestartGame);
	}

    void TaskOnClick()
    {
        SceneManager.LoadScene("SceneRoll");
    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("r") && RestartWithR != null)
        {
            RestartWithR.Invoke();
        }
	}

    void RestartGame()
    {
        SceneManager.LoadScene("SceneRoll");
    }
}

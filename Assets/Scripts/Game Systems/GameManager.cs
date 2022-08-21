using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] Button _nextSceneButton;
    
    // Start is called before the first frame update
    void Start()
    {
        _nextSceneButton.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartScene();
        }
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(gameObject.scene.name);
    }

    public void NextScene()
    {
        if (SceneManager.GetSceneByBuildIndex(gameObject.scene.buildIndex + 1) != null)
        SceneManager.LoadScene(gameObject.scene.buildIndex + 1);
    }

    public void EnableNextScene()
    {
        _nextSceneButton.interactable = true;
    }
}

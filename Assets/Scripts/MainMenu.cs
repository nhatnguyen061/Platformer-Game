using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string startScene,continueScene;
    // Start is called before the first frame update
    public GameObject continueButton;
    void Start()
    {
        if(PlayerPrefs.HasKey(startScene + "_unlocked"))
        {
            continueButton.SetActive(true);
        }
        else
        {
            continueButton.SetActive(false);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene(startScene);
        // delete all old information in object when choose new game button in main_menu
        PlayerPrefs.DeleteAll();

    }
    public void ContinueGame()
    {
        SceneManager .LoadScene(continueScene);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

}

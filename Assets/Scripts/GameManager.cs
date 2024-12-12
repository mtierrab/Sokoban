using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
      public Button[] buttons;
  
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }

   public void CheckLevelComplete()
    {
        foreach(Button button in buttons)
        {
            if(!button.isActive)
                return;
        }
        Debug.Log("level complete");
        EndLevel();
    }

    void EndLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void nextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }



}

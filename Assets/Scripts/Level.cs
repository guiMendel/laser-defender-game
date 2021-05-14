using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
  public void LoadGame()
  {
    SceneManager.LoadScene("Game");
    
    // Reset score
    GameSession gameSession = FindObjectOfType<GameSession>();
    gameSession.ResetScore();
  }
  public void LoadGameOver(int gameoverTimeout = 2)
  {
    StartCoroutine(TimeoutGameover(gameoverTimeout));
  }

  IEnumerator TimeoutGameover(int gameoverTimeout)
  {
    yield return new WaitForSeconds(gameoverTimeout);
    SceneManager.LoadScene("Game Over");
  }


  public void QuitGame()
  {
    Application.Quit();
  }
}

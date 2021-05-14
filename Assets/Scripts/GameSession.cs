using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
  [SerializeField] int playerScore = 0;
  
  public void AddToScore(int amount) {
    playerScore += amount;
    Debug.Log(playerScore);
  }

  public int GetScore() {
    return playerScore;
  }

  public void ResetScore() {
    playerScore = 0;
  }
  
  void Awake()
  {
    SetUpSingleton();
  }

  private void SetUpSingleton()
  {
    // if theres already one, destroy self
    if (FindObjectsOfType(GetType()).Length > 1)
    {
      gameObject.SetActive(false);
      Destroy(gameObject);
    }
    // If not, set up immortality!!!!
    else
    {
      DontDestroyOnLoad(gameObject);
    }
  }
  
  // Update is called once per frame
  void Update()
  {

  }
}

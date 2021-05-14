using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplayer : MonoBehaviour
{
  GameSession gameSession;
  TextMeshProUGUI textComponent;

  // Start is called before the first frame update
  void Start()
  {
    gameSession = FindObjectOfType<GameSession>();
    textComponent = GetComponent<TextMeshProUGUI>();
  }

  // Update is called once per frame
  void Update()
  {
    // Update text
    textComponent.text = gameSession.GetScore().ToString();
  }
}

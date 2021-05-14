using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthDisplayer : MonoBehaviour
{
  Player player;
  TextMeshProUGUI textComponent;

  // Start is called before the first frame update
  void Start()
  {
    player = FindObjectOfType<Player>();
    textComponent = GetComponent<TextMeshProUGUI>();
  }

  // Update is called once per frame
  void Update()
  {
    // Update text
    textComponent.text = player.GetHealth().ToString();
  }
}

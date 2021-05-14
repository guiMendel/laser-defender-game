using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
  [SerializeField] float scrollSpeed = 0.02f;
  Material quadMaterial;
  Vector2 offset;


  // Start is called before the first frame update
  void Start()
  {
    quadMaterial = GetComponent<Renderer>().material;
    offset = new Vector2(0f, scrollSpeed);
  }

  // Update is called once per frame
  void Update()
  {
    quadMaterial.mainTextureOffset += offset * Time.deltaTime;
  }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPos : MonoBehaviour
{
  // Create an array of vector3's that contain the dead ends of the mazes
  GameObject key;
  GameObject player;
  Vector3[] positionArr = new [] { new Vector3(3.066f, 0f, 5.211f),
                                   new Vector3(-9.64f, 0f, 7.16f),
                                   new Vector3(-9.64f, 0f, -3.75f),
                                   new Vector3(3.21f, 0f, -1.36f),
                                   new Vector3(9.48f, 0f, 4.82f),
                                   new Vector3(9.48f, 0f, 0.67f),
                                   new Vector3(5f, 0f, -5.27f),
                                   new Vector3(-3f, 0f, -3.14f),
                                   new Vector3(-1.25f, 0f, -9f),
                                   new Vector3(1.12f, 0f, -9f)};

  // Set the position to a random vector from the array
  void Start(){
    key = GameObject.FindGameObjectWithTag("Key");
    player = GameObject.FindGameObjectWithTag("Player");

    int index1 = Random.Range(0, positionArr.Length);
    int index2 = Random.Range(0, positionArr.Length);;
  
    while(index1 == index2)
      index2 = Random.Range(0, positionArr.Length);

    key.transform.position = positionArr[index1] + new Vector3(0f, 0.682f, 0f);
    player.transform.position = positionArr[index2];
  }

}

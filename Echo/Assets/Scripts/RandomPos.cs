using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPos : MonoBehaviour
{
  // Create an array of vector3's that contain the dead ends of the mazes
  Vector3[] positionArr = new [] { new Vector3(3.066f, 0.682f, 5.211f),
                                   new Vector3(-9.64f, 0.682f, 7.16f),
                                   new Vector3(-9.64f, 0.682f, -3.75f),
                                   new Vector3(3.21f, 0.682f, -1.36f),
                                   new Vector3(9.48f, 0.682f, 4.82f),
                                   new Vector3(9.48f, 0.682f, 0.67f)};

  // Set the position to a random vector from the array
  void Start(){
    transform.position = positionArr[Random.Range(0, positionArr.Length)];
  }

}

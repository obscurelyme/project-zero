using UnityEngine;

public class DroneMovement : MonoBehaviour
{
  [SerializeField]
  float movementSpeed = 4.5f;

  Vector3 newPosition = new Vector3();
  bool moveLeft = false;

  void Start()
  {
  }

  void Update()
  {
    newPosition = transform.position;

    if (moveLeft)
    {
      newPosition.x = Mathf.Clamp(newPosition.x - movementSpeed * Time.deltaTime, -8, 8);
      if (newPosition.x == -8)
      {
        moveLeft = false;
      }
    }
    else
    {
      newPosition.x = Mathf.Clamp(newPosition.x + movementSpeed * Time.deltaTime, -8, 8);
      if (newPosition.x == 8)
      {
        moveLeft = true;
      }
    }

    transform.position = newPosition;
  }
}

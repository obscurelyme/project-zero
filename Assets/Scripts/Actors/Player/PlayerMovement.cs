using UnityEngine;
using Zero;

public class PlayerMovement : MonoBehaviour
{
  PlayerInputActions.PlayerActions inputActions;
  // Transform playerTransform;
  UnityEngine.Vector2 movement;
  UnityEngine.Vector3 currentPos;
  UnityEngine.Quaternion leftRoation;
  UnityEngine.Quaternion rightRoation;
  UnityEngine.Quaternion forwardRoation;

  [SerializeField]
  float rotateSpeed = 110.0f;
  [SerializeField]
  float movementSpeed = 7.0f;
  [SerializeField]
  float clampLeft = -8.0f;
  [SerializeField]
  float clampRight = 8.0f;

  void Start()
  {
    inputActions = GetComponent<PlayerInput>().PlayerActions;
    leftRoation.eulerAngles = new UnityEngine.Vector3(0f, 0f, 10f);
    rightRoation.eulerAngles = new UnityEngine.Vector3(0f, 0f, -10f);
    forwardRoation.eulerAngles = new UnityEngine.Vector3(0f, 0f, 0f);
  }

  void Update()
  {
    currentPos = transform.position;
    movement = inputActions.Move.ReadValue<UnityEngine.Vector2>();
    if (movement.x == -1)
    {
      // NOTE: go left
      currentPos.x = UnityEngine.Mathf.Clamp(currentPos.x - movementSpeed * Time.deltaTime, clampLeft, clampRight);
      // NOTE: rotate to the left
      transform.rotation = UnityEngine.Quaternion.RotateTowards(transform.rotation, leftRoation, rotateSpeed * Time.deltaTime);
    }
    if (movement.x == 1)
    {
      // NOTE: go right
      currentPos.x = UnityEngine.Mathf.Clamp(currentPos.x + movementSpeed * Time.deltaTime, clampLeft, clampRight);
      // NOTE: rotate to the right
      transform.rotation = UnityEngine.Quaternion.RotateTowards(transform.rotation, rightRoation, rotateSpeed * Time.deltaTime);
    }
    if (movement.x == 0)
    {
      // NOTE: rotate to the center again
      transform.rotation = UnityEngine.Quaternion.RotateTowards(transform.rotation, forwardRoation, rotateSpeed * Time.deltaTime);
    }

    transform.position = currentPos;
  }
}

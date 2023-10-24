using UnityEngine;

public class Missile : MonoBehaviour
{
  [SerializeField]
  float speed = 10.0f;
  Vector3 firedPosition;
  Vector3 newPosition;
  Vector3 goalPosition;
  bool isFired = false;

  void Start()
  {

  }

  void Update()
  {
    if (isFired)
    {
      newPosition.y = Mathf.Lerp(firedPosition.y, goalPosition.y, speed * Time.deltaTime);
      transform.position = newPosition;
    }
  }

  void OnTriggerEnter2D(Collider2D collider2D)
  {
    Reset();
  }

  void Reset()
  {

  }

  public void Fire()
  {
    firedPosition = transform.position;
    goalPosition = new Vector3(firedPosition.x, firedPosition.y + 15, firedPosition.z);
    isFired = true;
  }
}

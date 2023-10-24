using UnityEngine;

public class Missile : MonoBehaviour
{
  [SerializeField]
  float speed = 10.0f;
  [SerializeField]
  Vector3 firedPosition;
  [SerializeField]
  Vector3 currentPosition;
  [SerializeField]
  Vector3 goalPosition;
  bool isFired = false;
  [SerializeField]
  [Range(0, 1)]
  float flightProgress = 0.0f;

  void Start()
  {
  }

  void Update()
  {
    if (isFired)
    {
      flightProgress += speed * Time.deltaTime;
      flightProgress = Mathf.Clamp(flightProgress, 0.0f, 1.0f);
      currentPosition = transform.position;
      currentPosition.y = Mathf.Lerp(firedPosition.y, goalPosition.y, flightProgress);
      transform.position = currentPosition;

      if (flightProgress == 1.0f)
      {
        Reset();
      }
    }
  }

  void OnTriggerEnter2D(Collider2D collider2D)
  {
    Reset();
  }

  void Reset()
  {
    // sprite.enabled = false;
    isFired = false;
    flightProgress = 0.0f;
  }

  public void Fire(Vector3 pos)
  {
    if (isFired)
    {
      // NOTE: do NOT allow duplicate firing.
      return;
    }
    transform.position = pos;
    firedPosition = transform.position;
    goalPosition = new Vector3(firedPosition.x, firedPosition.y + 10, firedPosition.z);
    isFired = true;
  }
}

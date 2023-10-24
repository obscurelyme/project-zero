using UnityEngine;
using UnityEngine.UIElements;

public class HeadsUpDisplay : MonoBehaviour
{
  private StageTimer timer;
  private UIDocument document;

  private Zero.Text timeText;
  private Zero.Text playerLives;

  void Start()
  {
    timer = GetComponent<StageTimer>();
    document = GetComponent<UIDocument>();
    var root = document.rootVisualElement;
    timeText = root.Q<Zero.Text>("stage-time");
    playerLives = root.Q<Zero.Text>("player-lives");
  }

  void Update()
  {
    timeText.text = timer.GetTime();
  }
}

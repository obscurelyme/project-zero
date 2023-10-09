using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Zero;

public class MainMenu : MonoBehaviour
{
  [SerializeField]
  private UIDocument document;
  private VisualElement root;

  private Zero.Button playGameBtn;
  private Zero.Button quitGameBtn;
  private Zero.Dialog quitGameDialog;

  void Start()
  {
    root = document.rootVisualElement;
    playGameBtn = root.Query<Zero.Button>("play-game");
    quitGameBtn = root.Query<Zero.Button>("quit-game");

    playGameBtn.RegisterCallback<ClickEvent>(PlayGame);
    playGameBtn.RegisterCallback<NavigationSubmitEvent>(PlayGame);

    quitGameBtn.RegisterCallback<ClickEvent>(QuitGame);
    quitGameBtn.RegisterCallback<NavigationSubmitEvent>(QuitGame);

    quitGameDialog = root.Query<Zero.Dialog>("quit-game");
    quitGameDialog.OnConfirm += CloseApplication;
  }

  void PlayGame(ClickEvent evt)
  {
    SceneManager.LoadScene("Stage_1");
  }

  void PlayGame(NavigationSubmitEvent evt)
  {
    SceneManager.LoadScene("Stage_1");
  }

  void QuitGame(ClickEvent evt)
  {
    DialogPortal.OpenDialog("quit-game");
  }

  void QuitGame(NavigationSubmitEvent evt)
  {
    DialogPortal.OpenDialog("quit-game");
  }

  private void CloseApplication()
  {
#if UNITY_EDITOR
    if (UnityEditor.EditorApplication.isPlaying)
    {
      UnityEditor.EditorApplication.isPlaying = false;
    }
#else
    Application.Quit();
#endif
  }
}

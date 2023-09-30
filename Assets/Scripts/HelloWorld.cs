using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class HelloWorld : MonoBehaviour
{
  [SerializeField] UIDocument _document;

  List<Button> buttons;
  List<Zero.Dialog> dialogs;

  private void Start()
  {
    var root = _document.rootVisualElement;
    dialogs = root.Query<Zero.Dialog>().ToList();
    buttons = root.Query<Button>(className: "dialog-button-activator").ToList();

    dialogs[4].OnConfirm += CloseApplication;

    buttons[0].RegisterCallback<ClickEvent>((evt) => dialogs[0].Open = true);
    buttons[1].RegisterCallback<ClickEvent>((evt) => dialogs[1].Open = true);
    buttons[2].RegisterCallback<ClickEvent>((evt) => dialogs[2].Open = true);
    buttons[3].RegisterCallback<ClickEvent>((evt) => dialogs[3].Open = true);
    buttons[4].RegisterCallback<ClickEvent>((evt) => dialogs[4].Open = true);
  }

  private void OnEnable() { }

  private void OnDisable() { }

  private void CloseApplication()
  {
#if UNITY_EDITOR
    if (EditorApplication.isPlaying)
    {
      EditorApplication.isPlaying = false;
    }
#else
    Application.Quit();
#endif
  }
}

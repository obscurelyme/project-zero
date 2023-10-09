using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using Zero;

public class HelloWorld : MonoBehaviour
{
  [SerializeField] UIDocument _document;
  List<Zero.Button> buttons;
  List<Zero.Dialog> dialogs;
  PlayerInputActions input;
  Vector2 movementInputVector2;

  private void Awake()
  {
    input = new PlayerInputActions();
  }

  private void Start()
  {
    var root = _document.rootVisualElement;
    dialogs = root.Query<Zero.Dialog>().ToList();
    buttons = root.Query<Zero.Button>(className: "dialog-button-activator").ToList();

    dialogs[2].OnConfirm += () =>
    {
      Debug.Log("Third dialog was confirmed.");
    };
    dialogs[4].OnConfirm += CloseApplication;

    buttons[0].RegisterCallback<NavigationSubmitEvent>((evt) => dialogs[0].Open = true);
    buttons[0].RegisterCallback<ClickEvent>(evt =>
    {
      DialogPortal.OpenDialog("FirstDialog");
    });
    buttons[1].RegisterCallback<ClickEvent>(evt =>
    {
      DialogPortal.OpenDialog("SecondDialog");
    });
    buttons[2].RegisterCallback<ClickEvent>(evt =>
    {
      DialogPortal.OpenDialog("ThirdDialog");
    });
    buttons[3].RegisterCallback<ClickEvent>(evt =>
    {
      DialogPortal.OpenDialog("FourthDialog");
    });
    buttons[4].RegisterCallback<ClickEvent>(evt =>
    {
      DialogPortal.OpenDialog("FifthDialog");
    });
  }

  private void Update()
  {
    movementInputVector2 = input.Player.Move.ReadValue<Vector2>();
  }

  private void OnEnable()
  {
    input.Player.Enable();
  }

  private void OnDisable()
  {
    input.Player.Disable();
  }

  private void OnNavigation()
  {

  }

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

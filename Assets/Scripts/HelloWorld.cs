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

  private void ActivateAllButtons()
  {
    foreach (var btn in buttons)
    {
      btn.focusable = true;
    }
  }

  private void DeactivateAllButtons()
  {
    foreach (var btn in buttons)
    {
      btn.focusable = false;
    }
  }

  private void Start()
  {
    Zero.Dialog.GlobalDialogClosed += ActivateAllButtons;
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
      dialogs[0].Open = true;
      DeactivateAllButtons();
    });
    buttons[1].RegisterCallback<ClickEvent>(evt =>
    {
      dialogs[1].Open = true;
      DeactivateAllButtons();

    });
    buttons[2].RegisterCallback<ClickEvent>(evt =>
    {
      dialogs[2].Open = true;
      DeactivateAllButtons();

    });
    buttons[3].RegisterCallback<ClickEvent>(evt =>
    {
      dialogs[3].Open = true;
      DeactivateAllButtons();

    });
    buttons[4].RegisterCallback<ClickEvent>(evt =>
    {
      dialogs[4].Open = true;
      DeactivateAllButtons();

    });

    buttons[0].RegisterCallback<NavigationMoveEvent>(evt =>
    {
      Debug.Log($"{evt.currentTarget}");
    });
    buttons[1].RegisterCallback<NavigationMoveEvent>(evt =>
    {
      Debug.Log($"{evt.currentTarget}");
    });
    buttons[2].RegisterCallback<NavigationMoveEvent>(evt =>
    {
      Debug.Log($"{evt.currentTarget}");
    });
    buttons[3].RegisterCallback<NavigationMoveEvent>(evt =>
    {
      Debug.Log($"{evt.currentTarget}");
    });
    buttons[4].RegisterCallback<NavigationMoveEvent>(evt =>
    {
      Debug.Log($"{evt.currentTarget}");
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

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

namespace Zero
{
  public enum DialogType
  {
    /// <summary>
    /// Confirmation Dialogs contain a Yes/No buttons to allow the player to confirm their behavior
    /// </summary>
    Confirmation,
    /// <summary>
    /// Info Dialogs contain only a single "acknowledge" button to dismiss,
    /// these dialogs are used to inform the player of something
    /// that does not require a Yes/No response
    /// </summary>
    Info
  }

  /// <summary>
  /// Zero Dialog Element UI
  /// </summary>
  public class Dialog : VisualElement
  {
    private bool _open = false;
    private DialogType _type;
    private string _name;
    public new class UxmlFactory : UxmlFactory<Dialog> { }

    /// <summary>
    /// Event fired when the confirm button is clicked.
    /// </summary>
    public Action OnConfirm;
    /// <summary>
    /// Event fired when the cancel button is clicked.
    /// </summary>
    public Action OnCancel;

    public static Action OnDialogOpen;
    public static Action OnDialogClose;

    Label titleText => this.Q<Label>("dialog-title-content");
    Label contentText => this.Q<Label>("dialog-content-text");
    Button confirmButton => this.Q<Button>("dialog-confirm-button");
    Button cancelButton => this.Q<Button>("dialog-cancel-button");

    public Dialog()
    {
      LoadUXMLAsset();
      // NOTE: Very basic defaults just so we don't have totally invalid values for the dialog
      _type = DialogType.Info;
      _name = $"Dialog-{System.Guid.NewGuid()}";
      contentText.text = _name;

      confirmButton.RegisterCallback<ClickEvent>(evt =>
      {
        if (OnConfirm != null)
        {
          OnConfirm.Invoke();
        }
        Open = false;
      });
      cancelButton.RegisterCallback<ClickEvent>(evt =>
      {
        if (OnCancel != null)
        {
          OnCancel.Invoke();
        }
        Open = false;
      });

      InitializeUI();
    }

    public Dialog(DialogType type, string name, string title, string description)
    {
      LoadUXMLAsset();
      _type = type;
      _name = name;
      titleText.text = title;
      contentText.text = description;

      confirmButton.RegisterCallback<ClickEvent>(evt =>
      {
        if (OnConfirm != null)
        {
          OnConfirm.Invoke();
        }
        Open = false;
      });
      cancelButton.RegisterCallback<ClickEvent>(evt =>
      {
        if (OnCancel != null)
        {
          OnCancel.Invoke();
        }
        Open = false;
      });

      InitializeUI();

      // NOTE: Sample code for testing, do not use.
      // contentText.text = "Hello";
      // contentText.text = $"Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum. Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.\n\nLorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.\n\nLorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
    }

    public Dialog(
      DialogType type,
      string name,
      string title,
      string description,
      Action onConfirmCallback,
      Action onCancelCallback)
    {
      LoadUXMLAsset();
      _type = type;
      _name = name;
      titleText.text = title;
      contentText.text = description;

      OnConfirm += onConfirmCallback;
      OnCancel += onCancelCallback;

      confirmButton.RegisterCallback<ClickEvent>(evt =>
      {
        OnConfirm.Invoke();
        Open = false;
      });
      cancelButton.RegisterCallback<ClickEvent>(evt =>
      {
        OnCancel.Invoke();
        Open = false;
      });

      InitializeUI();

      // NOTE: Sample code for testing, do not use.
      // contentText.text = "Hello";
      // contentText.text = $"Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum. Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.\n\nLorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.\n\nLorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
    }

    private void LoadUXMLAsset()
    {
      var asset = Resources.Load<VisualTreeAsset>("UI/Dialog");
      asset.CloneTree(this);
    }

    private void InitializeUI()
    {
      if (_type == DialogType.Info)
      {
        cancelButton.AddToClassList("dialog-hidden-element");
      }
      Open = false;
    }

    public bool Open
    {
      get
      {
        return _open;
      }
      set
      {
        _open = value;
        if (_open)
        {
          this.RemoveFromClassList("dialog-hidden-element");
          if (OnDialogOpen != null)
          {
            OnDialogOpen.Invoke();
          }
        }
        else
        {
          this.AddToClassList("dialog-hidden-element");
          if (OnDialogClose != null)
          {
            OnDialogClose.Invoke();
          }
        }
      }
    }

    public string Name
    {
      get { return _name; }
    }

    public DialogType Type
    {
      get { return _type; }
    }
  }
}

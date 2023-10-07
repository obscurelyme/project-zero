#nullable enable

using System;
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
    public static Action? GlobalDialogClosed;
    private bool _open = false;
    private DialogType _type = DialogType.Info;
    private string _title = "";
    private string _description = "";
    public new class UxmlFactory : UxmlFactory<Dialog, UxmlTraits> { }

    public new class UxmlTraits : VisualElement.UxmlTraits
    {
      readonly UxmlEnumAttributeDescription<DialogType> _typeAttr = new UxmlEnumAttributeDescription<DialogType>
      {
        name = "type",
        defaultValue = DialogType.Info
      };

      readonly UxmlStringAttributeDescription _descriptionAttr = new UxmlStringAttributeDescription
      {
        name = "description",
        defaultValue = ""
      };

      readonly UxmlStringAttributeDescription _titleAttr = new UxmlStringAttributeDescription
      {
        name = "title",
        defaultValue = ""
      };

      public override void Init(VisualElement ve, IUxmlAttributes attrs, CreationContext ctx)
      {
        base.Init(ve, attrs, ctx);
        Dialog thisDialog = (ve as Dialog)!;

        thisDialog.Type = _typeAttr.GetValueFromBag(attrs, ctx);
        thisDialog.Description = _descriptionAttr.GetValueFromBag(attrs, ctx);
        thisDialog.Title = _titleAttr.GetValueFromBag(attrs, ctx);
      }
    }

    /// <summary>
    /// Event fired when the confirm button is clicked.
    /// </summary>
    public Action? OnConfirm;
    /// <summary>
    /// Event fired when the cancel button is clicked.
    /// </summary>
    public Action? OnCancel;

    public static Action? OnDialogOpen;
    public static Action? OnDialogClose;

    Label titleText => this.Q<Label>("dialog-title-content");
    Label contentText => this.Q<Label>("dialog-content-text");
    Button confirmButton => this.Q<Button>("dialog-confirm-button");
    Button cancelButton => this.Q<Button>("dialog-cancel-button");
    VisualElement container => this.Q<VisualElement>(className: "dialog-container");

    public Dialog()
    {
      LoadUXMLAsset();
      // NOTE: Very basic defaults just so we don't have totally invalid values for the dialog
      Type = DialogType.Info;
      contentText.text = name;

      this.RegisterCallback<NavigationCancelEvent>(evt =>
      {
        OnCancel?.Invoke();
        Open = false;
      });
      confirmButton.RegisterCallback<ClickEvent>(evt =>
      {
        OnConfirm?.Invoke();
        Open = false;
      });
      confirmButton.RegisterCallback<NavigationSubmitEvent>(evt =>
      {
        OnConfirm?.Invoke();
        Open = false;
      });
      cancelButton.RegisterCallback<ClickEvent>(evt =>
      {
        OnCancel?.Invoke();
        Open = false;
      });
      cancelButton.RegisterCallback<NavigationSubmitEvent>(evt =>
      {
        OnCancel?.Invoke();
        Open = false;
      });

      InitializeUI();
    }

    public Dialog(DialogType type, string name, string title, string description)
    {
      LoadUXMLAsset();
      Type = type;
      Title = title;
      this.name = name;
      Description = description;

      confirmButton.RegisterCallback<ClickEvent>(evt =>
      {
        OnConfirm?.Invoke();
        Open = false;
      });
      cancelButton.RegisterCallback<ClickEvent>(evt =>
      {
        OnCancel?.Invoke();
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
      this.name = name;
      Type = type;
      Title = title;
      Description = description;

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
      container.AddToClassList("dialog-size-medium");
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
            confirmButton.Focus();

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

    public string Title
    {
      get { return _title; }
      set
      {
        _title = value;
        titleText.text = _title;
      }
    }

    public string Description
    {
      get { return _description; }
      set
      {
        _description = value;
        contentText.text = _description;
      }
    }
    public DialogType Type
    {
      get { return _type; }
      set
      {
        _type = value;
        ToggleCancelButton();
      }
    }
    private void ToggleCancelButton()
    {
      if (_type == DialogType.Confirmation)
      {
        cancelButton.RemoveFromClassList("dialog-hidden-element");
        return;
      }
      cancelButton.AddToClassList("dialog-hidden-element");
    }
  }
}

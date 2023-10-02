using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Zero
{
  public enum ButtonSize
  {
    Small,
    Medium,
    Large
  }

  public class Button : UnityEngine.UIElements.Button
  {
    ButtonSize _priorSize = ButtonSize.Medium;
    ButtonSize _currentSize = ButtonSize.Medium;
    ButtonSize Size
    {
      get { return _currentSize; }
      set
      {
        _priorSize = _currentSize;
        _currentSize = value;
        RemoveUnusedButtonSizeClass();
        AddUsedButtonSizeClass();
      }
    }

    public new class UxmlFactory : UxmlFactory<Button, UxmlTraits> { }
    public new class UxmlTraits : UnityEngine.UIElements.Button.UxmlTraits
    {
      readonly UxmlEnumAttributeDescription<ButtonSize> _sizeAttr = new UxmlEnumAttributeDescription<ButtonSize>
      {
        name = "size",
        defaultValue = ButtonSize.Medium
      };

      public override void Init(VisualElement ve, IUxmlAttributes attrs, CreationContext ctx)
      {
        base.Init(ve, attrs, ctx);
        var el = (ve as Button)!;

        el.Size = _sizeAttr.GetValueFromBag(attrs, ctx);
      }
    }

    public new static readonly string ussClassName = "zero-button";

    public Button()
    {
      AddToClassList(ussClassName);
    }

    private void RemoveUnusedButtonSizeClass()
    {
      switch (_priorSize)
      {
        case ButtonSize.Small:
          {
            RemoveFromClassList("button-size-small");
            break;
          }
        case ButtonSize.Large:
          {
            RemoveFromClassList("button-size-large");
            break;
          }
        case ButtonSize.Medium:
          {
            RemoveFromClassList("button-size-medium");
            break;
          }
      }
    }
    private void AddUsedButtonSizeClass()
    {
      switch (_currentSize)
      {
        case ButtonSize.Small:
          {
            AddToClassList("button-size-small");
            break;
          }
        case ButtonSize.Large:
          {
            AddToClassList("button-size-large");
            break;
          }
        case ButtonSize.Medium:
          {
            AddToClassList("button-size-medium");
            break;
          }
      }
    }
  }
}


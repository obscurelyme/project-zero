using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Zero
{
  public class Container : VisualElement
  {
    UnityEngine.UIElements.Justify _justifyContent;
    UnityEngine.UIElements.Align _alignItems;
    UnityEngine.UIElements.FlexDirection _direction;

    public UnityEngine.UIElements.FlexDirection FlexDirection
    {
      get { return _direction; }
      set
      {
        _direction = value;
        if (_direction == UnityEngine.UIElements.FlexDirection.Row)
        {
          AddToClassList("zero-container-row");
          return;
        }
        RemoveFromClassList("zero-container-row");
      }
    }
    public UnityEngine.UIElements.Justify JustifyContent
    {
      get
      {
        return _justifyContent;
      }
      set
      {
        _justifyContent = value;
        ClearJustifyContentClass();
        ApplyJustifyContentClass();
      }
    }
    public UnityEngine.UIElements.Align AlignItems
    {
      get
      {
        return _alignItems;
      }
      set
      {
        _alignItems = value;
        ClearAlignItemsClass();
        ApplyAlignItemsClass();
      }
    }

    private void ApplyJustifyContentClass()
    {
      switch (_justifyContent)
      {
        case Justify.Center:
          {
            AddToClassList("justify-content-center");
            break;
          }
        case Justify.SpaceBetween:
          {
            AddToClassList("justify-content-space-between");
            break;
          }
        case Justify.SpaceAround:
          {
            AddToClassList("justify-content-space-around");
            break;
          }
        case Justify.FlexEnd:
          {
            AddToClassList("justify-content-end");
            break;
          }
        default:
        case Justify.FlexStart:
          {
            AddToClassList("justify-content-start");
            break;
          }
      }
    }
    private void ApplyAlignItemsClass()
    {
      switch (_alignItems)
      {
        case Align.FlexStart:
          {
            AddToClassList("align-items-start");
            break;
          }
        case Align.FlexEnd:
          {
            AddToClassList("align-items-end");
            break;
          }
        case Align.Center:
          {
            AddToClassList("align-items-center");
            break;
          }
        default:
        case Align.Stretch:
          {
            AddToClassList("align-items-stretch");
            break;
          }
      }
    }
    private void ClearJustifyContentClass()
    {
      RemoveFromClassList("justify-content-start");
      RemoveFromClassList("justify-content-end");
      RemoveFromClassList("justify-content-center");
      RemoveFromClassList("justify-content-space-between");
      RemoveFromClassList("justify-content-space-around");
    }
    private void ClearAlignItemsClass()
    {
      RemoveFromClassList("align-items-start");
      RemoveFromClassList("align-items-end");
      RemoveFromClassList("align-items-center");
      RemoveFromClassList("align-items-stretch");
    }

    public new class UxmlFactory : UxmlFactory<Container, UxmlTraits> { }

    public new class UxmlTraits : VisualElement.UxmlTraits
    {
      readonly UxmlEnumAttributeDescription<UnityEngine.UIElements.Justify> _justifyContentAttr = new UxmlEnumAttributeDescription<UnityEngine.UIElements.Justify>
      {
        name = "justifyContent",
        defaultValue = Justify.FlexStart
      };

      readonly UxmlEnumAttributeDescription<UnityEngine.UIElements.Align> _alignItemsAttr = new UxmlEnumAttributeDescription<UnityEngine.UIElements.Align>
      {
        name = "alignItems",
        defaultValue = Align.Stretch
      };

      readonly UxmlEnumAttributeDescription<UnityEngine.UIElements.FlexDirection> _flexDirectionAttr = new UxmlEnumAttributeDescription<UnityEngine.UIElements.FlexDirection>
      {
        name = "flexDirection",
        defaultValue = FlexDirection.Column
      };

      public override void Init(VisualElement ve, IUxmlAttributes attrs, CreationContext ctx)
      {
        base.Init(ve, attrs, ctx);
        var thisEl = (ve as Container)!;

        thisEl.JustifyContent = _justifyContentAttr.GetValueFromBag(attrs, ctx);
        thisEl.AlignItems = _alignItemsAttr.GetValueFromBag(attrs, ctx);
        thisEl.FlexDirection = _flexDirectionAttr.GetValueFromBag(attrs, ctx);
      }
    }

    public Container()
    {
      AddToClassList("zero-container");
    }
  }
}

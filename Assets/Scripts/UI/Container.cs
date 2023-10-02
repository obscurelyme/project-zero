using System;
using UnityEngine.UIElements;

namespace Zero
{
  public enum GridSize
  {
    One = 1,
    Two,
    Three,
    Four,
    Five,
    Six,
    Seven,
    Eight,
    Nine,
    Ten,
    Eleven,
    Twelve
  }

  public class Container : VisualElement
  {
    GridSize _priorColumnSize = GridSize.Twelve;
    GridSize _currentColumnSize = GridSize.Twelve;
    GridSize _priorRowSize = GridSize.Twelve;
    GridSize _currentRowSize = GridSize.Twelve;
    UnityEngine.UIElements.Justify _justifyContent;
    UnityEngine.UIElements.Align _alignItems;
    UnityEngine.UIElements.FlexDirection _direction;

    public UnityEngine.UIElements.FlexDirection FlexDirection
    {
      get { return _direction; }
      set
      {
        _direction = value;
        switch (_direction)
        {
          case FlexDirection.Row:
            {
              RemoveFromClassList("zero-container-row-reverse");
              RemoveFromClassList("zero-container-column-reverse");
              AddToClassList("zero-container-row");
              break;
            }
          case FlexDirection.RowReverse:
            {
              RemoveFromClassList("zero-container-row");
              RemoveFromClassList("zero-container-column-reverse");
              AddToClassList("zero-container-row-reverse");
              break;
            }
          case FlexDirection.ColumnReverse:
            {
              RemoveFromClassList("zero-container-row-reverse");
              RemoveFromClassList("zero-container-row");
              AddToClassList("zero-container-column-reverse");
              break;
            }
          default:
          case FlexDirection.Column:
            {
              RemoveFromClassList("zero-container-row");
              RemoveFromClassList("zero-container-row-reverse");
              RemoveFromClassList("zero-container-column-reverse");
              break;
            }
        }
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
    public GridSize ColumnSize
    {
      get { return _currentColumnSize; }
      set
      {
        _priorColumnSize = _currentColumnSize;
        _currentColumnSize = value;
        RemoveFromClassList($"col-{Convert.ToInt32(_priorColumnSize)}");
        AddToClassList($"col-{Convert.ToInt32(_currentColumnSize)}");
      }
    }
    public GridSize RowSize
    {
      get { return _currentRowSize; }
      set
      {
        _priorRowSize = _currentRowSize;
        _currentRowSize = value;
        RemoveFromClassList($"row-{Convert.ToInt32(_priorRowSize)}");
        AddToClassList($"row-{Convert.ToInt32(_currentRowSize)}");
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
        name = "justify-content",
        defaultValue = Justify.FlexStart
      };

      readonly UxmlEnumAttributeDescription<UnityEngine.UIElements.Align> _alignItemsAttr = new UxmlEnumAttributeDescription<UnityEngine.UIElements.Align>
      {
        name = "align-items",
        defaultValue = Align.Stretch
      };

      readonly UxmlEnumAttributeDescription<UnityEngine.UIElements.FlexDirection> _flexDirectionAttr = new UxmlEnumAttributeDescription<UnityEngine.UIElements.FlexDirection>
      {
        name = "flex-direction",
        defaultValue = FlexDirection.Column
      };
      readonly UxmlEnumAttributeDescription<GridSize> _columnSizeAttr = new UxmlEnumAttributeDescription<GridSize>
      {
        name = "column-size",
        defaultValue = GridSize.Twelve
      };
      readonly UxmlEnumAttributeDescription<GridSize> _rowSizeAttr = new UxmlEnumAttributeDescription<GridSize>
      {
        name = "row-size",
        defaultValue = GridSize.Twelve
      };

      public override void Init(VisualElement ve, IUxmlAttributes attrs, CreationContext ctx)
      {
        base.Init(ve, attrs, ctx);
        var thisEl = (ve as Container)!;

        thisEl.JustifyContent = _justifyContentAttr.GetValueFromBag(attrs, ctx);
        thisEl.AlignItems = _alignItemsAttr.GetValueFromBag(attrs, ctx);
        thisEl.FlexDirection = _flexDirectionAttr.GetValueFromBag(attrs, ctx);
        thisEl.ColumnSize = _columnSizeAttr.GetValueFromBag(attrs, ctx);
        thisEl.RowSize = _rowSizeAttr.GetValueFromBag(attrs, ctx);
      }
    }

    public Container()
    {
      AddToClassList("zero-container");
    }
  }
}

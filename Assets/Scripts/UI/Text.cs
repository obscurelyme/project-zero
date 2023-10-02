#nullable enable
using Unity.VisualScripting;
using UnityEngine.UIElements;

namespace Zero
{
  public enum TextType
  {
    Heading1,
    Heading2,
    Heading3,
    Body,
    Disclaimer
  }

  public class Text : TextElement
  {
    private static string[] textClassLists = {
      "zero-text-heading-1",
      "zero-text-heading-2",
      "zero-text-heading-3",
      "zero-text-body",
      "zero-text-disclaimer"
    };

    private TextType _type;

    public TextType Type
    {
      get { return _type; }
      set
      {
        _type = value;
        switch (_type)
        {
          case TextType.Heading1:
            {
              AddTextTypeClass("zero-text-heading-1");
              break;
            }
          case TextType.Heading2:
            {
              AddTextTypeClass("zero-text-heading-2");
              break;
            }
          case TextType.Heading3:
            {
              AddTextTypeClass("zero-text-heading-3");
              break;
            }
          case TextType.Disclaimer:
            {
              AddTextTypeClass("zero-text-disclaimer");
              return;
            }
          case TextType.Body:
          default:
            {
              AddTextTypeClass("zero-text-body");
              return;
            }
        }
      }
    }

    public new class UxmlFactory : UxmlFactory<Text, UxmlTraits>
    {
    }

    public new class UxmlTraits : TextElement.UxmlTraits
    {
      readonly UxmlEnumAttributeDescription<TextType> _typeAttr = new UxmlEnumAttributeDescription<TextType>
      {
        name = "type",
        defaultValue = TextType.Body
      };

      public override void Init(VisualElement ve, IUxmlAttributes attrs, CreationContext ctx)
      {
        base.Init(ve, attrs, ctx);
        var thisTextEl = (ve as Text)!;

        thisTextEl.Type = _typeAttr.GetValueFromBag(attrs, ctx);
      }
    }

    public new static readonly string ussClassName = "zero-text";

    public Text()
    : this(string.Empty)
    {
    }

    public Text(string text)
    {
      AddToClassList(ussClassName);
      this.text = text;
    }

    private void AddTextTypeClass(string className)
    {
      foreach (string cls in textClassLists)
      {
        if (cls == className)
        {
          AddToClassList(cls);
        }
        else
        {
          RemoveFromClassList(cls);
        }
      }
    }
  }
}


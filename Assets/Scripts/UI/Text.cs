#nullable enable
using UnityEngine.UIElements;


namespace Zero
{
  public enum TextType
  {
    Heading1,
    Heading2,
    Heading3,
    Body
  }

  public class Text : TextElement
  {
    private TextType _type;

    public TextType Type
    {
      get { return _type; }
      set
      {
        _type = value;
        if (_type == TextType.Heading1)
        {
          RemoveFromClassList("zero-text-body");
          AddToClassList("zero-text-heading");
          return;
        }
        RemoveFromClassList("zero-text-heading");
        AddToClassList("zero-text-body");
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
  }
}


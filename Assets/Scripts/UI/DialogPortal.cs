using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Zero
{
  public class DialogPortal : VisualElement
  {
    public new class UxmlFactory : UxmlFactory<DialogPortal> { }

    public List<Dialog> dialogs => this.Query<Dialog>().ToList();

    public DialogPortal()
    {
      LoadUXMLAsset();
      this.AddToClassList("dialog-portal");
      this.AddToClassList("dialog-portal-hidden");

      this.RegisterCallback<AttachToPanelEvent>(evt =>
      {
        Dialog.OnDialogOpen += DisplayPortal;
        Dialog.OnDialogClose += HidePortal;
      });

      this.RegisterCallback<DetachFromPanelEvent>(evt =>
      {
        Dialog.OnDialogOpen -= DisplayPortal;
        Dialog.OnDialogClose -= HidePortal;
      });
    }

    private void LoadUXMLAsset()
    {
      var asset = Resources.Load<VisualTreeAsset>("UI/DialogPortal");
      asset.CloneTree(this);
    }

    private void DisplayPortal()
    {
      this.RemoveFromClassList("dialog-portal-hidden");
    }

    private void HidePortal()
    {
      this.AddToClassList("dialog-portal-hidden");
    }
  }
}


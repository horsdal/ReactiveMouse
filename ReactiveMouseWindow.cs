namespace ReactiveMouse
{
  using System;
  using System.Reactive.Linq;
  using System.Windows.Forms;

  public partial class ReactiveMouseWindow : Form
  {
    public ReactiveMouseWindow()
    {
      InitializeComponent();
    }

    private void OnLoad(object sender, EventArgs e)
    {
      var mouseMoves = Observable.FromEventPattern<MouseEventArgs>(this, "MouseMove");
      mouseMoves.Subscribe(ev => WriteCoordinatesToScreen(ev.EventArgs));
    }

    private void WriteCoordinatesToScreen(MouseEventArgs eventArgs)
    {
      coordinatesBox.Text = string.Format("(X: {0}, Y: {1})", eventArgs.X, eventArgs.Y);
    }
  }
}

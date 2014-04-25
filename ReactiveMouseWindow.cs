namespace ReactiveMouse
{
  using System;
  using System.Drawing;
  using System.Net;
  using System.Reactive;
  using System.Reactive.Linq;
  using System.Windows.Forms;

  public partial class ReactiveMouseWindow : Form
  {
    private IObservable<EventPattern<MouseEventArgs>> mouseMoves;
    private IObservable<Color> colorByInOrOutOfSquare;
    private const string MsftQuoteUrl = "http://www.nasdaq.com/quotedll/quote.dll?page=dynamic&mode=data&&selected=MSFT";

    public ReactiveMouseWindow()
    {
      InitializeComponent();
    }

    private void OnLoad(object sender, EventArgs e)
    {
      SetTextboxTextByCoordinates();
      SetBackgroundColorByMousePosition();
      SetTextColorByMouseAndKeyPressed();
      SetTitleByMsftQoute();
    }
    private void SetTextboxTextByCoordinates()
    {
      mouseMoves = Observable.FromEventPattern<MouseEventArgs>(this, "MouseMove");
      mouseMoves.Subscribe(ev => WriteCoordinatesToScreen(ev.EventArgs));
    }

    private void WriteCoordinatesToScreen(MouseEventArgs eventArgs)
    {
      coordinatesBox.Text = string.Format("(X: {0}, Y: {1})", eventArgs.X, eventArgs.Y);
    }

    private void SetBackgroundColorByMousePosition()
    {
      colorByInOrOutOfSquare = CreateColorByInOrOutOfSquare();
      colorByInOrOutOfSquare.Subscribe(color => this.BackColor = color);
    }

    private IObservable<Color> CreateColorByInOrOutOfSquare()
    {
      return mouseMoves
        .Select(ev => IsInSquare(ev.EventArgs)).DistinctUntilChanged()
        .Select(ev => ev ? Color.OrangeRed : Color.Blue);
    }

    private bool IsInSquare(MouseEventArgs eventArgs)
    {
      return eventArgs.X < 100 && eventArgs.Y < 100;
    }

    private void SetTextColorByMouseAndKeyPressed()
    {
      var textColorByKeyPressedChanged = colorByInOrOutOfSquare.CombineLatest(CreateIsSpacePressedChanged(), (color, isSpace) => isSpace ? color : Color.Black);
      textColorByKeyPressedChanged.Subscribe(color => coordinatesBox.ForeColor = color);
    }


    private IObservable<bool> CreateIsSpacePressedChanged()
    {
      return Observable.FromEventPattern<KeyPressEventArgs>(coordinatesBox, "KeyPress")
        .Select(args => args.EventArgs.KeyChar == ' ')
        .DistinctUntilChanged();
    }

    private void SetTitleByMsftQoute()
    {
      var callNasdaq = Observable
          .Defer(() => Observable.StartAsync(() => new WebClient().DownloadStringTaskAsync(new Uri(MsftQuoteUrl))))
          .Select(body => body.Split('|')[1]);
      callNasdaq.Subscribe(quote => OnNextMsftQuote(quote, callNasdaq));
    }

    private void OnNextMsftQuote(string quote, IObservable<string> next)
    {
      Invoke(new Action(() => this.Text = quote));
      next.DelaySubscription(TimeSpan.FromSeconds(30)).Subscribe(nextQuote => OnNextMsftQuote(nextQuote, next));
    }
  }
}

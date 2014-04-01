namespace ReactiveMouse
{
  partial class ReactiveMouseWindow
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.coordinatesBox = new System.Windows.Forms.TextBox();
      this.SuspendLayout();
      // 
      // textBox1
      // 
      this.coordinatesBox.Location = new System.Drawing.Point(504, 589);
      this.coordinatesBox.Name = "textBox1";
      this.coordinatesBox.Size = new System.Drawing.Size(253, 20);
      this.coordinatesBox.TabIndex = 0;
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(797, 621);
      this.Controls.Add(this.coordinatesBox);
      this.Name = "Form1";
      this.Text = "Form1";
      this.Load += new System.EventHandler(this.OnLoad);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox coordinatesBox;
  }
}


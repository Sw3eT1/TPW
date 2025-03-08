using System;
using System.Windows.Forms;
using Utils;

public class MyForm : Form
{
    private Button openWindowButton;
    private WindowOpener windowOpener;

    public MyForm()
    {
        this.Text = "Główne Okno";
        this.Size = new System.Drawing.Size(400, 300);
        
        windowOpener = new WindowOpener();

        openWindowButton = new Button
        {
            Text = "Rozpocznij symulacje kulek !",
            Location = new System.Drawing.Point(150, 100),
            Size = new System.Drawing.Size(100, 30)
        };

        openWindowButton.Click += OpenNewWindow;
        this.Controls.Add(openWindowButton);
    }

    private void OpenNewWindow(object sender, EventArgs e)
    {
        windowOpener.OpenNewWindow(300, 200);
    }
}

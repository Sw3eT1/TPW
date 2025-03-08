using System;
using System.Windows.Forms;
using Utils; // Importujemy naszą klasę

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
            Text = "Otwórz okno",
            Location = new System.Drawing.Point(150, 100),
            Size = new System.Drawing.Size(100, 30)
        };

#pragma warning disable CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).
        openWindowButton.Click += OpenNewWindow;
#pragma warning restore CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).
        this.Controls.Add(openWindowButton);
    }

    private void OpenNewWindow(object sender, EventArgs e)
    {
        windowOpener.OpenNewWindow(300, 200); // Otwiera nowe okno 300x200
    }
}

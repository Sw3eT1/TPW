using System;
using System.Windows.Forms;

namespace Utils
{
    public class WindowOpener
    {
        public void OpenNewWindow(int width, int height)
        {
            Form newForm = new Form
            {
                Text = "Nowe Okno",
                Size = new System.Drawing.Size(width, height)
            };

            newForm.Show();
        }
    }
}

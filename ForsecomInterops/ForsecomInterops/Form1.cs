using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace ForsecomInterops
{
    public partial class Form1 : Form
    {
        private static Form1 instance;

        private Form1()
        {
            InitializeComponent();
        }

        public static Form1 Instance
        {
            get
            {
                if (instance == null || instance.IsDisposed)
                    instance = new Form1();
                return instance;
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            // Only hide the form if the user is trying to close it (or it's being programmatically closed).
            // If the system is shutting down or its parent form is closing, we do want it to close properly.
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true; // cancel the formclose event
                Hide(); // hide the form
                return;
            }

            // Else, there's something else going on and we should truly close the form
        }

        public static DateTime lastActive;
        private ObjectForScripting objectForScripting;

        /// <summary>
        /// Load form event handler. Based on sample code from https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.webbrowser.objectforscripting?view=netcore-3.1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            objectForScripting = new ObjectForScripting(webBrowser1);
            webBrowser1.AllowWebBrowserDrop = false;
            webBrowser1.IsWebBrowserContextMenuEnabled = true;
            webBrowser1.WebBrowserShortcutsEnabled = false;
            webBrowser1.ObjectForScripting = this.objectForScripting;
            //webBrowser1.ScriptErrorsSuppressed = true;

            // Only necessary because we can't .Navigate() to a static html file
            webBrowser1.DocumentText = File.ReadAllText("Webpage.html");
        }

        /// <summary>
        /// This is an example of the C#-side pinging functions within the webpage. Here, clicking on a WinForms button outside of the webpage
        /// calls the "test()" function defined within the webpage, and causes an alert to pop up.
        /// This could be adapted to have TOCS ask for Forsecom's current login status.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            Debug.WriteLine(webBrowser1.Document.InvokeScript("test",
            new String[] { "This is text in C# that has been sent to the web browser and is now being shown with a JS alert!" }));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            lastActive = DateTime.Now;
            label2.Text = lastActive.ToString();
            this.objectForScripting.lastActive = lastActive;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label3.Text = "Status: " + (String)webBrowser1.Document.InvokeScript("getLoginStatus");

        }
    }
}

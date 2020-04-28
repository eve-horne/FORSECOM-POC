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
        public Form1()
        {
            InitializeComponent();
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

            // TODO: Mark wanted us to test this spike with Vue.JS if possible. That means we should probably set up a separate html file to serve as the DocumentText
            // so that we can try integrating Vuex into all of this.
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
            // TODO: check if InvokeScript will pass back return values of JS functions to the C#-side code?
            // If this can't pass back return values, then we'll have to be clever about how the webpage would tell
            // TOCS the current login status. Perhaps it would then immediately do a "window.external.SomeC#Function(The necessary information)"
            // to initiate a separate call back from the webpage to the C#-side code.
        }

        private void button2_Click(object sender, EventArgs e)
        {
            lastActive = DateTime.Now;
            label2.Text = lastActive.ToString();
            this.objectForScripting.lastActive = lastActive;
        }

    }
}

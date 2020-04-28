using System;
using System.Windows.Forms;

namespace ForsecomInterops
{
    /// <summary>
    /// Singleton object that will be set as Form1's Webbrowser.Objectforscripting.
    /// When we actually use this code as reference, this will probably be called something like "ForseComInterop.cs"
    /// </summary>
    [System.Runtime.InteropServices.ComVisible(true)] // This attribute is necessary so that Form1's Webbrowser can "see" this object
    public class SingletonObjectForScripting
    {
        /// <summary>
        /// Private constructor so nobody can call it but the class itself
        /// </summary>
        private SingletonObjectForScripting() {}

        /// <summary>
        /// The only instance of this class we care about: the all important singleton
        /// </summary>
        private static SingletonObjectForScripting m_instance;

        /// <summary>
        /// Public-facing property that exposes the m_instance field. Should probably only be used by Form1 to set the Webbrowser.Objectforscripting.
        /// </summary>
        public static SingletonObjectForScripting Instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = new SingletonObjectForScripting();
                }
                return m_instance;
            }
        }

        /// <summary>
        /// This is an example of the webpage pinging functions within C#-side. Here, clicking on a JS button inside of the webpage
        /// calls the SendInfoToWindows() function defined below, and causes a Windows alert to pop up.
        /// This could be adapted to have the Forsecom SPA periodically ask TOCS for the last recorded datetime of user movement.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SendInfoToWindows(String message)
        {
            MessageBox.Show("This is a Windows alert! The webpage sent me this: " + message, "Windows alert!");
            // Here, the same question applies as asked in Form1.cs - do return values of this function get sent back
            // to the JS? Else, we'll probably need to do webBrowser1.Document.InvokeScript(the necessary information) inside of this function
            // before returning.
        }
        public void requestLastActiveTime()
        {

        }
    }
}

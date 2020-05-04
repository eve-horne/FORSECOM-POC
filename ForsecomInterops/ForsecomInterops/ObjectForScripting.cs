using System;
using System.Windows.Forms;

namespace ForsecomInterops
{
    /// <summary>
    /// Singleton object that will be set as Form1's Webbrowser.Objectforscripting.
    /// When we actually use this code as reference, this will probably be called something like "ForseComInterop.cs"
    /// (Lives where we put the windows form)
    /// </summary>
    [System.Runtime.InteropServices.ComVisible(true)] // This attribute is necessary so that Form1's Webbrowser can "see" this object
    public class ObjectForScripting // needs access to FormBase somehow
    {

        public ObjectForScripting() {}
        internal string lastActiveDate; // this would be in FormBase as a static property

        // This property can be referenced by both the JS-world and the C#-world.
        // There's a parallel property kept in Vue's Vuex store, which is updated every minute against this property.
        public string UserLastActiveDateTime
        {
            get {
                return lastActiveDate;
            }
        }

        /// <summary>
        /// This is an example of the webpage pinging functions within C#-side. Here, clicking on a JS button inside of the webpage
        /// calls the SendInfoToWindows() function defined below, and causes a Windows alert to pop up.
        /// This could be adapted to have the Forsecom SPA periodically ask TOCS for the last recorded datetime of user movement.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SendInfoToWindows(string message)
        {
            MessageBox.Show("This is a Windows alert! The webpage sent me this: " + message, "Windows alert!");
        }

        /// <summary>
        /// Simple logging function that JS can call to ping C#
        /// </summary>
        /// <param name="message"></param>
        public void WinLog(string message)
        {
            Console.WriteLine(message); //#if debug - applicationlogger.log (in actual TOCS project)
        }
    }
}

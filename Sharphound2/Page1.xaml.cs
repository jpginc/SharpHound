using System.Windows;
using System.Windows.Controls;
using System.Runtime.InteropServices;
using System;
using System.IO;
using System.Windows.Threading;

namespace Sharphound2
{
    public partial class Page1 : Page
    {
        StringWriter writer;
        DispatcherTimer timer;


        public Page1()
        {
            InitializeComponent();
            @out.Text = @"Click the 'Run Bloodhound' button on the bottom left. 
You can run it with standard command line arguments, add the args to the edit box on the left.
You can get the help text by adding the argument --help
Once Bloodhound starts, this window will freeze until it finishes (which can take ages).";
            writer = new StringWriter();
            Console.SetOut(writer);
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 2);
            timer.Tick += new EventHandler(Timer_Tick);
            timer.Start();
        }

        void Timer_Tick(object sender, EventArgs e)
        {
            writer.Flush();
            var txt = writer.GetStringBuilder().ToString();
            writer.GetStringBuilder().Clear();
            @out.Text += txt;
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string command = @in.Text;
            if(command.Contains("--help"))
            {
                var o = new Sharphound2.Sharphound.Options();
                @out.Text += o.GetUsage();
                return;
            }
            @out.Text += "Starting Sharphound with command: " + command + " \r\n";
            Sharphound2.Sharphound.InvokeBloodHound(CommandLineToArgs(command));
        }

        //copy paste from https://stackoverflow.com/questions/298830/split-string-containing-command-line-parameters-into-string-in-c-sharp
        [DllImport("shell32.dll", SetLastError = true)]
        static extern IntPtr CommandLineToArgvW(
        [MarshalAs(UnmanagedType.LPWStr)] string lpCmdLine, out int pNumArgs);
        private string[] CommandLineToArgs(string commandLine)
        {
            int argc;
            var argv = CommandLineToArgvW(commandLine, out argc);
            if (argv == IntPtr.Zero)
                throw new System.ComponentModel.Win32Exception();
            try
            {
                var args = new string[argc];
                for (var i = 0; i < args.Length; i++)
                {
                    var p = Marshal.ReadIntPtr(argv, i * IntPtr.Size);
                    args[i] = Marshal.PtrToStringUni(p);
                }

                return args;
            }
            finally
            {
                Marshal.FreeHGlobal(argv);
            }
            
        }
    }
}

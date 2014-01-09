using System;

namespace Runner
{
    public static class RunnerApp
    {
        [STAThread]
        public static void Main()
        {
            var window = new MainWindow();
            window.ShowDialog();
        }
    }
}
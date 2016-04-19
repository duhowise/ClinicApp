using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Documents;

namespace ClinicApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
           this.InitializeComponent();

            Thread.Sleep(1000);
        }
    }

    internal class LineAdorner
    {
        public LineAdorner()
        {
        }
    }
}

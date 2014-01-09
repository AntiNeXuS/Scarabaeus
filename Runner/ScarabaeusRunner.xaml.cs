using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.IO.Ports;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Runner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SerialPort _port;

        public MainWindow()
        {
            InitializeComponent();

            try
            {
                _port = new SerialPort("COM5", 9600);
                _port.Open();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            this.Closing += (sender, args) =>
            {
                if (_port.IsOpen)
                {
                    _port.Close();
                }
            };
        }

        private void Leg0_Update()
        {
            if (Coha0 == null || Femur0 == null || Tibia0 == null || _port == null)
            {
                return;
            }

            var data = string.Format("0, {0}, {1}, {2}", Coha0.Value, Femur0.Value, Tibia0.Value);
            if (_port.IsOpen && _port.BytesToWrite == 0)
            {
                _port.WriteLine(data);
            }
        }

        private void Leg1_Update()
        {
            if (Coha1 == null || Femur1 == null || Tibia1 == null || _port == null)
            {
                return;
            }

            var data = string.Format("1, {0}, {1}, {2}", Coha1.Value, Femur1.Value, Tibia1.Value);
            if (_port.IsOpen && _port.BytesToWrite == 0)
            {
                _port.WriteLine(data);
            }
        }

        private void Leg2_Update()
        {
            if (Coha2 == null || Femur2 == null || Tibia2 == null || _port == null)
            {
                return;
            }

            var data = string.Format("2, {0}, {1}, {2}", Coha2.Value, Femur2.Value, Tibia2.Value);
            if (_port.IsOpen && _port.BytesToWrite == 0)
            {
                _port.WriteLine(data);
            }
        }

        private void Leg3_Update()
        {
            if (Coha3 == null || Femur3 == null || Tibia3 == null || _port == null)
            {
                return;
            }

            var data = string.Format("3, {0}, {1}, {2}", Coha3.Value, Femur3.Value, Tibia3.Value);
            if (_port.IsOpen && _port.BytesToWrite == 0)
            {
                _port.WriteLine(data);
            }
        }

        private void Leg0_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Leg0_Update();
        }

        private void Leg1_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Leg1_Update();
        }

        private void Leg2_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Leg2_Update();
        }

        private void Leg3_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Leg3_Update();
        }
    }
}

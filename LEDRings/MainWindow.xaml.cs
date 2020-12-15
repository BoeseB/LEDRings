using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LEDRings
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly LedRingService _ledRingService;

        private double profibility;
        private double availableAmount;



        public MainWindow()
        {
            InitializeComponent();
            var mqttClient = new MqttClient();
            _ledRingService = new LedRingService(mqttClient, 8, LedRingDirection.Clockwise);
        }

        private void ProfibilitySlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.profibility = e.NewValue;
            _ledRingService.SetRing(profibility);
        }

        private void AvailableAmountSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.availableAmount = e.NewValue;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Persist(profibility, availableAmount);
        }

        private void Persist(double profibility, double availableAmount)
        {
            MessageBox.Show($"Persisting profibility: {profibility}, availableAmount: {availableAmount}");
        }
    }
}

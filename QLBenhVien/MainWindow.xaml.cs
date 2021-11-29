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

namespace QLBenhVien
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //BHYT
            MainContent.Children.Clear();
            Views.BHYTView usercontrol = new Views.BHYTView();
            MainContent.Children.Add(usercontrol);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //Sick
            MainContent.Children.Clear();
            Views.SickView usercontrol = new Views.SickView();
            MainContent.Children.Add(usercontrol);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //Bill
            MainContent.Children.Clear();
            Views.BillView usercontrol = new Views.BillView();
            MainContent.Children.Add(usercontrol);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            //Patient
            MainContent.Children.Clear();
            Views.PatientView usercontrol = new Views.PatientView();
            MainContent.Children.Add(usercontrol);
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            //MedicalRecord
            MainContent.Children.Clear();
            Views.MedicalRecordView usercontrol = new Views.MedicalRecordView();
            MainContent.Children.Add(usercontrol);
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            //Medicine
            MainContent.Children.Clear();
            Views.MedicineView usercontrol = new Views.MedicineView();
            MainContent.Children.Add(usercontrol);
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            //Location
            MainContent.Children.Clear();
            Views.LocationView usercontrol = new Views.LocationView();
            MainContent.Children.Add(usercontrol);
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            //Prescription
            MainContent.Children.Clear();
            Views.PrescriptionView usercontrol = new Views.PrescriptionView();
            MainContent.Children.Add(usercontrol);
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            //Print
            MainContent.Children.Clear();
            Views.PrintView usercontrol = new Views.PrintView();
            MainContent.Children.Add(usercontrol);
        }

        private void Button_GotFocus(object sender, RoutedEventArgs e)
        {
            
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            
        }
    }
}

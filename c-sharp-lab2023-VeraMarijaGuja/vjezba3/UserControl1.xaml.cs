﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace vjezba3
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {

        static string file = "patients.json";
        class Patient
        {
            public string Oib { get; set; }
            public string Mbo { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public DateTime Dob { get; set; }
            public string Gender { get; set; }
            public string MedicalDiagnosis { get; set; }
            public bool Status { get; set; }

            public void PrintPatient()
            {
                Console.WriteLine("OIB: " + Oib);
                Console.WriteLine("MBO: " + Mbo);
                Console.WriteLine("First name and Last name: " + FirstName + " " + LastName);
                Console.WriteLine("Date of birth: " + Dob.ToString("dd.MM.yyyy."));
                Console.WriteLine("Gender: " + Gender);
                Console.WriteLine("Medical diagnosis: " + MedicalDiagnosis);
                Console.WriteLine("Status: " + Status);
            }
        }


        public UserControl1()
        {
            InitializeComponent();

            if (File.Exists(file))
            {
                string json = File.ReadAllText(file);
                List<Patient> patients = JsonConvert.DeserializeObject<List<Patient>>(json);
                foreach (Patient p in patients)
                {
                    patientsList.Items.Add(p);
                }
            }
        }

        private void ButtonAddPatient(object sender, RoutedEventArgs e)
        {
            string oib = txtOib.Text.Trim();
            string mbo = txtMbo.Text.Trim();
            string firstName = txtFirstName.Text.Trim();
            string lastName = txtLastName.Text.Trim();
            DateTime DateOfBirth = (dpDateOfBirth.SelectedDate ?? DateTime.Now).Date;
            string gender = (cbGender.SelectedItem as ComboBoxItem)?.Content.ToString();
            string diagnosis = txtDiagnosis.Text.Trim();

            if (string.IsNullOrEmpty(oib) || string.IsNullOrEmpty(mbo) || string.IsNullOrEmpty(firstName) ||
                string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(gender) || string.IsNullOrEmpty(diagnosis))
            {
                MessageBox.Show("Not all data is given!");
                return;
            }
            else if (oib.Length != 11)
            {
                MessageBox.Show("OIB must have 11 characters");
                return;
            }

            else if (mbo.Length != 8)
            {
                MessageBox.Show("MBO must have 8 characters.");
                return;
            }


            Patient patient = new Patient
            {
                Oib = oib,
                Mbo = mbo,
                FirstName = firstName,
                LastName = lastName,
                Dob = DateOfBirth,
                Gender = gender,
                MedicalDiagnosis = diagnosis,
                Status = true
            };

            patientsList.Items.Add(patient);
            string json = JsonConvert.SerializeObject(patientsList.Items.Cast<Patient>().ToList());
            File.WriteAllText(file, json);
        }

        private void MenuItem1(object sender, RoutedEventArgs e)
        {
            // Show stackPanel1, hide stackPanel2
            stackPanel1.Visibility = Visibility.Visible;
            stackPanel2.Visibility = Visibility.Collapsed;
            stackPanel4.Visibility = Visibility.Collapsed;
        }

        private void MenuItem2(object sender, RoutedEventArgs e)
        {
            // Show stackPanel2, hide stackPanel1
            stackPanel1.Visibility = Visibility.Collapsed;
            stackPanel2.Visibility = Visibility.Collapsed;
            stackPanel4.Visibility = Visibility.Visible;
        }

        private void MenuItem3(object sender, RoutedEventArgs e)
        {
            // Show stackPanel2, hide stackPanel1
            stackPanel1.Visibility = Visibility.Collapsed;
            stackPanel4.Visibility = Visibility.Collapsed;
            stackPanel2.Visibility = Visibility.Visible;
        }



        private void ButtonEditPatientSearch(object sender, RoutedEventArgs e)
        {
            string searchOib = txtsearchOib.Text.Trim();
            Patient editPatient = patientsList.Items.OfType<Patient>().FirstOrDefault(p => p.Oib == searchOib);
            if (editPatient != null)
            {
                txtEditOib.Text = editPatient.Oib;
                txtEditMbo.Text = editPatient.Mbo;
                txtEditFirstName.Text = editPatient.FirstName;
                dpDateOfBirth.SelectedDate = editPatient.Dob;
                cbEditGender.SelectedIndex = editPatient.Gender == "Female" ? 0 : 1;
                txtEditDiagnosis.Text = editPatient.MedicalDiagnosis;
                stackPanel3.Visibility = stackPanel3.Visibility == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        private void ButtonDeletePatient(object sender, RoutedEventArgs e)
        {
            string oibToDelete = txtDeleteOib.Text.Trim();
            Patient deletePatient = patientsList.Items.OfType<Patient>().FirstOrDefault(p => p.Oib == oibToDelete);

            if (deletePatient != null)
            {
                patientsList.Items.Remove(deletePatient);
                string json = JsonConvert.SerializeObject(patientsList.Items.Cast<Patient>().ToList());
                File.WriteAllText(file, json);
            }
            else
            {
                MessageBox.Show("Patient with OIB " + oibToDelete + " doesnt exists.");
            }
        }



    }
}

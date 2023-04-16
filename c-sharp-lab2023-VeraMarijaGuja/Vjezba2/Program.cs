using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

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

class Program
{
    static List<Patient> patients = new List<Patient>();
    static string file = "patients.json";

    static void Main()
    {
        string action;
        do
        {
            Console.WriteLine("What do you want to do? ");
            Console.WriteLine("1. Receive the patient");
            Console.WriteLine("2. Discharge the patient ");
            Console.WriteLine("3. Update patient data");
            Console.WriteLine("4. Print all patients data");
            Console.WriteLine("0. Leave the program");
            action = Console.ReadLine();

            switch (action)
            {
                case "1":
                    ReceivePatient();
                    break;
                case "2":
                    DischargePatient();
                    break;
                case "3":
                    UpdatePatientDiagnose();
                    break;
                case "4":
                    PrintAllPatientData();
                    break;
                case "0":
                    Console.WriteLine("ProgramFinished!");
                    break;
                default:
                    Console.WriteLine("Wrong action!!!");
                    break;
            }
            SaveData();
        } while (action != "0");
    }

    static void ReceivePatient()
    {
        Patient patient = new Patient();

        Console.Write("Enter patient oib: ");
        patient.Oib = Console.ReadLine();

        while (patient.Oib.Length != 11)
        {
            Console.Write("Enter again. Oib must have 11 characters ");
            patient.Oib = Console.ReadLine();
        }
        Console.Write("Enter MBO: ");
        patient.Mbo = Console.ReadLine();

        while (patient.Mbo.Length != 9)
        {
            Console.Write("Enter again. MBO must have 9 characters!! ");
            patient.Mbo = Console.ReadLine();
        }

        Console.Write("Enter First name: ");
        patient.FirstName = Console.ReadLine();

        Console.Write("Enter Last name: ");
        patient.LastName = Console.ReadLine();

        Console.Write("Enter Date of birth:  (dd.mm.yyyy.): ");
        string dob = Console.ReadLine();
        DateTime DateOfBirth;

        while (!DateTime.TryParseExact(dob, "dd.MM.yyyy.", null, System.Globalization.DateTimeStyles.None, out DateOfBirth))
        {
            Console.Write("Wrong! Enter again (dd.mm.yyyy.): ");
            dob = Console.ReadLine();
        }
        patient.Dob = DateOfBirth;

        Console.Write("Enter gender (M or F): ");
        patient.Gender = Console.ReadLine();

        while (patient.Gender != "M" && patient.Gender != "F")
        {
            Console.Write("Wrong! Enter again (M or F): ");
            patient.Gender = Console.ReadLine();
        }

        Console.Write("Enter diagnose: ");
        patient.MedicalDiagnosis = Console.ReadLine();
        patient.Status = true;
        patients.Add(patient);

        Console.WriteLine("Success!");
    }

    static void DischargePatient()
    {
        Console.Write("Enter patient Oib you want to discharge: ");
        string oib = Console.ReadLine();

        Patient patient = patients.Find(p => p.Oib == oib);

        if (patient == null)
        {
            Console.WriteLine("Patient not found!");
            return;
        }

        patient.Status = false;

        Console.WriteLine("Successfuly discharged patient:");
    }

    static void UpdatePatientDiagnose()
    {
        Console.Write("Enter patient Oib which data you want to change: ");
        string oib = Console.ReadLine();

        Patient patient = patients.Find(p => p.Oib == oib);

        if (patient == null)
        {
            Console.WriteLine("Patient not found ! ");
            return;
        }

        Console.WriteLine("Enter new diagnose:");
        string newDiagnose = Console.ReadLine();
        patient.MedicalDiagnosis = newDiagnose;

        Console.WriteLine("Success! ");
    }

    static void PrintAllPatientData()
    {
        foreach (Patient patient in patients)
        {
            patient.PrintPatient();
            Console.WriteLine();
        }
    }

    static void ReadDataFromFile()
    {
        if (File.Exists(file))
        {
            string json = File.ReadAllText(file);
            patients = JsonSerializer.Deserialize<List<Patient>>(json);
        }
    }

    static void SaveData()
    {
        string json = JsonSerializer.Serialize(patients);
        File.WriteAllText(file, json);
    }
}


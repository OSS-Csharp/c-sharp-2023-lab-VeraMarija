using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using vjezba4.DataAccess;
using System.Data.Entity;
using System.Data;
using System.Windows.Controls;

namespace vjezba4.ViewModel
{
    public class PatientViewModel
    {
        private ICommand _saveCommand;
        private ICommand _resetCommand;
        private ICommand _editCommand;
        private ICommand _deleteCommand;
        private ICommand _activeCommand;
        private ICommand _discargedCommand;
        private ICommand _allCommand;
        private PatientRepository _repository;
        private Patient _patientEntity = null;
        public Patient PatientRecord { get; set; }
        public PatientEntities PatientEntities { get; set; }

        public ICommand AllCommand
        {
            get
            {
                if (_allCommand == null)
                    _allCommand = new RelayCommand(param => GetAll(), null);

                return _allCommand;
            }
        }
        public ICommand ResetCommand
        {
            get
            {
                if (_resetCommand == null)
                    _resetCommand = new RelayCommand(param => ResetData(), null);

                return _resetCommand;
            }
        }

        public ICommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                    _saveCommand = new RelayCommand(param => SaveData(), null);

                return _saveCommand;
            }
        }

        public ICommand EditCommand
        {
            get
            {
                if (_editCommand == null)
                    _editCommand = new RelayCommand(param => EditData((string)param), null);

                return _editCommand;
            }
        }

        public ICommand DeleteCommand
        {
            get
            {
                if (_deleteCommand == null)
                    _deleteCommand = new RelayCommand(param => DeletePatient((string)param), null);

                return _deleteCommand;
            }
        }

        public ICommand ActiveCommand
        {
            get
            {
                if (_activeCommand == null)
                    _activeCommand = new RelayCommand(param => Active(), null);

                return _activeCommand;
            }
        }

        public ICommand DiscargedCommand
        {
            get
            {
                if (_discargedCommand == null)
                    _discargedCommand = new RelayCommand(param => Discarged(), null);

                return _discargedCommand;
            }
        }
        public PatientViewModel()
        {
            _patientEntity = new Patient();
            _repository = new PatientRepository();
            PatientRecord = new Patient();
            GetAll();
        }

        public void Active()
        {  
            PatientRecord.PatientRecords = new ObservableCollection<Patient>(_repository.GetByStatus("active"));
        }

        public void Discarged()
        {
            PatientRecord.PatientRecords = new ObservableCollection<Patient>(_repository.GetByStatus("discarged"));
        }
        public void ResetData()
        {
            PatientRecord.Oib = string.Empty;
            PatientRecord.Mbo = string.Empty;
            PatientRecord.FirstName = string.Empty;
            PatientRecord.LastName = string.Empty;
            PatientRecord.Dob = DateTime.Now;
            PatientRecord.Gender = string.Empty;
            PatientRecord.MedicalDiagnosis = string.Empty;
            PatientRecord.Insurance = string.Empty;
        }

        public void DeletePatient(string oib)
        {
            if (MessageBox.Show("Confirm delete of this record?", "Patient", MessageBoxButton.YesNo)
                == MessageBoxResult.Yes)
            {
                try
                {
                    _repository.DischargePatient(oib);
                    MessageBox.Show("Record successfully deleted.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error occured while saving. " + ex.InnerException);
                }
                finally
                {
                    GetAll();
                }
            }
        }

        public void SaveData()
        {
            if (PatientRecord != null)
            {
                _patientEntity.Oib = PatientRecord.Oib;
                _patientEntity.Mbo = PatientRecord.Mbo;
                _patientEntity.FirstName = PatientRecord.FirstName;
                _patientEntity.LastName = PatientRecord.LastName;
                _patientEntity.Dob = PatientRecord.Dob;
                _patientEntity.Gender = PatientRecord.Gender;
                _patientEntity.MedicalDiagnosis = PatientRecord.MedicalDiagnosis;
                _patientEntity.Status = "active";
                _patientEntity.Insurance = PatientRecord.Insurance;

                try
                {
                    if (_repository.Get(PatientRecord.Oib) == null)
                    {
                        _repository.AddPatient(_patientEntity);
                        MessageBox.Show("New record successfully saved.");
 
                    }
                    else
                    {
                        _patientEntity.Oib = PatientRecord.Oib;
                        _repository.UpdatePatient(_patientEntity);
                        MessageBox.Show("Record successfully updated.");

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error occured while saving. " + ex.InnerException);
                }
                finally
                {
                    GetAll();
                    ResetData();
                }
            }
        }

        public void EditData(string oib)
        {
            var model = _repository.Get(oib);
            PatientRecord.Oib = model.Oib;
            PatientRecord.Mbo = model.Mbo;
            PatientRecord.FirstName = model.FirstName;
            PatientRecord.LastName = model.LastName;
            PatientRecord.Dob = model.Dob;
            PatientRecord.Gender = model.Gender;
            PatientRecord.MedicalDiagnosis = model.MedicalDiagnosis;
            PatientRecord.Status = "discharged";
            PatientRecord.Insurance = model.Insurance;
        }

        public void GetAll()
        {
            PatientRecord.PatientRecords = new ObservableCollection<Patient>();
            _repository.GetAll().ForEach(data => PatientRecord.PatientRecords.Add(new Patient()
            {
                Oib = data.Oib,
                Mbo = data.Mbo,
                FirstName = data.FirstName,
                LastName = data.LastName,
                Dob = data.Dob,
                Gender = data.Gender,
                MedicalDiagnosis = data.MedicalDiagnosis,
                Status = data.Status,
                Insurance = data.Insurance
            }));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using ProjectMVVM.Model;

namespace ProjectMVVM.ViewModel
{
    class MedeWerkersVM : ObserableObject, IPage
    {

        public MedeWerkersVM()
        {

            _ContactpersonTypes = ContactpersonType.getContactpersonType();
            _Contactpersons = Contactperson.getContactperson();
        }

        private ObservableCollection<ContactpersonType> _ContactpersonTypes;
        public ObservableCollection<ContactpersonType> ContactpersonTypes
        {
            get
            {
                return _ContactpersonTypes;
            }

            set
            {
                _ContactpersonTypes = value;
                OnPropertyChanged("ContactpersonsType");
            }
        }

        private ContactpersonType _ContactpersonTypesSelected;
        public ContactpersonType SelectedContactpersonType
        {
            get
            {
                return _ContactpersonTypesSelected;
            }
            set
            {

                _ContactpersonTypesSelected = value;
                OnPropertyChanged("SelectedContactpersonstype");
            }

        }

        //property toevoegen waaraan we de list box uit de usercontrol homepage aan zullen binden.

        private Contactperson _ContactpersonsSelected;
        public Contactperson SelectedContactperson
        {
            get
            {

                return _ContactpersonsSelected;
            }
            set
            {

                _ContactpersonsSelected = value;
                OnPropertyChanged("SelectedContactperson");
            }

        }

        public ICommand AddNewContactpersonsCommand
        {
            get
            {

                return new RelayCommand(AddNewContactpersons);

            }

        }

        private void AddNewContactpersons()
        {
            Contactperson anieuw = new Contactperson();

            SelectedContactperson = anieuw;
            Contactpersons.Add(anieuw);
        }

        public ICommand SaveContactpersonsCommand
        {
            get
            {

                return new RelayCommand(SaveContactpersons);

            }

        }
        private void DeleteContactpersons()
        {

            Contactperson.DeleteContactPerson(SelectedContactperson);
            Contactpersons.Remove(SelectedContactperson);
        }


        public ICommand DeleteContactpersonsCommand
        {
            get
            {

                return new RelayCommand(DeleteContactpersons);

            }

        }

        private void SaveContactpersons()
        {
            if (SelectedContactperson.Id != 0)
            { Contactperson.SaveContactPerson(SelectedContactperson); }
            else { Contactperson.SaveNewContactperson(SelectedContactperson);
            Contactpersons = Contactperson.getContactperson();
            }
            
        }

        //property toevoegen waaraan we de list box uit de usercontrol homepage aan zullen binden.
        private ObservableCollection<Contactperson> _Contactpersons;
        public ObservableCollection<Contactperson> Contactpersons
        {
            get
            {

                return _Contactpersons;
            }

            set
            {

                _Contactpersons = value;
                OnPropertyChanged("Contactpersons");
            }
        }

        public string Name
        {
            get { return "MedeWerkers"; }
        }

        public ICommand LogoCommand
        {
            get
            {

                return new RelayCommand(Logo);

            }

        }

        private void Logo()
        {
            Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Title = "kies Logo";
            openFileDialog1.InitialDirectory = "";
            openFileDialog1.Filter = "JPG files (*.jpg)|*.jpg|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            SelectedContactperson.foto = openFileDialog1.FileName;
                            OnPropertyChanged("SelectedFestival");
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }

        }


    }
}

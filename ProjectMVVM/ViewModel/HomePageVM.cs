using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using ProjectMVVM.Model;

namespace ProjectMVVM.ViewModel
{
    class HomePageVM : ObserableObject, IPage     {
        public string Name
        {
            get { return "HomePage"; }
        }

        public HomePageVM()
        {

            _FestivalsSelected = Festival.getFestival();
        }
        private Festival _FestivalsSelected;
        public Festival SelectedFestival
        {
            get
            {

                return _FestivalsSelected;
            }
            set
            {

                _FestivalsSelected = value;
                OnPropertyChanged("SelectedFestival");
            }

        }


        public ICommand SavefestivalCommand
        {
            get
            {

                return new RelayCommand(Savefestival);

            }

        }

        private void Savefestival()
        {
            Festival.SaveFestival(SelectedFestival);

        }

        public ICommand GrondplanCommand
        {
            get
            {

                return new RelayCommand(Grondplan);

            }

        }

        private void Grondplan()
        {
            Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Title="kies Grondplan";
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
                            SelectedFestival.Grondplan=openFileDialog1.FileName;
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
            openFileDialog1.Title="kies Logo";
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
                            SelectedFestival.Logo=openFileDialog1.FileName;
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

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
    class BandsVM : ObserableObject, IPage
    {

        public string Name
        {
            get { return "Bands"; }
        }

        public BandsVM() {

            _Genres = Genre.getGenre();
            _Bands = Band.getBand();
            
            
        }

        //property toevoegen waaraan we de list box uit de usercontrol homepage aan zullen binden.
        private ObservableCollection<Genre> _Genres;
        public ObservableCollection<Genre> Genres
        {
            get
            {
                return _Genres;
            }

            set
            {
                _Genres = value;
                OnPropertyChanged("Genres");
            }
        }

        private Genre _GenreSelected;
        public Genre SelectedGenre
        {
            get
            {

                return _GenreSelected;
            }
            set
            {
                _GenreSelected = value;
                OnPropertyChanged("SelectedGenre");
            }
        }

        private Band _BandsSelected;
        public Band SelectedBand
        {
            get
            {
                return _BandsSelected;
            }
            set
            {

                _BandsSelected = value;
                _Genres = Genre.getGenre();
                //controleert welke genres er aan de band hangen.
                if (_BandsSelected.Name!=null)
                {
                    foreach (Genre genreg in Genres)
                    {
                        genreg.checkgenre = false;
                    }
                    foreach (Genre genre in _BandsSelected.Genres)
                    {
                        foreach (Genre genreg in Genres)
                        {
                            if (genre.Id == genreg.Id)
                            {
                                genreg.checkgenre = true;
                            }
                        }
                    }
                }
                OnPropertyChanged("Genres");
                OnPropertyChanged("SelectedBand");
            }

        }

        //property toevoegen waaraan we de list box uit de usercontrol homepage aan zullen binden.
        private ObservableCollection<Band> _Bands;
        public ObservableCollection<Band> Bands
        {
            get
            {

                return _Bands;
            }

            set
            {

                _Bands = value;
                OnPropertyChanged("Bands");
            }
        }

        public ICommand AddNewBandsCommand
        {
            get
            {

                return new RelayCommand(AddNewBands);

            }

        }

        private void AddNewBands()
        {
            Band anieuw = new Band();
            Bands.Add(anieuw);
            SelectedBand = anieuw;
            
        }

        public ICommand SaveBandsCommand
        {
            get
            {

                return new RelayCommand(SaveBands);

            }

        }

        private void SaveBands()
        {
            if (SelectedBand.Id != 0)
            { Band.SaveBand(SelectedBand); }
            else
            {
                Band.SaveNewBand(SelectedBand);
                Bands = Band.getBand();
            }

        }

        private void DeleteBands()
        {

            Band.DeleteBand(SelectedBand);
            Bands.Remove(SelectedBand);
        }

        public ICommand DeleteBandsCommand
        {
            get
            {

                return new RelayCommand(DeleteBands);

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
                            SelectedBand.Picture = openFileDialog1.FileName;
                            OnPropertyChanged("SelectedBand");
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }

        }

        public ICommand AddNewGenreCommand
        {
            get
            {

                return new RelayCommand(AddNewGenre);

            }

        }

        private void AddNewGenre()
        {
            Genre anieuw = new Genre();

            SelectedGenre = anieuw;
            Genres.Add(anieuw);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using ProjectMVVM.Model;

namespace ProjectMVVM.ViewModel
{
    class LineUpVM : ObserableObject, IPage
    {

        public string Name
        {
            get { return "LineUP"; }
        }

        public LineUpVM()
        {
            _Stages = Stage.getStage();
            _Bands = Band.getBand();
        }

        private LineUp _LineUpSelected;
        public LineUp SelectedLineUp {
            get {

                return _LineUpSelected;
            }
            set { 
            
            _LineUpSelected=value;
            SelectedBands = _LineUpSelected.band;
            OnPropertyChanged("SelectedLineUp");
            }

        }

        //property toevoegen waaraan we de list box uit de usercontrol homepage aan zullen binden.
        private ObservableCollection<LineUp> _LineUps;
        public ObservableCollection<LineUp> LineUps
        {
            get
            {
                return _LineUps;
            }

            set
            {

                _LineUps = value;
                OnPropertyChanged("LineUps");
            }
        }

        private Stage _StagesSelected;
        public Stage SelectedStages {
            get {

                return _StagesSelected;
            }
            set { 
            
            _StagesSelected=value;
            OnPropertyChanged("SelectedStages");
            LineUps = LineUp.getLineUp(SelectedStages);
            }

        }

        //property toevoegen waaraan we de list box uit de usercontrol homepage aan zullen binden.
        private ObservableCollection<Stage> _Stages;
        public ObservableCollection<Stage> Stages
        {
            get
            {

                return _Stages;
            }

            set
            {

                _Stages = value;
                OnPropertyChanged("Stages");
            }
        }

        public ICommand AddNewStagesCommand
        {
            get
            {

                return new RelayCommand(AddNewStages);

            }

        }

        private void AddNewStages()
        {
            Stage anieuw = new Stage();

            SelectedStages = anieuw;
            Stages.Add(anieuw);
        }

        public ICommand SaveStagesCommand
        {
            get
            {

                return new RelayCommand(SaveStages);

            }

        }
        private void DeleteStages()
        {

            Stage.DeleteStage(SelectedStages);
            Stages.Remove(SelectedStages);
        }

        public ICommand DeleteStagesCommand
        {
            get
            {

                return new RelayCommand(DeleteStages);

            }

        }

        private void SaveStages()
        {
            if (SelectedStages.Id != 0)
            { Stage.SaveStage(SelectedStages); }
            else
            {
                Stage.SaveNewStage(SelectedStages);
                Stages = Stage.getStage();
            }

        }

        public ICommand AddNewLineUpsCommand
        {
            get
            {
                return new RelayCommand(AddNewLineUps);
            }

        }

        private void AddNewLineUps()
        {
            LineUp anieuw = new LineUp();

            SelectedLineUp = anieuw;
            LineUps.Add(anieuw);
        }

        public ICommand SaveLineUpsCommand
        {
            get
            {

                return new RelayCommand(SaveLineUps);

            }

        }
        private void DeleteLineUps()
        {

            LineUp.DeleteLineUp(SelectedLineUp);
            LineUps.Remove(SelectedLineUp);
        }


        public ICommand DeleteLineUpsCommand
        {
            get
            {

                return new RelayCommand(DeleteLineUps);

            }

        }

        private void SaveLineUps()
        {
            if (SelectedLineUp.Id != 0)
            { LineUp.SaveLineUp(SelectedLineUp); }
            else
            {
                SelectedLineUp.band = SelectedBands;
                LineUp.SaveNewLineUp(SelectedLineUp);
                LineUps = LineUp.getLineUp(SelectedStages);
            }

        }

        private Band _BandsSelected;
        public Band SelectedBands
        {
            get
            {

                return _BandsSelected;
            }
            set
            {

                _BandsSelected = value;
                OnPropertyChanged("SelectedBands");
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


    }

    }


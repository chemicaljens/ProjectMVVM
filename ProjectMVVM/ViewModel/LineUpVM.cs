using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
        private Festival fest;
        public LineUpVM()
        {
            _Stages = Stage.getStage();
            fest = Festival.getFestival();
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
            
            if (SelectedStages.Id != 0 )
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
            
            anieuw.Date = fest.StarDate;
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
            Boolean errortime =false;
            foreach (LineUp lineu in LineUps)
                {
                    Int32 uurf; Int32 minf; Int32 uuru; Int32 minu; Int32 uurs ;Int32 mins; Int32 uursf ; Int32 minsf;
                    try
                    {
                        String[] timef = lineu.From.Split(':');
                        uurf = Int32.Parse(timef[0]);
                         minf = Int32.Parse(timef[1]);
                        String[] timeu = lineu.Until.Split(':');
                         uuru = Int32.Parse(timeu[0]);
                         minu = Int32.Parse(timeu[1]);

                        String[] times = SelectedLineUp.Until.Split(':');
                         uurs = Int32.Parse(times[0]);
                         mins = Int32.Parse(times[1]);
                        String[] timesf = SelectedLineUp.From.Split(':');
                         uursf = Int32.Parse(timesf[0]);
                         minsf = Int32.Parse(timesf[1]);
                    }
                    catch (Exception)
                    {
                        
                        throw;
                    }
                
                    if ( (uurf<uursf && uursf<uuru) | (uursf==uurf && minf<minsf) | (uuru==uursf && minsf<minu))
                    {
                        MessageBox.Show("Uw start uur overlapt gelieve dit aan te passen.");
                        errortime = true;
                    }

                    if ((uurf < uurs && uurs < uuru) | (uurs == uurf && minf > minsf) | (uuru == uursf && minsf < minu))
                    {
                        MessageBox.Show("Uw eind uur overlapt gelieve dit aan te passen.");
                        errortime = true;
                    }
                    
                }
            if (SelectedLineUp.Date< fest.StarDate | SelectedLineUp.Date>fest.EndDate)
            {
                MessageBox.Show("gelieve een geldige datum te selecteren.");
                errortime = true;
            }

            if (SelectedBands.Id == 0)
            {
                MessageBox.Show("gelieve een band te selecteren.");
                errortime = true;
            }
            else
            {
                SelectedLineUp.band = SelectedBands;
                SelectedLineUp.stage = SelectedStages.Id;
            }
            if (!errortime)
            {

                if (SelectedLineUp.Id != 0)
                { LineUp.SaveLineUp(SelectedLineUp);
                LineUps = LineUp.getLineUp(SelectedStages);
                }
                else
                {
                    SelectedLineUp.band = SelectedBands;
                    LineUp.SaveNewLineUp(SelectedLineUp);
                    LineUps = LineUp.getLineUp(SelectedStages);
                }
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


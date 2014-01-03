using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace ProjectMVVM.ViewModel
{
    class ApplicationVM : ObserableObject
    {


        public ApplicationVM()
        {
            _pages = new ObservableCollection<IPage>();
            //nieuwe pagina toevoegen.
            //bij nieuwe pages moet deze lijst aangebuld worden met de nieuwe viewModel klasse.
            _pages.Add(new HomePageVM());
            _pages.Add(new TicketsVM());
            _pages.Add(new LineUpVM());
            _pages.Add(new BandsVM());
            _pages.Add(new MedeWerkersVM());

            //defaul zetten we de current page in op eerste item
            _currentPage = Pages[0];
        }

        private IPage _currentPage;
        public IPage CurrentPage
        {
            get
            {
                return _currentPage;
            }
            set
            {
                _currentPage = value;

                OnPropertyChanged("CurrentPage");
            }
        }

        private ObservableCollection<IPage> _pages;
        public ObservableCollection<IPage> Pages
        {
            get
            {
                return _pages;

            }
            set
            {
                _pages = value;
                OnPropertyChanged("Pages");

            }
        }

        public ICommand ChangePageCommand
        {
            get { return new RelayCommand<IPage>(ChangePage); }

        }

        private void ChangePage(IPage page)
        {

            CurrentPage = page;

        }

    }
}

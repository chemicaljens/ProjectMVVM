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
    class TicketsVM : ObserableObject, IPage
    {
        public TicketsVM()
        {

            _Tickets = Ticket.getTickets();
            _TicketTypes = TicketType.getTicketType();
            _users = User.getUsers();

        }
        private ObservableCollection<User> _users;
        public ObservableCollection<User> Users {
            get 
            {

                return _users;

            }

            set 
            {
                _users = value;
                OnPropertyChanged("Users");

            }
        }

        private User _Selecteduser;
        public User SelectedUser
        {
            get
            {

                return _Selecteduser;

            }

            set 
            {
                _Selecteduser = value;
                OnPropertyChanged("SelectedUser");

            }
        }

        private TicketType _TicketTypeSelected;
        public TicketType SelectedTicketType
        {
            get
            {

                return _TicketTypeSelected;
            }
            set
            {

                _TicketTypeSelected = value;
                OnPropertyChanged("SelectedTicketType");
            }

        }

        //property toevoegen waaraan we de list box uit de usercontrol homepage aan zullen binden.
        private ObservableCollection<TicketType> _TicketTypes;
        public ObservableCollection<TicketType> TicketTypes
        {
            get
            {

                return _TicketTypes;
            }

            set
            {

                _TicketTypes = value;
                OnPropertyChanged("TicketTypes");
            }
        }



        private Ticket _TicketSelected;
        public Ticket SelectedTicket {
            get {

                return _TicketSelected;
            }
            set { 
            
            _TicketSelected=value;
            OnPropertyChanged("SelectedTickets");
            }

        }


        //property toevoegen waaraan we de list box uit de usercontrol homepage aan zullen binden.
        private ObservableCollection<Ticket> _Tickets;
        public ObservableCollection<Ticket> Tickets
        {
            get
            {

                return _Tickets;
            }

            set
            {

                _Tickets = value;
                OnPropertyChanged("Tickets");
            }
        }

        public string Name
        {
            get { return "Tickets"; }
        }


        public ICommand AddNewTicketTypesCommand
        {
            get
            {

                return new RelayCommand(AddNewTicketTypes);

            }

        }

        private void AddNewTicketTypes()
        {
            TicketType anieuw = new TicketType();

            SelectedTicketType = anieuw;
            TicketTypes.Add(anieuw);
        }

        public ICommand SaveTicketTypesCommand
        {
            get
            {

                return new RelayCommand(SaveTicketTypes);

            }

        }
        private void DeleteTicketTypes()
        {

            TicketType.DeleteTicketType(SelectedTicketType);
            TicketTypes.Remove(SelectedTicketType);
        }


        public ICommand DeleteTicketTypesCommand
        {
            get
            {

                return new RelayCommand(DeleteTicketTypes);

            }

        }

        private void SaveTicketTypes()
        {
            if (SelectedTicketType.Id != 0)
            { TicketType.SaveTicketType(SelectedTicketType); }
            else
            {
                TicketType.SaveNewTicketType(SelectedTicketType);
                TicketTypes = TicketType.getTicketType();
            }

        }

        public ICommand AddNewTicketsCommand
        {
            get
            {

                return new RelayCommand(AddNewTickets);

            }

        }

        private void AddNewTickets()
        {
            Ticket anieuw = new Ticket();

            SelectedTicket = anieuw;
            anieuw.Tickettype = SelectedTicketType;
            anieuw.user = SelectedUser;
            Tickets.Add(anieuw);
        }

        public ICommand SaveTicketsCommand
        {
            get
            {

                return new RelayCommand(SaveTickets);

            }

        }
        private void DeleteTickets()
        {

            Ticket.DeleteTicket(SelectedTicket);
            Tickets.Remove(SelectedTicket);
        }


        public ICommand DeleteTicketsCommand
        {
            get
            {

                return new RelayCommand(DeleteTickets);

            }

        }

        private void SaveTickets()
        {
            if (SelectedTicket.Id != 0)
            { Ticket.SaveTicket(SelectedTicket); }
            else
            {
                Ticket.SaveNewTicket(SelectedTicket);
                Tickets = Ticket.getTickets();

            }

        }


        public ICommand AddNewUserCommand
        {
            get
            {

                return new RelayCommand(AddNewUser);

            }

        }

        private void AddNewUser()
        {
            User anieuw = new User();

            SelectedUser = anieuw;
            Users.Add(anieuw);
        }

        public ICommand SaveUserCommand
        {
            get
            {

                return new RelayCommand(SaveUser);

            }

        }
        private void DeleteUser()
        {

            User.DeleteUser(SelectedUser);
            Users.Remove(SelectedUser);
        }

        public ICommand DeleteUserCommand
        {
            get
            {

                return new RelayCommand(DeleteUser);

            }

        }

        private void SaveUser()
        {
            if (SelectedUser.Id != 0)
            { User.SaveUser(SelectedUser); }
            else
            {
                User.SaveNewUser(SelectedUser);
                Users = User.getUsers();
            }

        }


    }
}

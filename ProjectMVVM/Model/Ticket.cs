using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectMVVM.Model;

namespace ProjectMVVM.Model
{
    class Ticket
    {
        public int Id { get; set; }
        public  User user { get; set; }
        public  TicketType Tickettype { get; set; }
        public int Amount { get; set; }

        internal static ObservableCollection<Ticket> getTickets()
        {
            ObservableCollection<User> allUsers = User.getUsers();
            ObservableCollection<TicketType> TicketTypes = TicketType.getTicketType();

            ObservableCollection<Ticket> lijst = new ObservableCollection<Ticket>();

            string SQL = "SELECT * FROM Ticket";
            DbDataReader reader = Database.GetData(SQL);

            while (reader.Read())
            {
                Ticket anieuw = new Ticket();
                anieuw.Id = Int32.Parse(reader["Id"].ToString());
                foreach (User auser in allUsers)
                {
                    if (auser.Id == Int32.Parse(reader["TicketBuyer"].ToString()))
                    { anieuw.user = auser; }
                }

                foreach (TicketType aTicketType in TicketTypes)
                {
                    if (aTicketType.Id == Int32.Parse(reader["TicketType"].ToString()))
                    { anieuw.Tickettype = aTicketType; }
                }

                anieuw.Amount = Int32.Parse(reader["Amount"].ToString());
                
                lijst.Add(anieuw);
            }
            return lijst;
        }

        public string Error
        {
            get { return "er is een fout"; }
        }

        public string this[string columnName]
        {
            get
            {
                try
                {
                    object value = this.GetType().GetProperty(columnName).GetValue(this);
                    Validator.ValidateProperty(value, new ValidationContext(this, null, null)
                    {
                        MemberName = columnName
                    });
                }
                catch (ValidationException ex)
                {
                    return ex.Message;
                }
                return String.Empty;
            }
        }

        public static void SaveNewTicket(Ticket nieuwticket)
        {
            String SQL = "INSERT INTO Ticket (TicketBuyer,TicketType,Amount)VALUES(@TicketBuyer,@TicketType,@Amount)";
            DbParameter par1 = Database.AddParameter("@TicketBuyer", nieuwticket.user.Id);
            DbParameter par2 = Database.AddParameter("@TicketType", nieuwticket.Tickettype.Id);
            DbParameter par3 = Database.AddParameter("@Amount", nieuwticket.Amount);
            try
            {
                Database.ModifyData(SQL, par1, par2, par3);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void DeleteTicket(Ticket SelectedTickets)
        {
            String SQL = "DELETE From Ticket where id=@id";
            DbParameter par1 = Database.AddParameter("@Id", SelectedTickets.Id);
            if (par1.Value == null) par1.Value = DBNull.Value;
            try
            {
                Database.ModifyData(SQL, par1);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void SaveTicket(Ticket SelectedTicket)
        {
            String SQL = "Update Ticket SET TicketBuyer=@TicketBuyer , TicketType=@TicketType , Amount=@Amount Where Id=" + SelectedTicket.Id;
            DbParameter par1 = Database.AddParameter("@TicketBuyer", SelectedTicket.user.Id);
            DbParameter par2 = Database.AddParameter("@TicketType", SelectedTicket.Tickettype.Id);
            DbParameter par3 = Database.AddParameter("@Amount", SelectedTicket.Amount);
            try
            {
                Database.ModifyData(SQL, par1, par2, par3);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

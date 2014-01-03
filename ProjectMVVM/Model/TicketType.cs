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
    class TicketType
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public Double Price { get; set; }
        public int AvailableTickets { get; set; }

        internal static System.Collections.ObjectModel.ObservableCollection<TicketType> getTicketType()
        {
            ObservableCollection<TicketType> lijst = new ObservableCollection<TicketType>();

            string SQL = "SELECT * FROM TicketType";
            DbDataReader reader = Database.GetData(SQL);

            while (reader.Read())
            {
                TicketType anieuw = new TicketType();
                anieuw.Id = Int32.Parse(reader["Id"].ToString());
                anieuw.Name = reader["Name"].ToString();
                anieuw.Price = Double.Parse(reader["Price"].ToString());
                anieuw.AvailableTickets = Int32.Parse(reader["AvailableTickets"].ToString());

                lijst.Add(anieuw);
            }
            return lijst;
        }

        public static void SaveNewTicketType(TicketType nieuwTicketType)
        {

            String SQL = "INSERT INTO TicketType (Name,Price,AvailableTickets)VALUES(@Name,@Price,@AvailableTickets)";
            DbParameter par1 = Database.AddParameter("@Name", nieuwTicketType.Name);
            DbParameter par2 = Database.AddParameter("@Price", nieuwTicketType.Price);
            DbParameter par3 = Database.AddParameter("@AvailableTickets", nieuwTicketType.AvailableTickets);
            try
            {
                Database.ModifyData(SQL, par1, par2, par3);
            }
            catch (Exception)
            {

                throw;
            }
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

        public static void DeleteTicketType(TicketType SelectedTicketType)
        {
            String SQL = "DELETE From TicketType where id=@id";
            DbParameter par1 = Database.AddParameter("@Id", SelectedTicketType.Id);
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

        public static void SaveTicketType(TicketType SelectedTicketType)
        {
            String SQL = "Update TicketType SET Name=@Name , Price=@Price , AvailableTickets=@AvailableTickets Where Id=" + SelectedTicketType.Id;
            DbParameter par1 = Database.AddParameter("@Name", SelectedTicketType.Name);
            DbParameter par2 = Database.AddParameter("@Price", SelectedTicketType.Price);
            DbParameter par3 = Database.AddParameter("@AvailableTickets", SelectedTicketType.AvailableTickets);
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

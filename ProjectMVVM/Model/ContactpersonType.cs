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
    class ContactpersonType
    {

        public int Id { get; set; }
        public String Name { get; set; }

        public static ObservableCollection<ContactpersonType> getContactpersonType()
        {
            ObservableCollection<ContactpersonType> lijst = new ObservableCollection<ContactpersonType>();

            string SQL = "SELECT * FROM Contactpersontype";
            DbDataReader reader = Database.GetData(SQL);

            while (reader.Read())
            {
                ContactpersonType anieuw = new ContactpersonType();
                anieuw.Id = Int32.Parse(reader["Id"].ToString());
                anieuw.Name = reader["Name"].ToString();
                lijst.Add(anieuw);
            }
            return lijst;
        }

        public static void SaveNewContactpersonType(ContactpersonType ContactpersonType)
        {

            String SQL = "INSERT INTO Contactpersontype VALUES(@Id,@)";

            DbParameter par1 = Database.AddParameter("@Id", ContactpersonType.Id);

            if (par1.Value == null) par1.Value = DBNull.Value;

            Database.ModifyData(SQL, par1);
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

        public static ContactpersonType getSelectedContactperson(int id)
        {
            ContactpersonType SelectedContactperson = new ContactpersonType();

            string SQL = "SELECT * FROM Contactpersontype where Id=@Id";

            
            DbParameter par1 = Database.AddParameter("@Id", id);

            if (par1.Value == null) par1.Value = DBNull.Value;

            DbDataReader reader = Database.GetData(SQL, par1);

            while (reader.Read())
            {
                SelectedContactperson.Id = Int32.Parse(reader["Id"].ToString());
                SelectedContactperson.Name = reader["Name"].ToString();      
            }




            return SelectedContactperson;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectMVVM.Model;

namespace ProjectMVVM.Model
{
    class User : IDataErrorInfo
    {

        public int Id { get; set; }
        [Required(ErrorMessage = "gelieve een voornaam in the vullen")]
        public String FirstName { get; set; }
        [Required(ErrorMessage = "gelieve een Naam in the vullen")]
        public String Name { get; set; }
        [Required(ErrorMessage="gelieve een e-mail adress in the vullen")]
        [EmailAddress(ErrorMessage="gelieve een geldig e-mail adress in te vullen")]
        public String Email { get; set; }
        [Required(ErrorMessage = "gelieve een login in the vullen")]
        public String Login { get; set; }
        [Required(ErrorMessage = "gelieve een  password in the vullen")]
        public String Password { get; set; }
        [Required(ErrorMessage = "gelieve een geboorteDatum in the vullen")]
        public DateTime BirthDay { get; set; }

        internal static System.Collections.ObjectModel.ObservableCollection<User> getUsers()
        {
            ObservableCollection<User> lijst = new ObservableCollection<User>();

            string SQL = "SELECT * FROM FestivalVisitor";
            DbDataReader reader = Database.GetData(SQL);

            while (reader.Read())
            {
                User anieuw = new User();
                anieuw.Id = Int32.Parse(reader["Id"].ToString());
                anieuw.FirstName = reader["FirstName"].ToString();
                anieuw.Name = reader["Name"].ToString();
                anieuw.Email = reader["Email"].ToString();
                anieuw.Login = reader["Login"].ToString();
                anieuw.Password = reader["Password"].ToString();
                anieuw.BirthDay = (DateTime)reader["BirthDay"];

                lijst.Add(anieuw);
            }
            return lijst;
        }

        public static void SaveNewUser(User nieuwUser)
        {

            String SQL = "INSERT INTO FestivalVisitor VALUES(@Name,@FirstName,@Email,@Login,@Password,@BirthDay)";

            DbParameter par1 = Database.AddParameter("@NAME", nieuwUser.Name);
            DbParameter par2 = Database.AddParameter("@Firstname", nieuwUser.FirstName);
            DbParameter par3 = Database.AddParameter("@Email", nieuwUser.Email);
            DbParameter par4 = Database.AddParameter("@Login", nieuwUser.Login);
            DbParameter par5 = Database.AddParameter("@Password", nieuwUser.Password);
            DbParameter par6 = Database.AddParameter("@BirthDay", nieuwUser.BirthDay);

            if (par1.Value == null) par1.Value = DBNull.Value;
            try
            {
                Database.ModifyData(SQL, par1, par2, par3, par4, par5, par6);
            }
            catch (Exception)
            {           
                throw;
            }
            
        }

        internal static void DeleteUser(User SelectedUser)
        {
            String SQL = "DELETE From FestivalVisitor where id=@id";
            DbParameter par1 = Database.AddParameter("@Id", SelectedUser.Id);
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

        internal static void SaveUser(User SelectedUser)
        {
            String SQL = "Update FestivalVisitor SET Name=@Name,FirstName=@FirstName,Email=@Email,Login=@Login,Password=@Password,Birthday=@BirthDay Where Id=" + SelectedUser.Id;
            DbParameter par1 = Database.AddParameter("@NAME", SelectedUser.Name);
            DbParameter par2 = Database.AddParameter("@Firstname", SelectedUser.FirstName);
            DbParameter par3 = Database.AddParameter("@Email", SelectedUser.Email);
            DbParameter par4 = Database.AddParameter("@Login", SelectedUser.Login);
            DbParameter par5 = Database.AddParameter("@Password", SelectedUser.Password);
            DbParameter par6 = Database.AddParameter("@BirthDay", SelectedUser.BirthDay);
            try
            {
                Database.ModifyData(SQL, par1, par2, par3, par4, par5, par6);
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
    }
}

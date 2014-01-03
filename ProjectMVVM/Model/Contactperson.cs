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
    class Contactperson : IDataErrorInfo
    {
        public int Id{ get; set; }

        [Required(ErrorMessage = "De Naam is Verplicht")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "min 2 karakters, max 50 karakters")]
        public String Name { get; set; }

        [Required(ErrorMessage = "De bedrijf is Verplicht")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "min 2 karakters, max 50 karakters")]
        public String Company { get; set; }

        [Required(ErrorMessage="gelieve een positie te selecteren")]
        public ContactpersonType JobRole { get; set; }

        [Required(ErrorMessage = "De Stad is Verplicht")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "min 2 karakters, max 50 karakters")]
        public String City { get; set; }

        [EmailAddress(ErrorMessage = "Dit is geen geldig E-mail adress")]
        public String Email { get; set; }

        [Phone(ErrorMessage = "Dit is geen geldig Telefoon nr.")]
        public String Phone { get; set; }

        [Phone(ErrorMessage = "Dit is geen geldig Telefoon nr.")]
        public String CellPhone { get; set; }
        
        [Required(ErrorMessage = "het Adress is Verplicht")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "min 2 karakters, max 50 karakters")]
        public String Address { get; set; }

        public String foto { get; set; }

        public static ObservableCollection<Contactperson> getContactperson()
        {
            ObservableCollection<Contactperson> lijst = new ObservableCollection<Contactperson>();

            string SQL = "SELECT * FROM Contactperson";
            DbDataReader reader = Database.GetData(SQL);

            while (reader.Read())
            {
                Contactperson anieuw = new Contactperson();
                anieuw.Id = Int32.Parse(reader["Id"].ToString());
                anieuw.Name = reader["Name"].ToString();
                anieuw.Company = reader["Company"].ToString();
                anieuw.City = reader["City"].ToString();
                anieuw.Email = reader["Email"].ToString();
                anieuw.Phone = reader["Phone"].ToString();
                anieuw.CellPhone = reader["CellPhone"].ToString();
                anieuw.Address = reader["Address"].ToString();
                anieuw.JobRole = ContactpersonType.getSelectedContactperson(Int32.Parse(reader["Jobrole"].ToString()));
                anieuw.foto = reader["Foto"].ToString();

                
                lijst.Add(anieuw);
            }
            return lijst;
        }

        public static void SaveNewContactperson(Contactperson NewContactPerson)
        {
            String SQL = "INSERT INTO Contactperson (Name,Company,Jobrole,City,Email,Phone,CellPhone,Address,Foto)VALUES(@Name,@Company,@Jobrole,@City,@Email,@Phone,@CellPhone,@Address,@Foto)";
            DbParameter par1 = Database.AddParameter("@Name", NewContactPerson.Name);
            DbParameter par2 = Database.AddParameter("@Company", NewContactPerson.Company);
            DbParameter par3 = Database.AddParameter("@Jobrole", NewContactPerson.JobRole.Id);
            DbParameter par4 = Database.AddParameter("@City", NewContactPerson.City);
            DbParameter par5 = Database.AddParameter("@Email", NewContactPerson.Email);
            if (par5.Value == null) par5.Value = DBNull.Value;
            DbParameter par6 = Database.AddParameter("@Phone", NewContactPerson.Phone);
            if (par6.Value == null) par6.Value = DBNull.Value;
            DbParameter par7 = Database.AddParameter("@CellPhone", NewContactPerson.CellPhone);
            if (par7.Value == null) par7.Value = DBNull.Value;
            DbParameter par8 = Database.AddParameter("@Address", NewContactPerson.Address);
            DbParameter par9 = Database.AddParameter("@Foto", NewContactPerson.foto);
            if (par9.Value == null) par9.Value = DBNull.Value;
            try 
	        {	        
		        Database.ModifyData(SQL, par1, par2, par3, par4, par5, par6, par7, par8, par9);
	        }
	        catch (Exception)
	        {
		
    		throw;
	        }
            
        }

        internal static void SaveContactPerson(Contactperson ContactPerson)
        {
            String SQL = "Update Contactperson SET Name=@Name ,Company=@Company ,Jobrole=@jobrole,City=@city,Email=@Email,Phone=@phone,CellPhone=@CellPhone,Address=@Address,Foto=@Foto Where Id=" + ContactPerson.Id;
            DbParameter par1 = Database.AddParameter("@Name", ContactPerson.Name);
            DbParameter par2 = Database.AddParameter("@Company", ContactPerson.Company);
            DbParameter par3 = Database.AddParameter("@Jobrole", ContactPerson.JobRole.Id);
            DbParameter par4 = Database.AddParameter("@City", ContactPerson.City);
            DbParameter par5 = Database.AddParameter("@Email", ContactPerson.Email);
            if (par5.Value == null) par5.Value = DBNull.Value;
            DbParameter par6 = Database.AddParameter("@Phone", ContactPerson.Phone);
            if (par6.Value == null) par6.Value = DBNull.Value;
            DbParameter par7 = Database.AddParameter("@CellPhone", ContactPerson.CellPhone);
            if (par7.Value == null) par7.Value = DBNull.Value;
            DbParameter par8 = Database.AddParameter("@Address", ContactPerson.Address);
            DbParameter par9 = Database.AddParameter("@Foto", ContactPerson.foto);
            if (par9.Value == null) par9.Value = DBNull.Value;
            try
            {
                Database.ModifyData(SQL, par1, par2, par3, par4, par5, par6, par7, par8, par9);
            }
            catch (Exception)
            {
                
                throw;
            }
            
        }

        internal static void DeleteContactPerson(Contactperson SelectedContactperson)
        {
            String SQL = "DELETE From Contactperson where id=@id";
            DbParameter par1 = Database.AddParameter("@Id", SelectedContactperson.Id);
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

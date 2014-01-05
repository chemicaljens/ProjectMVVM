using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMVVM.Model
{
    class Genre
    {

        public int Id { get; set; }
        public String Name { get; set; }
        public bool checkgenre{get; set;}

        internal static ObservableCollection<Genre> getGenre()
        {
            ObservableCollection<Genre> lijst = new ObservableCollection<Genre>();

            string SQL = "SELECT * FROM Genre";
            DbDataReader reader = Database.GetData(SQL);

            while (reader.Read())
            {
                Genre anieuw = new Genre();
                anieuw.Id = Int32.Parse(reader["Id"].ToString());
                anieuw.Name = reader["Genre"].ToString();
                anieuw.checkgenre = true;

                lijst.Add(anieuw);
            }
            return lijst;
        }

        internal static ObservableCollection<Genre> getGenres(int id)
        {
            ObservableCollection<Genre> Genrelijst = new ObservableCollection<Genre>();

            string SQLGenre = "SELECT id,Genre.Genre FROM Genre JOIN BandGenre ON Genre.Id=BandGenre.Genre AND Band=@Id";
            DbParameter par1 = Database.AddParameter("@Id", id);
            DbDataReader reader = Database.GetData(SQLGenre,par1);

            while (reader.Read())
            {
                Genre anieuw = new Genre();
                anieuw.Id = Int32.Parse(reader["Id"].ToString());
                anieuw.Name = reader["Genre"].ToString();

                Genrelijst.Add(anieuw);

            }
            return Genrelijst;
        }

        internal static void updateBandGenre(Band SelectedBand,ObservableCollection<Genre> genres)
        {
            String SQL = "DELETE From BandGenre where Band=@Band";
            DbParameter par1 = Database.AddParameter("@Band", SelectedBand.Id);
            if (par1.Value == null) par1.Value = DBNull.Value;
            try
            {
                Database.ModifyData(SQL, par1);
            }
            catch (Exception)
            {

                throw;
            }

            foreach (Genre gen in genres)
            {
                if (gen.checkgenre == true)
                {

                    String SQL2 = "INSERT INTO BandGenre (Band,Genre)VALUES(@Band,@Genre)";
                    DbParameter par11 = Database.AddParameter("@Band", SelectedBand.Id);
                    DbParameter par12 = Database.AddParameter("@Genre", gen.Id);
                    try
                    {
                        Database.ModifyData(SQL2, par11, par12);
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
            }
        }

        public static void SaveGenre(Genre SelectedGenre)
        {
            String SQL = "Update Genre SET Genre=@Genre  Where Id=" + SelectedGenre.Id;
            DbParameter par1 = Database.AddParameter("@Genre", SelectedGenre.Name);
            try
            {
                Database.ModifyData(SQL, par1);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void SaveNewGenre(Genre nieuwGenre)
        {

            String SQL = "INSERT INTO Genre (Genre)VALUES(@Genre)";
            DbParameter par1 = Database.AddParameter("@Genre", nieuwGenre.Name);
            try
            {
                Database.ModifyData(SQL, par1);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}

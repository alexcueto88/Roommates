using Microsoft.Data.SqlClient;
using Roommates.Models;
using Roommates.Repositories;

namespace Roomates.Repositories
{
    public class ChoreRepository : BaseRepository 
    {
        public ChoreRepository(string connectionString): base(connectionString) { }

        // GET LIST OF ALL THE CHORES IN THE DATABE

        public List<Chore> GetAll()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Id, Name from Chore";

                    SqlDataReader reader = cmd.ExecuteReader(); 

                    List<Chore> chore = new List<Chore>();

                    while (reader.Read())
                    {
                        int idColumnPosition = reader.GetOrdinal("Id");

                        int idValue = reader.GetInt32(idColumnPosition);

                        int nameColumnPosition = reader.GetOrdinal("Name");
                        string nameValue = reader.GetString(nameColumnPosition);


                        Chore Chore = new Chore
                        {
                            Id = idValue,
                            Name = nameValue,

                        };

                        chore.Add(Chore);
                    }
                    reader.Close();

                        return chore;

                    }

                }
            }


            public Chore GetById(int id)
            {
                using (SqlConnection conn = Connection)
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT NAME and Chore *** ";
                        cmd.Parameters.AddWithValue("id", id);
                        SqlDataReader reader = cmd.ExecuteReader();

                        Chore chore = null;

                        if (reader.Read())
                        {
                            chore = new Chore
                            {
                                Id = id,
                                Name = reader.GetString(reader.GetOrdinal("Name")),

                            };
                        }

                        reader.Close();

                        return chore;
                    }
                }
            }


            public void Insert(Chore chore)
            {
                using (SqlConnection conn = Connection)
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"INSERT INTO Chore (Name)
                                         OUTPUT INSERTED.Id 
                                         VALUES (@name)";
                        cmd.Parameters.AddWithValue("@name", chore.Name);
                        int id = (int)cmd.ExecuteScalar();

                        chore.Id = id;

                    }
                }
            }

        }

     }


 


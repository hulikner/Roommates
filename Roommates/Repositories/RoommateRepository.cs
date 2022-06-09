using Microsoft.Data.SqlClient;
using Roommates.Models;
using System.Collections.Generic;

namespace Roommates.Repositories
{
    public class RoommateRepository : BaseRepository
    {
        public RoommateRepository(string connectionString) : base(connectionString) { }
        public Roommate GetById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT Roommate.FirstName, Roommate.RentPortion, Room.Name
                                        FROM Roommate
                                        JOIN Room ON Room.Id = Roommate.RoomId
                                        WHERE Roommate.Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        Roommate roommate = null;

                        // If we only expect a single row back from the database, we don't need a while loop.
                        if (reader.Read())
                        {
                            roommate = new Roommate()
                            {
                                Id = id,
                                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                RentPortion = reader.GetInt32(reader.GetOrdinal("RentPortion")),
                                Room = new Room()
                                {
                                    Name = reader.GetString(reader.GetOrdinal("Name"))
                                }
                            };
                        }
                        return roommate;
                    }

                }
            }
        }
    }
}

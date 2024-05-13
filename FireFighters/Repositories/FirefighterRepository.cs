using System.Data;
using FireFighters.Models;
using FireFighters.Models.DTOs;
using Microsoft.Data.SqlClient;

namespace FireFighters.Repositories;

public class FirefighterRepository : IFirefighterRepository
{
    private readonly IConfiguration _configuration;

    public FirefighterRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    private SqlConnection GetOpenConnection()
    {
        SqlConnection connection = new(_configuration.GetConnectionString("Default"));
        connection.Open();
        return connection;
    }


    public async Task<ReturnedFireAction> GetActionById(int id)
    {
        using (SqlConnection connection = GetOpenConnection())
        {
            string query = "SELECT * FROM FireAction WHERE IdFireAction = @id";
            using (SqlCommand command = new(query, connection))
            {
                command.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    FireAction fireAction = new FireAction()
                    {
                        IdFireAction = Convert.ToInt32(reader["IdFireAction"]),
                        StartTime = Convert.ToDateTime(reader["StartTime"]),
                        EndTime = Convert.ToDateTime(reader["EndTime"]),
                        NeedSpecialEquipm = Convert.ToBoolean("NeedSpecialEquipm")
                    };

                    IEnumerable<Firefighter> firefighters = await GetFireFightersByActionId(id);

                    return new ReturnedFireAction()
                    {
                        IdFireAction = fireAction.IdFireAction,
                        StartTime = fireAction.StartTime,
                        EndTime = fireAction.EndTime,
                        NeedSpecialEquipm = fireAction.NeedSpecialEquipm,
                        Firefighters = firefighters
                    };
                }
            }
        }

        return null;
    }


    private async Task<IEnumerable<Firefighter>> GetFireFightersByActionId(int id)

    {
        List<Firefighter_Action> firefighterActions = new List<Firefighter_Action>();
        List<Firefighter> firefighters = new List<Firefighter>();

        using (SqlConnection connection = GetOpenConnection())
        {
            string query = "SELECT * FROM Firefighter_Action WHERE IdAction = @id";
            using (SqlCommand command = new(query, connection))
            {
                command.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = await command.ExecuteReaderAsync();


                while (await reader.ReadAsync())
                {
                    firefighterActions.Add(new()
                    {
                        IdAction = Convert.ToInt32(reader[" IdAction"]),
                        IdFirefighter = Convert.ToInt32(reader["IdFirefighter"])
                    });
                }
            }

            foreach (var firefighetAction in firefighterActions)
            {
                int fireFigherId = firefighetAction.IdFirefighter;

                using (SqlConnection connection2 = GetOpenConnection())
                {
                    string query2 = "SELECT * FROM Firefighter WHERE IdAction = @id";
                    using (SqlCommand command = new(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", fireFigherId);
                        SqlDataReader reader = await command.ExecuteReaderAsync();
                        while (await reader.ReadAsync())
                        {
                            firefighters.Add(new()
                            {
                                IdFirefight = Convert.ToInt32(reader["IdFirefight"]),
                                FirstName = Convert.ToString(reader["FirstName"]),
                                LastName = Convert.ToString(reader["LastName"])
                            });
                        }
                    }
                }
            }


            return firefighters;
        }
    }


    public async Task<bool> DeleteActionWithId(int id)
    {
        using SqlConnection connection = GetOpenConnection();

        string query = "SELECT * FROM FireAction WHERE IdFireAction = @Id";

        FireAction fireActionl = null;

        using (SqlCommand command = new(query, connection))
        {
            command.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                fireActionl = new FireAction()
                {
                    IdFireAction = Convert.ToInt32(reader["IdFireAction"]),
                    StartTime = Convert.ToDateTime(reader["StartTime"]),
                    EndTime = Convert.ToDateTime(reader["EndTime"]),
                    NeedSpecialEquipm = Convert.ToBoolean("NeedSpecialEquipm")
                };
            }
        }

        if (fireActionl.EndTime != null)
        {
            return false;
        }
        else
        {
            using (SqlCommand command = new(query, connection))
            {
                command.CommandText = "DELETE FROM FireAction WHERE IdFireAction = @Id";
                command.Parameters.AddWithValue("@Id", id);
                SqlDataReader reader = await command.ExecuteReaderAsync();
                return command.ExecuteNonQuery() > 0;
            }
        }

    }
}
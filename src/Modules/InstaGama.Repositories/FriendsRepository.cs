using InstaGama.Domain.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace InstaGama.Repositories
{
    public class FriendsRepository : IFriendsRepository
    {
        private readonly IConfiguration _configuration;

        public FriendsRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<Friends>> GetFriendsByUserIdAsync(int userId)
        {
            using (var con = new SqlConnection(_configuration["ConnectionString"]))
            {
                var sqlCmd = $@"SELECT AmigoId,
                                       UsuarioId,
                                       Pendencia
                                FROM
                                       Relacionamentos
                                WHERE
                                       UsuarioId= '{userId}'";

                using SqlCommand cmd = new SqlCommand(sqlCmd, con);
                {
                    cmd.CommandType = CommandType.Text;
                    con.Open();

                    var reader = await cmd
                                            .ExecuteReaderAsync()
                                            .ConfigureAwait(false);

                    var friendsForUser = new List<Friends>();

                    while (reader.Read())
                    {
                        var friends = new Friends(int.Parse(reader["AmigoId"].ToString()),
                                                  int.Parse(reader["UsuarioId"].ToString()),
                                                  bool.Parse(reader["Pendencia"].ToString()));

                        friendsForUser.Add(friends);
                    }

                    return friendsForUser;

                }

            }

        }

        public async Task<int> InsertAsync(Friends friends)
        {
            using (var con = new SqlConnection(_configuration["ConnectionString"]))
            {
                var sqlCmd = @"INSERT INTO
                               Friends (AmigosId,
                                        UsuarioId,
                                        Pendencia)
                                VALUES(@amigosId,
                                        @usuarioId,
                                        @pendencia);, SELECT scope_identify();";

                using (var cmd = new SqlCommand(sqlCmd, con))
                {
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("amigoId", friends.UserFriendId);
                    cmd.Parameters.AddWithValue("usuarioId", friends.UserId);
                    cmd.Parameters.AddWithValue("Pendencia", friends.Pendency);

                    con.Open();
                    var id = await cmd
                                    .ExecuteScalarAsync()
                                    .ConfigureAwait(false);

                    return int.Parse(id.ToString());
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var con = new SqlConnection(_configuration["ConnectionString"]))
            {
                var sqlCmd = $@"DELETE 
                                FROM
                                Relacionamentos
                               WHERE 
                                Id={id}";

                using (var cmd = new SqlCommand(sqlCmd, con))
                {
                    cmd.CommandType = CommandType.Text;
                    con.Open();

                    await cmd
                           .ExecuteScalarAsync()
                           .ConfigureAwait(false);
                }
            }
        }
    }
}



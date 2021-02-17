﻿using InstaGama.Domain.Entities;
using InstaGama.Domain.Interfaces;
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
                var sqlCmd = @$"SELECT Id,
										UsuarioId,
                                        UsuarioAmigoId,
										Pendencia
                                FROM
										Amigos
                                WHERE
										UsuarioId='{userId}' and Pendencia=0;";

                using (var cmd = new SqlCommand(sqlCmd, con))
                {
                    cmd.CommandType = CommandType.Text;
                    con.Open();

                    var reader = await cmd
                                        .ExecuteReaderAsync()
                                        .ConfigureAwait(false);

                    var userFriends = new List<Friends>();
                 

                    while (reader.Read())
                    {
                      
                        var friends = new Friends(int.Parse(reader["Id"].ToString()),
                                               int.Parse(reader["UsuarioId"].ToString()),
                                               int.Parse(reader["UsuarioAmigoId"].ToString()),
                                               int.Parse(reader["Pendencia"].ToString()));

                        userFriends.Add(friends);
                    }

                    return userFriends;
                }
            }
        }

        public async Task<List<Friends>> GetFriendsByFriendPendingIdAsync(int userId)
        {
            using (var con = new SqlConnection(_configuration["ConnectionString"]))
            {
                var sqlCmd = @$"SELECT Id,
										UsuarioId,
                                        UsuarioAmigoId,
										Pendencia
                                FROM
										Amigos
                                WHERE
										UsuarioId='{userId}' and Pendencia=1;";

                using (var cmd = new SqlCommand(sqlCmd, con))
                {
                    cmd.CommandType = CommandType.Text;
                    con.Open();

                    var reader = await cmd
                                        .ExecuteReaderAsync()
                                        .ConfigureAwait(false);

                    var userFriends = new List<Friends>();


                    while (reader.Read())
                    {

                        var friends = new Friends(int.Parse(reader["Id"].ToString()),
                                               int.Parse(reader["UsuarioId"].ToString()),
                                               int.Parse(reader["UsuarioAmigoId"].ToString()),
                                               int.Parse(reader["Pendencia"].ToString()));

                        userFriends.Add(friends);
                    }

                    return userFriends;
                }
            }
        }
        public async Task<List<Friends>> GetFriendsByFriendIdAsync(int friendId)
        {
            using (var con = new SqlConnection(_configuration["ConnectionString"]))
            {
                var sqlCmd = @$"SELECT Id,
										UsuarioId,
                                        UsuarioAmigoId,
										Pendencia
                                FROM
										Amigos
                                WHERE
										UsuarioAmigoId='{friendId}' and Pendencia=0;";

                using (var cmd = new SqlCommand(sqlCmd, con))
                {
                    cmd.CommandType = CommandType.Text;
                    con.Open();

                    var reader = await cmd
                                        .ExecuteReaderAsync()
                                        .ConfigureAwait(false);

                    var userFriends = new List<Friends>();


                    while (reader.Read())
                    {

                        var friends = new Friends(int.Parse(reader["Id"].ToString()),
                                               int.Parse(reader["UsuarioId"].ToString()),
                                               int.Parse(reader["UsuarioAmigoId"].ToString()),
                                               int.Parse(reader["Pendencia"].ToString()));

                        userFriends.Add(friends);
                    }

                    return userFriends;
                }
            }
        }
        public async Task<int> InsertAsync(Friends friends)
        {
          using(var con = new SqlConnection(_configuration["ConnectionString"]))
            {
               var sqlCmd = $@"INSERT INTO 
                                        Amigos(UsuarioId,
		                                UsuarioAmigoId,
				                        Pendencia)
				                        VALUES(@usuarioId,
				                               @usuarioAmigoId,
					                           @pendencia); SELECT scope_identity();";

                using(var cmd = new SqlCommand(sqlCmd, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("usuarioId",friends.UserId);
                    cmd.Parameters.AddWithValue("usuarioAmigoId", friends.UserFriendId);
                    cmd.Parameters.AddWithValue("pendencia", friends.Pendency);
                    con.Open();

                    var id = await cmd
                                      .ExecuteScalarAsync()
                                      .ConfigureAwait(false);
                    return int.Parse(id.ToString());
                }
            }
        }

        public async Task UpdateAsync(int idUser, int idFriend)
        {
            using (var con = new SqlConnection(_configuration["ConnectionString"]))
            {

                var sqlCmd = @$"UPDATE Amigos SET Pendencia=@pendencia WHERE UsuarioId='{idUser}' and UsuarioAmigoId='{idFriend}';";

                using (var cmd = new SqlCommand(sqlCmd, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("pendencia", 0);
                    con.Open();
                    await cmd
                     .ExecuteScalarAsync()
                     .ConfigureAwait(false);

                }
            }
        }

        
    }
}

using InstaGama.Domain.Entities;
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

        public async Task<List<Friends>> GetFriendsByFriendPendingAsync(int userId)
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
										UsuarioAmigoId='{userId}' and Pendencia=1;";

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
        public async Task<Friends> GetFriendsByFriendIdAsync(int userId, int friendId)
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
										UsuarioAmigoId='{friendId}' and Pendencia=0 and UsuarioId='{userId}'";

                using (var cmd = new SqlCommand(sqlCmd, con))
                {
                    cmd.CommandType = CommandType.Text;
                    con.Open();

                    var reader = await cmd
                                        .ExecuteReaderAsync()
                                        .ConfigureAwait(false);

                  


                    while (reader.Read())
                    {

                        var friends = new Friends(int.Parse(reader["Id"].ToString()),
                                               int.Parse(reader["UsuarioId"].ToString()),
                                               int.Parse(reader["UsuarioAmigoId"].ToString()),
                                               int.Parse(reader["Pendencia"].ToString()));

                      
                        
                            return friends;
                       

                        
                    }

                    return default;
                }
            }
        }

        public async Task<Friends> GetFriendsByFriendIdPendingAsync(int userId, int friendId)
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
										UsuarioAmigoId='{userId}' and Pendencia=1 and UsuarioId='{friendId}'";

                using (var cmd = new SqlCommand(sqlCmd, con))
                {
                    cmd.CommandType = CommandType.Text;
                    con.Open();

                    var reader = await cmd
                                        .ExecuteReaderAsync()
                                        .ConfigureAwait(false);

               


                    while (reader.Read())
                    {

                        var friend = new Friends(int.Parse(reader["Id"].ToString()),
                                               int.Parse(reader["UsuarioId"].ToString()),
                                               int.Parse(reader["UsuarioAmigoId"].ToString()),
                                               int.Parse(reader["Pendencia"].ToString()));

                        return friend;
                    }

                    return default;
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

                var sqlCmd = @$"UPDATE Amigos SET Pendencia=@pendencia WHERE UsuarioId='{idFriend}' and UsuarioAmigoId='{idUser}';";

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

        public async Task DeleteAsync(int userId, int idFriend)
        {
            using (var con = new SqlConnection(_configuration["ConnectionString"]))
            {
                var sqlCmd = $@"DELETE 
                                FROM
                                Amigos
                                WHERE 
                                UsuarioAmigoId='{userId}' and UsuarioId='{idFriend}'";

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

        public async Task<List<User>> GetProfileAllFriends(int userId)
        {
            using (var con = new SqlConnection(_configuration["ConnectionString"]))
            {
                var sqlCmd = @$" SELECT u.Id,
                                        u.Nome,
                                        g.descricao,
                                        u.GeneroId,
                                        u.Email,
                                        u.DataNascimento,
                                        u.Foto
                                        FROM 
                                        Usuario u
                                        INNER JOIN Amigos a 
                                        on a.UsuarioAmigoId=u.Id
                                        Inner Join Genero g
                                        on u.GeneroId=g.Id
                                        where a.UsuarioId='{userId}';";

                using (var cmd = new SqlCommand(sqlCmd, con))
                {
                    cmd.CommandType = CommandType.Text;
                    con.Open();

                    var reader = await cmd
                                        .ExecuteReaderAsync()
                                        .ConfigureAwait(false);
                    var userFriend = new List<User>();

                    while (reader.Read())
                    {
                        var user = new User(int.Parse(reader["Id"].ToString()),
                                             reader["Email"].ToString(),
                                             reader["Nome"].ToString(),
                                             DateTime.Parse(reader["DataNascimento"].ToString()),
                                             new Gender(reader["Descricao"].ToString()),
                                             reader["Foto"].ToString());

                        user.SetId(int.Parse(reader["id"].ToString()));
                        user.Gender.SetId(int.Parse(reader["GeneroId"].ToString()));
                        userFriend.Add(user);

                        return userFriend;
                    }

                    return default;
                }
            }
        }

        public async Task<User> GetProfileFriendById(int idUser,int friendId)
        {

            using (var con = new SqlConnection(_configuration["ConnectionString"]))
            {
                var sqlCmd = @$"SELECT  u.Id,
                                        u.Nome,
                                        g.descricao,
                                        u.GeneroId,
                                        u.Email,
                                        u.DataNascimento,
                                        u.Foto
                                        FROM 
                                        Usuario u
                                        INNER JOIN Amigos a 
                                        on a.UsuarioAmigoId=u.Id
                                        Inner Join Genero g
                                        on u.GeneroId=g.Id
                                        where a.UsuarioId='{idUser}' and a.UsuarioAmigoId='{friendId}';";

                using (var cmd = new SqlCommand(sqlCmd, con))
                {
                    cmd.CommandType = CommandType.Text;
                    con.Open();

                    var reader = await cmd
                                        .ExecuteReaderAsync()
                                        .ConfigureAwait(false);
                  

                    while (reader.Read())
                    {
                        
                        var user = new User(int.Parse(reader["Id"].ToString()),
                                           reader["Email"].ToString(),
                                           reader["Nome"].ToString(),
                                           DateTime.Parse(reader["DataNascimento"].ToString()),
                                           new Gender(reader["Descricao"].ToString()),
                                           reader["Foto"].ToString());

                        user.SetId(int.Parse(reader["id"].ToString()));
                        user.Gender.SetId(int.Parse(reader["GeneroId"].ToString()));
        

                        return user;
                    }

                    return default;
                }
            }
        }
    }
}

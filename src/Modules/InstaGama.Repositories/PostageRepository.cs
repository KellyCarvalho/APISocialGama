using InstaGama.Domain.Core.Interfaces;
using InstaGama.Domain.Entities;
using InstaGama.Domain.Interfaces;
using Microsoft.Extensions.Configuration;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace InstaGama.Repositories
{
    public class PostageRepository : IPostageRepository
    {
        private readonly IConfiguration _configuration;
      

        public PostageRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

     

        public async Task<List<Postage>> GetPostageByUserIdAsync(int userId)
        {
            using (var con = new SqlConnection(_configuration["ConnectionString"]))
            {
                var sqlCmd = @$"SELECT Id,
	                                   UsuarioId,
                                       Texto,
                                        Foto,
                                       Criacao
                                FROM 
	                                Postagem
                                WHERE 
	                                UsuarioId= '{userId}'";

                using (var cmd = new SqlCommand(sqlCmd, con))
                {
                    cmd.CommandType = CommandType.Text;
                    con.Open();

                    var reader = await cmd
                                        .ExecuteReaderAsync()
                                        .ConfigureAwait(false);

                    var postagesForUser = new List<Postage>();

                    while (reader.Read())
                    {
                        var postage = new Postage(int.Parse(reader["Id"].ToString()),
                                                    reader["Texto"].ToString(),
                                                    reader["Foto"].ToString(),
                                                    int.Parse(reader["UsuarioId"].ToString()),
                                                    DateTime.Parse(reader["Criacao"].ToString()));

                        postagesForUser.Add(postage);
                    }

                    return postagesForUser;
                }
            }
        }

        public async Task<List<Postage>> GetPostageFriendAsync(int userId)
        {
            using (var con = new SqlConnection(_configuration["ConnectionString"]))
            {
                var sqlCmd = @$"SELECT  a.UsuarioId, 
                                        p.Id,
                                        p.Foto,
                                        p.Texto,
                                        p.Criacao
                                        FROM 
                                        Amigos b
                                        INNER JOIN Amigos a 
                                        on a.UsuarioAmigoId=b.UsuarioId
                                        INNER JOIN Postagem p
                                        on p.UsuarioId=b.UsuarioAmigoId
                                        where b.UsuarioId='{userId}' 
                                        order by  p.Criacao desc;";

                using (var cmd = new SqlCommand(sqlCmd, con))
                {
                    cmd.CommandType = CommandType.Text;
                    con.Open();

                    var reader = await cmd
                                        .ExecuteReaderAsync()
                                        .ConfigureAwait(false);

                    var postagesForUser = new List<Postage>();

                    while (reader.Read())
                    {
                        var postage = new Postage(int.Parse(reader["Id"].ToString()),
                                                    reader["Texto"].ToString(),
                                                    reader["Foto"].ToString(),
                                                    int.Parse(reader["UsuarioId"].ToString()),
                                                    DateTime.Parse(reader["Criacao"].ToString()));

                        postagesForUser.Add(postage);
                    }

                    return postagesForUser;
                }
            }
        }




        public async Task<int> InsertAsync(Postage postage)
        {
            using (var con = new SqlConnection(_configuration["ConnectionString"]))
            {
                var sqlCmd = @"INSERT INTO
                                      Postagem (
		                              UsuarioId,
                                      Texto,
                                      Foto,
		                              Criacao)
                                      VALUES (
			                         @usuarioId,
                                     @texto,
                                     @foto,
		                             @criacao);
		                             SELECT scope_identity();";

                using (var cmd = new SqlCommand(sqlCmd, con))
                {
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("usuarioId", postage.UserId);
                    cmd.Parameters.AddWithValue("texto", postage.Text);
                    cmd.Parameters.AddWithValue("foto", postage.Photo);
                    cmd.Parameters.AddWithValue("criacao", postage.Created);

                    con.Open();
                    var id = await cmd
                                    .ExecuteScalarAsync()
                                    .ConfigureAwait(false);

                    return int.Parse(id.ToString());
                }
            }
        }
    }
}

﻿using InstaGama.Domain.Entities;
using InstaGama.Domain.Interfaces;
using System;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace InstaGama.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration _configuration;

        public UserRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<User> GetByIdAsync(int id)
        {
            using (var con = new SqlConnection(_configuration["ConnectionString"]))
            {
                var sqlCmd = @$"SELECT u.Id,
	                                 u.Nome,
	                                 u.Email,
	                                 u.Senha,
                                     u.DataNascimento,
                                     u.Foto,
	                                 g.Id as GeneroId,
	                                 g.Descricao
                                FROM 
	                                Usuario u
                                INNER JOIN 
	                                Genero g ON g.Id = u.GeneroId
                                WHERE 
	                                u.Id= '{id}'";

                using (var cmd = new SqlCommand(sqlCmd, con))
                {
                    cmd.CommandType = CommandType.Text;
                    con.Open();

                    var reader = await cmd
                                        .ExecuteReaderAsync()
                                        .ConfigureAwait(false);

                    while (reader.Read())
                    {
                        var user = new User(reader["Email"].ToString(),
                                            reader["Senha"].ToString(),
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

        public async Task<List<User>> GetAllUsersAsync()
        {
            using (var con = new SqlConnection(_configuration["ConnectionString"]))
            {
                var sqlCmd = @$"SELECT u.Id,
	                                 u.Nome,
	                                 u.Email,
	                                 u.Senha,
                                     u.DataNascimento,
                                     u.Foto,
	                                 g.Id as GeneroId,
	                                 g.Descricao
                                FROM 
	                                Usuario u
                                INNER JOIN 
	                                Genero g ON g.Id = u.GeneroId;";
                var allUsers = new List<User>();

                using (var cmd = new SqlCommand(sqlCmd, con))
                {
                    cmd.CommandType = CommandType.Text;
                    con.Open();

                    var reader = await cmd
                                        .ExecuteReaderAsync()
                                        .ConfigureAwait(false);

                    while (reader.Read())
                    {
                        var user = new User(reader["Email"].ToString(),
                                            reader["Senha"].ToString(),
                                            reader["Nome"].ToString(),
                                            DateTime.Parse(reader["DataNascimento"].ToString()),
                                            new Gender(reader["Descricao"].ToString()),
                                            reader["Foto"].ToString());

                        user.SetId(int.Parse(reader["id"].ToString()));
                        user.Gender.SetId(int.Parse(reader["GeneroId"].ToString()));

                        allUsers.Add(user);

                    
                    }

                    return allUsers;
                }
            }
        }



        public async Task<User> GetByLoginAsync(string login)
        {
            using (var con = new SqlConnection(_configuration["ConnectionString"]))
            {
                var sqlCmd = @$"SELECT u.Id,
	                                 u.Nome,
	                                 u.Email,
	                                 u.Senha,
                                     u.DataNascimento,
                                     u.Foto,
	                                 g.Id as GeneroId,
	                                 g.Descricao
                                FROM 
	                                Usuario u
                                INNER JOIN 
	                                Genero g ON g.Id = u.GeneroId
                                WHERE 
	                                u.Email= '{login}'";

                using (var cmd = new SqlCommand(sqlCmd, con))
                {
                    cmd.CommandType = CommandType.Text;
                    con.Open();

                    var reader = await cmd
                                        .ExecuteReaderAsync()
                                        .ConfigureAwait(false);

                    while(reader.Read())
                    {
                        var user = new User(reader["Nome"].ToString(),
                                            DateTime.Parse(reader["DataNascimento"].ToString()),
                                            new Gender(reader["Descricao"].ToString()),
                                            reader["Foto"].ToString());

                        user.InformationLoginUser(reader["Email"].ToString(), reader["Senha"].ToString());
                        user.SetId(int.Parse(reader["id"].ToString()));
                        user.Gender.SetId(int.Parse(reader["GeneroId"].ToString()));

                        return user;
                    }

                    return default;
                }
            }
        }

     
        public async Task<int> InsertAsync(User user)
        {
            using (var con = new SqlConnection(_configuration["ConnectionString"]))
            {
                var sqlCmd = @"INSERT INTO
                                Usuario (GeneroId,
                                           Nome,
                                           Email,
                                           Senha,
                                           DataNascimento,
                                           Foto)
                                VALUES (@generoId,
                                        @nome,
                                        @email,
                                        @senha,
                                        @dataNascimento,
                                        @foto); SELECT scope_identity();";

                using (var cmd = new SqlCommand(sqlCmd, con))
                {
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("generoId", user.Gender.Id);
                    cmd.Parameters.AddWithValue("nome", user.Name);
                    cmd.Parameters.AddWithValue("email", user.Email);
                    cmd.Parameters.AddWithValue("senha", user.Password);
                    cmd.Parameters.AddWithValue("dataNascimento", user.Birthday);
                    cmd.Parameters.AddWithValue("foto", user.Photo);

                    con.Open();
                    var id = await cmd
                                    .ExecuteScalarAsync()
                                    .ConfigureAwait(false);

                    return int.Parse(id.ToString());
                }
            }
        }

        public async Task<List<string>> GetPhotosUserAsync(int userId)
        {
            using (var con = new SqlConnection(_configuration["ConnectionString"]))
            {
                var sqlCmd = @$"SELECT p.Foto
                                FROM Postagem p
                                INNER JOIN Usuario u ON u.Id = p.UsuarioId
                                WHERE u.Id='{userId}';";

                using (var cmd = new SqlCommand(sqlCmd, con))
                {
                    cmd.CommandType = CommandType.Text;
                    con.Open();

                    var reader = await cmd
                                        .ExecuteReaderAsync()
                                        .ConfigureAwait(false);

                    var photosForUser = new List<string>();

                    while (reader.Read())
                    {
                        var photos = reader["Foto"].ToString();
                        if (!string.IsNullOrEmpty(photos)) {
                            photosForUser.Add(photos);
                        }
                        
                    }

                    return photosForUser;
                }
            }

        }



        public async Task UpdateAsync(User user, int idUser)
        {
            using (var con = new SqlConnection(_configuration["ConnectionString"]))
            {

                var sqlCmd = @$"UPDATE Usuario SET Nome=@nome,GeneroId=@generoId, Email=@email, Senha=@senha,DataNascimento=@dataNascimento, Foto=@foto   WHERE Id='{idUser}'";

                using (var cmd = new SqlCommand(sqlCmd, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("nome", user.Name);
                    cmd.Parameters.AddWithValue("generoId", user.Gender.Id);
                    cmd.Parameters.AddWithValue("email", user.Email);
                    cmd.Parameters.AddWithValue("senha", user.Password);
                    cmd.Parameters.AddWithValue("dataNascimento", user.Birthday);
                    cmd.Parameters.AddWithValue("foto", user.Photo);
                    con.Open();

                                       await cmd
                                      .ExecuteScalarAsync()
                                      .ConfigureAwait(false);

                 



                }
            }
        }

        public async Task DeleteAsync(int idUser)
        {
            using (var con = new SqlConnection(_configuration["ConnectionString"]))
            {
                var sqlCmd = $@"DELETE 
                                FROM
                                Usuario
                               WHERE 
                                Id={idUser}";

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

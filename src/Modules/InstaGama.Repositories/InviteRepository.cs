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
    public class InviteRepository : IInviteRepository
    {
        private readonly IConfiguration _configuration;

        public InviteRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Invite> GetByIdAsync(int id)
        {
            using(var con = new SqlConnection(_configuration["ConnectionString"]))
            {
                var sqlCmd = @$" Id,
                                 IdUsuario,
                                 IdusuarioConvidado                                
                                 Mensagem,
                                 Status_Convite,
                                 From Convite
                                 Where 
                                 Id='{id}'";

                using(var cmd = new SqlCommand(sqlCmd, con))
                {
                    cmd.CommandType = CommandType.Text;
                    con.Open();

                    var reader = await cmd
                                         .ExecuteReaderAsync()
                                         .ConfigureAwait(false);
                    while (reader.Read())
                    {
                        var invite = new Invite(int.Parse(reader["IdUsuario"].ToString()),
                                                int.Parse(reader["IdusuarioConvidado"].ToString()),
                                                reader["Mensagem"].ToString(),
                                               int.Parse(reader["Status_Convite"].ToString()));

                        invite.SetId(int.Parse(reader["Id"].ToString()));

                        return invite;
                    }
                    return default;
                }

            }


           
        }

        public async Task<Invite> GetByUserAsync(int idUser)
        {
            using (var con = new SqlConnection(_configuration["ConnectionString"]))
            {
                var sqlCmd = @$" Id,
                                 IdUsuario,
                                 IdusuarioConvidado                                
                                 Mensagem,
                                 Status_Convite,
                                 From Convite
                                 Where 
                                 IdUsuario='{idUser}'";

                using (var cmd = new SqlCommand(sqlCmd, con))
                {
                    cmd.CommandType = CommandType.Text;
                    con.Open();

                    var reader = await cmd
                                         .ExecuteReaderAsync()
                                         .ConfigureAwait(false);
                    while (reader.Read())
                    {
                        var invite = new Invite(int.Parse(reader["IdUsuario"].ToString()),
                                                int.Parse(reader["IdusuarioConvidado"].ToString()),
                                                reader["Mensagem"].ToString(),
                                               int.Parse(reader["Status_Convite"].ToString()));

                        invite.SetId(int.Parse(reader["Id"].ToString()));

                        return invite;
                    }
                    return default;
                }

            }


        }

        public async Task<int> InsertAsync(Invite invite)
        {
            using(var con = new SqlConnection(_configuration["ConnectionString"]))
            {
                var sqlCmd = @"INSERT INTO 
                                     Convite (IdUsuario,
                                              IdUsuarioConvidado,
                                              Mensagem,
                                              Status_Convite) 
                                              VALUES (@idUsuario,
                                                      @idUsuarioConvidado,
                                                      @mensagem,
                                                      @status_convite); SELECT scope_identity();";

                using(var cmd = new SqlCommand(sqlCmd, con))
                {
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("idUsuario", invite.IdUser);
                    cmd.Parameters.AddWithValue("IdUsuarioConvidado", invite.IdUserInvite);
                    cmd.Parameters.AddWithValue("Mensagem", invite.Message);
                    cmd.Parameters.AddWithValue("Status_Convite", invite.Status);

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

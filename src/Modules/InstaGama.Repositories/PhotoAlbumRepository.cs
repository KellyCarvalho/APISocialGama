using InstaGama.Domain.Entities;
using InstaGama.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace InstaGama.Repositories
{
    public class PhotoAlbumRepository : IPhotoAlbumRepository
    {
        private readonly IConfiguration _configuration;

        public PhotoAlbumRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task<PhotoAlbum> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PhotoAlbum> GetByPostageAsync(int idPostage)
        {
            throw new NotImplementedException();
        }

        public Task<PhotoAlbum> GetByUserAsync(int idUser)
        {
            throw new NotImplementedException();
        }

        public Task<int> InsertAsync(PhotoAlbum photoAlbum)
        {
            throw new NotImplementedException();
        }
    }  




}

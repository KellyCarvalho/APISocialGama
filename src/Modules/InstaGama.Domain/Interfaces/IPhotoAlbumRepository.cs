using InstaGama.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InstaGama.Domain.Interfaces
{
  public interface IPhotoAlbumRepository
    {
        Task<int> InsertAsync(PhotoAlbum photoAlbum);
        Task<PhotoAlbum> GetByUserAsync(int idUser);
        Task<PhotoAlbum> GetByPostageAsync(int idPostage);
        Task<PhotoAlbum> GetByIdAsync(int id);
    }
}

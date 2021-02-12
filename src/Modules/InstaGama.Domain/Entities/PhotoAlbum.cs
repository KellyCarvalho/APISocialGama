using System;
using System.Collections.Generic;
using System.Text;

namespace InstaGama.Domain.Entities
{
   public class PhotoAlbum
    {
        public PhotoAlbum(int idUser, int idPostge)
        {
            IdUser = idUser;
            IdPostge = idPostge;
        }

        public int Id { get; private set; }
        public int IdUser { get; private set; }
        public int IdPostge { get; private set; }

        public void SetId(int id)
        {
            Id = id;
        }
    }
}

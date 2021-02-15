using InstaGama.Domain.Entities.ValueObject;
using System;

namespace InstaGama.Domain.Entities
{
    public class Postage
    {
        public Postage(string text,
                        int userId)
        {
            Text = text;
            UserId = userId;

            Created = DateTime.Now;
        }

        public Postage( string text, int userId, string photo)
        {
            
            Text = text;
            UserId = userId;
            Photo = photo;
            Created = DateTime.Now;
        }



        public Postage(int id,
                        string text,
                        int userId,
                        DateTime created)
        {
            Id = id;
            Text = text;
            UserId = userId;
            Created = created;
        }
        public Postage(int id,
                       string text,
                       string photo,
                       int userId,
                       DateTime created)
        {
            Id = id;
            Text = text;
            Photo = photo;
            UserId = userId;
            Created = created;
        }



        public int Id { get; private set; }
        public int UserId { get; private set; }
        public string Text { get; private set; }
        public string Photo { get; private set; }
        public DateTime Created { get; private set; }

        public void SetId(int id)
        {
            Id = id;
        }

        public bool IsValid()
        {
            if(string.IsNullOrEmpty(UserId.ToString())||
                string.IsNullOrEmpty(Text)){
                return false;
            }
            return true;

        }

    
    }
}

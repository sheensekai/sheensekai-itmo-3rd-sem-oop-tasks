using System;
using System.Collections.Generic;
using System.Text;

namespace MyLabs.Lab2
{
    class SearchQuery
    {
        Target target;

        public readonly string artist;
        public readonly string song;
        public readonly string collection;
        public readonly int year;
        public readonly Genre genre;

        public SearchQuery()
        {
            Console.WriteLine("Enter artist: ");
            artist = Console.ReadLine();
            if (string.IsNullOrEmpty(artist))
            {
                artist = null;
            }

            Console.WriteLine("Enter songname: ");
            song = Console.ReadLine();
            if (string.IsNullOrEmpty(song))
            {
                song = null;
            }

            Console.WriteLine("Enter collection: ");
            collection = Console.ReadLine();
            if (string.IsNullOrEmpty(collection))
            {
                collection = null;
            }

            Console.WriteLine("Enter year: ");
            string buffer = Console.ReadLine();
            if (string.IsNullOrEmpty(buffer))
            {
                year = -1;
            }
            else
            {
                year = Int32.Parse(buffer);
            }

            Console.WriteLine("Enter genre: ");
            buffer = Console.ReadLine();
            if (string.IsNullOrEmpty(buffer))
            {
                genre = null;
            }
            else
            {
                genre = Genre.FindGenre(buffer);    
            }

        }


        public List<ICollection> list;

        public List<ICollection> result
        {
            set { list = value; }
            get { return list; }
        }
        enum Target {
            Song,
            Artist,
            Collection
        }
    }
}

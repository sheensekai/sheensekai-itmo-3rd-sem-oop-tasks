using System;
using System.Collections.Generic;
using System.Text;

namespace MyLabs.Lab2
{
    class Genre
    {
        static Genre root = new Genre(10);
        public readonly string name;
        public readonly Genre parent;
        public readonly List<Genre> subgenres;

        static public Genre FindGenre(string name)
        {
           Genre genre = FindGenreVisit(name, root);
            if (genre == null)
            {
                throw new GenreException("There's no such genre");
            }
            return genre;
        }
        
        static public void AddGenre(string name, string parent_name = null)
        {
            Genre genre;
            if (object.ReferenceEquals(parent_name, null))
            {
                genre = new Genre(name);
            }
            else
            {
                Genre parent = Genre.FindGenre(parent_name);
                genre = new Genre(name, parent);
            }
        }

        public Genre(string name)
            :this(name, root)
        { }

        public Genre(string name, Genre parent)
        {
            Genre genre = FindGenreVisit(name, root);
            if (genre != null)
            {
                throw new GenreException("There's already such genre");
            }

            this.name = name;
            parent.subgenres.Add(this);
            this.parent = this;
            this.subgenres = new List<Genre>();
        }

        private Genre(int val)
        {
            this.name = "any";
            this.parent = null;
            this.subgenres = new List<Genre>();
        }

        static private Genre FindGenreVisit(string name, Genre parent)
        {
            foreach (var genre in parent.subgenres)
            {
                if (genre.name == name)
                {
                    return genre;
                }

                Genre tmp = FindGenreVisit(name, genre);
                if (tmp != null)
                {
                    return tmp;
                }
            }

            return null;
        }

        public bool IsSubGenre(Genre other)
        {
            Genre tmp = this.parent;
            
            while(tmp != null)
            {
                if (tmp == other)
                {
                    return true;
                }
                else
                {
                    tmp = tmp.parent;
                }
            }

            return false;
        }

    }
}

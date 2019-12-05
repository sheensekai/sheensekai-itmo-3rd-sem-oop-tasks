using System;
using System.Collections.Generic;
using System.Text;

namespace MyLabs.Lab2
{
    class Artist
    {
        private readonly Genre main_genre;
        private readonly string name;
        private readonly List<ICollection> own_collections;
        private readonly List<ICollection> feat_collections;


        public Artist(string name, Genre genre)
        {
            this.name = name;
            main_genre = genre;
            own_collections = new List<ICollection>();
            feat_collections = new List<ICollection>();
        }

        public Album AddAlbum(string name, int year, Genre genre = null)
        {
            foreach(ICollection collection in own_collections)
            {
                if(collection.GetName() == name)
                {
                    throw new CollectionException("The artist already has an album/single called the same");
                }
            }

            if (genre == null)
            {
                genre = this.main_genre;
            }

            Album tmp = new Album(this, name, genre, year);
            own_collections.Add(tmp);

            return tmp;
        }
        
        public Single AddSingle(string name, int year, Genre genre = null, List<Artist> artists = null)
        {
            foreach (ICollection collection in own_collections)
            {
                if (collection.GetName() == name)
                {
                    throw new CollectionException("The artist already has an album/single called the same");
                }
            }

            if (genre == null)
            {
                genre = this.main_genre;
            }

            if (artists == null)
            {
                artists = new List<Artist>();
                artists.Add(this);
            }

            Single tmp = new Single(artists, this, name, genre, year);
            foreach(Artist artist in artists)
            {
                if (artist != this)
                {
                    AddAsFeat(tmp);
                }
            }

            own_collections.Add(tmp);

            return tmp;
        }


        public void AddAsFeat(ICollection collection)
        {
            foreach(ICollection tmp in feat_collections)
            {
                if (tmp == collection)
                {
                    throw new CollectionException("The artist already has an album/single called the same as feat");
                }
            }
            feat_collections.Add(collection);
        }

        public string artist_name
        {
            get { return name;  }
        }

        public Genre artist_genre
        {
            get { return main_genre; }
        }

        public List<ICollection> artist_own_collections
        {
            get { return own_collections;  }
        }

        public List<ICollection> artist_feat_collections
        {
            get { return feat_collections; }
        }

        public string ToString()
            => "Artist " + name + " ";

    }
}

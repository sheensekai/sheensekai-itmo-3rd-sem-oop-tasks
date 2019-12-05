using System;
using System.Collections.Generic;
using System.Text;

namespace MyLabs.Lab2
{
    class Collection : SongList
    {
        public string name;
        public Genre genre;
        public int year;
        public List<Artist> artists;

        public Collection(string name, Genre genre, int year)
        {
            this.name = name;
            this.genre = genre;
            this.year = year;
            this.artists = new List<Artist>();
        }

        public Collection(string name, Genre genre, int year, SongList list)
            :   base(list)
        {
            this.name = name;
            this.genre = genre;
            this.year = year;
            this.artists = new List<Artist>();
        }

        public override string GetName()
        {
            return name;
        }

        public Song AddSong(Artist artist, string name, Genre genre = null, List<Artist> artists = null)
        {
            List<Song> tmp_list = FindBySongName(name).ToList();

            if (tmp_list.Count > 0)
            {
                throw new SongException("There is a song called the same in the collection");
            }

            if (artists == null)
            {
                if (genre == null)
                {
                    genre = artist.artist_genre;
                }

                artists = new List<Artist>();
                artists.Add(artist);
            }
            else if (!artists.Contains(artist))
            {
                artists.Add(artist);
            }

            Song song = new Song(artists, artist, this, name, genre, year);
            base.AddSong(song);

            foreach(Artist tmp in artists)
            {
                if (!tmp.artist_feat_collections.Contains(this) && !tmp.artist_own_collections.Contains(this))
                {
                    tmp.AddAsFeat(this);
                }
                AddArtist(tmp);
            }

            return song;
        }

        private Artist AddArtist(Artist artist)
        {
            if (artists.Contains(artist))
            {
                return null;
            }
            else
            {
                this.artists.Add(artist);
                return artist;
            }
        }

        public override ICollection FindByYear(int year)
        {
            if (this.year == year)
            {
                return this;
            }
            else
            {
                 return base.FindByYear(year);
            }
        }

        public override ICollection FindByCollection(string name)
        {
            if (this.name.Contains(name))
            {
                return this;
            }

            else
            {
                return null;
            }
        }

        public static Collection MakeCollection(Catalog catalog, string name, Genre genre, int year, SongList list)
        {
            Collection collection = new Collection(name, genre, year, list);
            catalog.AddCollection(collection);
            return collection;
        }
        public override string ToString()
        {
            string result = "Collection " + name + " by ";
            foreach(Artist artist in artists)
            {
                result += artist.ToString();
            }
            result += "\n";
            result += base.ToString();
            return result;
        }

    }
}

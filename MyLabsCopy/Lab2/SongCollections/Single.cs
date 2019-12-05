using System;
using System.Collections.Generic;
using System.Text;

namespace MyLabs.Lab2
{
    class Single : ICollection
    {
        List<Artist> artists;
        Artist artist;
        string name;
        Genre genre;
        int year;
        Song song;

        public Single(List<Artist> artists, Artist artist, string name, Genre genre, int year)
        {
            this.artists = artists;
            this.artist = artist;
            this.name = name;
            this.genre = genre;
            this.year = year;

            this.song = new Song(artists, artist, this, name, genre, year);
        }

        public string GetName()
        {
            return song.song_name;
        }

        public ICollection FindByYear(int year)
        {
            if (song.song_year == year)
            {
                return this;
            }
            else
            {
                return null;
            }
        }

        public ICollection FindByArtist(string name)
        {
            if (song.song_artist.artist_name == name)
            {
                return this;
            }
            else
            {
                return null;
            }
        }

        public ICollection FindBySongName(string name)
        {
            if (song.song_name == name)
            {
                return this;
            }
            else
            {
                return null;
            }
        }

        public ICollection FindByCollection(string name)
        {
            return FindBySongName(name);
        }

        public ICollection FindByGenre(Genre genre)
        {
            if (song.song_genre.IsSubGenre(genre))
            {
                return this;
            }
            else 
            {
                return null;  
            }

        }

        public List<Song> ToList()
        {
            List<Song> tmp = new List<Song>();
            tmp.Add(song);
            return tmp;
        }

        public override string ToString()
        {
            string result = "Single By " + artist.ToString() + "\n";
            result += song.ToString();
            return result;
        }

    }
}

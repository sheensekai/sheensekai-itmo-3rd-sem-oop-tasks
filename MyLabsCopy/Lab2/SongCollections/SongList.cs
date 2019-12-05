using System;
using System.Collections.Generic;
using System.Text;

namespace MyLabs.Lab2
{
    class SongList : ICollection
    {
        public virtual ICollection FindByYear(int year)
        {
            SongList result = new SongList();

            foreach (var song in songs)
            {
                if (song.song_year == year)
                {
                    result.songs.Add(song);
                }
            }

            if (result.ToList().Count < 1)
            {
                return null;
            }

            return result;
        }

        public virtual ICollection FindBySongName(string name)
        {
            SongList result = new SongList();

            foreach (var song in songs)
            {
                if (song.song_name.Contains(name))
                {
                    result.songs.Add(song);
                }
            }

            return result;
        }

        public virtual ICollection FindByArtist(string name)
        {
            SongList result = new SongList();

            foreach (var song in songs)
            {

                foreach (var artist in song.song_artists)
                {
                    if (artist.artist_name.Contains(name))
                    {
                        result.songs.Add(song);
                    }
                }
            }

            return result;
        }

        public virtual ICollection FindByCollection(string name)
        {
            return null;
        }

        public virtual ICollection FindByGenre(Genre genre)
        {
            SongList result = new SongList();

            foreach (var song in songs)
            {
                if (song.song_genre.IsSubGenre(genre))
                {
                    result.songs.Add(song);
                }

            }

            return result;
        }

        public SongList()
        {
            songs = new List<Song>();
        }

        public SongList(SongList other)
        {
            this.songs = other.songs;
        }

        public void AddSong(Song song)
        {

            songs.Add(song);
        }

        public Song AddSong(Artist artist, string name, int year, Genre genre = null, List<Artist> artists = null)
        {
            List<Song> tmp_list = FindBySongName(name).ToList();

            if (tmp_list.Count > 0)
            {
                throw new SongException("There is a song called the same in the collection");
            }
            if (genre == null)
            {
                genre = artist.artist_genre;
            }
            Song song = new Song(artists, artist, this, name, genre, year);

            songs.Add(song);
            return song;
        }

        public virtual string GetName()
        {
            return null;
        }

        public override string ToString()
        {
            string result = "List of songs\n";
            foreach (var song in songs)
            {
                result += song.ToString();
            }
            return result;
        }

        public List<Song> ToList()
        {
            return songs;
        }

        public List<Song> songs;
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace MyLabs.Lab2
{
    class Album : Collection
    {
        Artist main_artist;

        public Album(Artist main_artist, string name, Genre genre, int year)
            : base(name, genre, year)
        {
            this.main_artist = main_artist;
        }


        public Song AddSong(List<Artist> artists, string name, Genre genre = null)
        {
            Song song;
            artists.Add(main_artist);
            if (genre == null) {
                song = base.AddSong(main_artist, name, main_artist.artist_genre, artists);
            }
            else
            {
                song = base.AddSong(main_artist, name, genre, artists);
            }
            
            foreach(Artist artist in artists)
            {
                if (artist != main_artist)
                {
                    artist.AddAsFeat(this);
                }
            }

            return song;
        }

        public Song AddSong(string name, Genre genre = null)
        {
            List<Artist> list = new List<Artist>();
            list.Add(main_artist);
            
            return AddSong(list, name, genre);
        }

        public override ICollection FindByArtist(string name)
        {
            if (main_artist.artist_name == name)
            {
                return this;
            }

            else
            {
                return base.FindByArtist(name);
            }
        }

        public override string ToString()
        {
            string result = "Album By " + main_artist.ToString() + "\n";
            result += base.ToString() + "\n";
            return result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace MyLabs.Lab2
{
    class Song
    {

        public Song(List<Artist> artists, Artist artist, ICollection collection, string name, Genre genre, int year)
        {
            this.artist = artist;
            this.collection = collection;
            this.name = name;
            this.genre = genre;
            this.year = year;
            this.artists = artists;
        }

        private readonly Artist artist;
        private readonly List<Artist> artists;
        private readonly ICollection collection;
        private readonly string name;
        private readonly Genre genre;
        private readonly int year;

        public string song_name
        {
            get { return name;  }
        }

        public int song_year
        {
            get { return year;  }
        }

        public Genre song_genre
        {
            get { return genre;  }
        }

        public Artist song_artist
        {
            get { return artist;  }
        }

        public List<Artist> song_artists
        {
            get { return artists;  }
        }
        public override string ToString()
            => "Song " + name + " by " + artist.ToString() + "\n";
    }
}

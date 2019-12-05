using System;
using System.Collections.Generic;
using System.Text;

namespace MyLabs.Lab2
{
    class Catalog
    {
        public Dictionary<string, SongList> songs_by_name;
        public Dictionary<int, SongList> songs_by_year;
        public Dictionary<Genre, SongList> songs_by_genre;
        public Dictionary<string, Artist> artists;
        public Dictionary<string, List<ICollection>> collections;

        public Catalog()
        {
            this.songs_by_name = new Dictionary<string, SongList>();
            this.songs_by_year = new Dictionary<int, SongList>();
            this.songs_by_genre = new Dictionary<Genre, SongList>();
            this.artists = new Dictionary<string, Artist>();
            this.collections = new Dictionary<string, List<ICollection>>();

        }
        
        private List<ICollection> FindImpl(SearchQuery query)
        {
            CollectionFinder.FindByArtist(this, query);
            CollectionFinder.FindByCollection(this, query);
            CollectionFinder.FindBySongName(this, query);
            CollectionFinder.FindByYear(this, query);
            CollectionFinder.FindByGenre(this, query);

            return query.result;
        }

        public void Find()
        {
            SearchQuery query = new SearchQuery();
            List<ICollection> result = FindImpl(query);

            if (result == null)
            {
                Console.WriteLine("Sorry, didn't find anything");
                return;
            }
            foreach(ICollection collection in result)
            {
                string buffer = collection.ToString();
                Console.Write(buffer);
            }
        }

        private void AddSongByName(Song song)
        {
            SongList tmp = new SongList();
            songs_by_name.TryGetValue(song.song_name, out tmp);
            if (object.ReferenceEquals(tmp, null))
            {
                tmp = new SongList();
                tmp.AddSong(song);
                songs_by_name.Add(song.song_name, tmp);
            }
            else
            {
                tmp.AddSong(song);
            }
        }

        private void AddSongByYear(Song song)
        {
            SongList tmp = new SongList();
            songs_by_year.TryGetValue(song.song_year, out tmp);
            if (object.ReferenceEquals(tmp, null))
            {
                tmp = new SongList();
                tmp.AddSong(song);
                songs_by_year.Add(song.song_year, tmp);
            }
            else
            {
                tmp.AddSong(song);
            }
        }

        private void AddSongByGenre(Song song)
        {
            SongList tmp;
            songs_by_genre.TryGetValue(song.song_genre, out tmp);
            if (object.ReferenceEquals(tmp, null))
            {
                tmp = new SongList();
                tmp.AddSong(song);
                songs_by_genre.Add(song.song_genre, tmp);
            }
            else
            {
                tmp.AddSong(song);
            }
        }

        public void AddArtist(Artist artist)
        {
            Artist tmp;
            artists.TryGetValue(artist.artist_name, out tmp);
            if (!object.ReferenceEquals(tmp, null))
            {
                throw new ArtistException("The artist is already in the catalog");
            }

            artists.Add(artist.artist_name, artist);


                foreach (ICollection collection in artist.artist_own_collections)
                {
                    AddCollection(collection);
                }
            
        }

        public void AddCollection(ICollection collection)
        {
            if (collection.GetName() == null)
            {
                return;
            }

            List<ICollection> tmp;
            collections.TryGetValue(collection.GetName(), out tmp);

            if (object.ReferenceEquals(tmp, null))
            {
                tmp = new List<ICollection>();
                tmp.Add(collection);
                collections.Add(collection.GetName(), tmp);
            }
            else
            {
                foreach(ICollection tmp_collection in tmp)
                {
                    if (tmp_collection == collection)
                    {
                        throw new CollectionException("The collection is already in the catalog");
                        return;
                    }
                }

                tmp.Add(collection);

                
            }
            foreach (Song song in collection.ToList())
            {
                AddSong(song);
            }

        }

        public Artist FindArtist(string name)
        {
            Artist tmp;
            artists.TryGetValue(name, out tmp);
            return tmp;
        }

        private void AddSong(Song song)
        {
            SongList list;
            songs_by_name.TryGetValue(song.song_name, out list);

            if (list != null)
            {
                foreach (Song tmp in list.ToList())
                {
                    if (tmp == song)
                    {
                        throw new SongException("The song is already in the catalog");
                        return;
                    }
                }
            }
            AddSongByGenre(song);
            AddSongByName(song);
            AddSongByYear(song);
        }

        
    }
}

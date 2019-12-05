using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyLabs.Lab2
{
    class CollectionFinder
    {

        public static void FindByArtist(Catalog catalog, SearchQuery query)
        {
            if (query.artist == null)
            {
                return;
            }

            Artist tmp = null;
            List<ICollection> result = new List<ICollection>();
            foreach (var artist in catalog.artists.Values.Where(x => x.artist_name.Contains(query.artist)))
            {
                result.AddRange(artist.artist_own_collections);
                result.AddRange(artist.artist_feat_collections);
            }
            query.result = result;


        }

        public static void FindByCollection(Catalog catalog, SearchQuery query)
        {
            if (query.collection == null)
            {
                return;
            }

            List<ICollection> collections = new List<ICollection>();

            if (query.result == null)
            {
                catalog.collections.TryGetValue(query.collection, out collections);
            }
            else
            {
                foreach(var collection in query.result)
                {
                    ICollection tmp = collection.FindByCollection(query.collection);
                    if (tmp != null)
                    {
                        collections.Add(tmp);
                    }
                }

            }

            query.result = collections;
        }

        public static void FindBySongName(Catalog catalog, SearchQuery query)
        {
            if (query.song == null)
            {
                return;
            }

            List<ICollection> collections = new List<ICollection>();

            if (query.result == null)
            {

                var song_names = catalog.songs_by_name.Keys.Where(x => x.Contains(query.song));

                foreach(var song_name in song_names)
                {
                    catalog.songs_by_name.TryGetValue(song_name, out SongList list);
                    SongList col = (SongList)list.FindBySongName(query.song);
                    if (col != null && col.ToList().Count > 0)
                    {
                        collections.Add(col);
                    }
                  
                }

            }
            else
            {
                foreach (var collection in query.result)
                {
                    ICollection tmp = collection.FindBySongName(query.song);
                    if (tmp != null && tmp.ToList().Count > 0)
                    {
                        collections.Add(tmp);
                    }
                }
            }

            query.result = collections;
            if (query.result.Count == 0)
            {
                query.result = null;
            }
        }


        public static void FindByYear(Catalog catalog, SearchQuery query)
        {
            if (query.year == -1)
            {
                return;
            }

            List<ICollection> collections;

            if (query.result == null)
            {
                SongList songs;
                catalog.songs_by_year.TryGetValue(query.year, out songs) ;
                collections = new List<ICollection>();
                collections.Add(songs);
            }
            else
            {
                collections = new List<ICollection>();

                foreach (var collection in query.result)
                {
                    ICollection tmp = collection.FindByYear(query.year);
                    if (tmp != null)
                    {
                        collections.Add(tmp);
                    }
                }
            }

            query.result = collections;
        }

        public static void FindByGenre(Catalog catalog, SearchQuery query)
        {
            if (query.genre == null)
            {
                return;
            }

            List<ICollection> collections;

            if (query.result == null)
            {
                SongList songs;
                catalog.songs_by_genre.TryGetValue(query.genre, out songs);
                collections = new List<ICollection>();
                collections.Add(songs);
            }
            else
            {
                collections = new List<ICollection>();

                foreach (var collection in query.result)
                {
                    ICollection tmp = collection.FindByGenre(query.genre);
                    if (tmp != null)
                    {
                        collections.Add(tmp);
                    }
                }
            }

            query.result = collections;
        }

    }
}

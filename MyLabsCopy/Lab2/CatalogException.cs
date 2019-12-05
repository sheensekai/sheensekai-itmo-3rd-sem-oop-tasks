using System;
using System.Collections.Generic;
using System.Text;

namespace MyLabs.Lab2
{
    class CatalogException : Exception
    {
        public CatalogException()
        : base()
        { }
        public CatalogException(string message)
            :base(message)
        {}
    }

    class ArtistException : CatalogException
    {
        public ArtistException()
            : base()
        { }
        public ArtistException(string message)
            : base(message)
        {}
    }

    class AlbumException : CatalogException
    {
        public AlbumException()
            : base()
        { }
        public AlbumException(string message)
            : base(message)
        {}
    }

    class SongException : CatalogException
    {
        public SongException()
            : base()
        { }
        public SongException(string message)
            : base(message)
        {}
    }

    class SingleException : CatalogException
    {
        public SingleException()
            : base()
        { }
        public SingleException(string message)
            : base(message)
        {}
    }

    class CollectionException : CatalogException
    {
        public CollectionException()
            : base()
        {}
        public CollectionException(string message)
            : base(message)
        {}
    }

    class GenreException : CatalogException
    {
        public GenreException()
            : base()
        {}

        public GenreException(string message)
            : base (message)
        { }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace MyLabs.Lab2
{
    interface ICollection
    {
        ICollection FindByYear(int year);
        ICollection FindByArtist(string name);
        ICollection FindBySongName(string name);
        ICollection FindByCollection(string name);
        ICollection FindByGenre(Genre genre);
        //void AddSong(string name);
        List<Song> ToList();
        public string GetName();
    }
}

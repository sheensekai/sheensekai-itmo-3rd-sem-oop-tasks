using System;
using System.Collections.Generic;
using System.Text;

namespace MyLabs.Lab2
{
    class Predicates
    {
        class Year
        {
            int year;
            public Year(int year)
            {
                this.year = year;
            }

            public bool Check(Song song)
            {
                return this.year == song.song_year;
            }

        }

        class Name
        {
            string name;

            public Name(string name)
            {
                this.name = name;
            }

            public bool Check(Song song)
            {
                return this.name == song.song_name;
            }
        }

    }
}

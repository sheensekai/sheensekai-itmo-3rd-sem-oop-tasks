using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace MyLabs.Lab3
{
    static class IniParser
    {
        public static IniFile Parse(string file)
        {
            IniFile ini = new IniFile();

            try
            {
                IniSection current = null;
                foreach (string line in File.ReadAllLines(file))
                {
                    string tmp = line;

                    int val = 0;
                    foreach (char ch in tmp)
                    {
                        if (ch != ' ')
                        {
                            break;
                        }
                        ++val;
                    }

                    if (val > 0)
                    {
                        tmp = tmp.Substring(val);
                    }

                    if (tmp.Contains(';'))
                    {
                        tmp = tmp.Substring(0, tmp.IndexOf(';'));
                    }

                    if (string.IsNullOrEmpty(tmp))
                    {
                        continue;
                    }

                    if (tmp.StartsWith('['))
                    {
                        current = ParseSection(tmp);
                        ini.Add(current);
                    }

                   else
                    {
                        

                        int equal = tmp.IndexOf("=");
                        string[] splitted = tmp.Split('=');
                        if (splitted.Length != 2)
                        {
                            throw new IniParserException("Wrong file format in next line: " + line);
                        }

                        int blank = splitted[0].IndexOf(' ');
                        if (blank > 0)
                        {
                            for (int i = blank; i < splitted[0].Length; ++i)
                            {
                                if (splitted[0][i] != ' ')
                                {
                                    throw new IniParserException("Wrong file format in next line: " + line);
                                }
                            }

                            splitted[0] = splitted[0].Remove(blank);
                        }

                        blank = splitted[1].IndexOf(' ');
                        val = 0;
                        if (blank >= 0)
                        {
                            if (blank == 0)
                            {
                                for (int i = blank; i < splitted[1].Length; ++i)
                                {
                                    if (splitted[1][i] != ' ')
                                    {
                                        break;
                                    }
                                    ++val;
                                }
                                splitted[1] = splitted[1].Remove(blank, val);


                                blank = splitted[1].IndexOf(' ');
                                if (blank >= 0)
                                {
                                    val = 0;
                                    for (int i = blank; i < splitted[1].Length; ++i)
                                    {
                                        if (splitted[1][i] != ' ')
                                        {
                                            throw new IniParserException("Wrong file format in next line: " + line);
                                        }
                                        ++val;
                                    }

                                    splitted[1] = splitted[1].Remove(blank);
                                }
                            }

                            else
                            {
                                for (int i = blank; i < splitted[1].Length; ++i)
                                {
                                    if (splitted[1][i] != ' ')
                                    {
                                        throw new IniParserException("Wrong file format in next line: " + line);
                                    }
                                    ++val;
                                }

                                splitted[1] = splitted[1].Remove(blank);
                            }
                        }



                        if (current == null)
                        {
                            throw new IniParserException("Field doesn't belong to any section on next line: " + line);
                        }

                        current.AddField(splitted[0], splitted[1]);
                    }
                }
            }

            catch (IniParserException exp)
            {
                throw exp;
            }
            catch (Exception exp)
            {
                throw new IniParserException("Something went wrong while reading the file");
            }

            return ini;
        }

        private static IniSection ParseSection(string line)
        {
            bool format = true;
            if (!line.EndsWith(']'))
            {
                format = false;
            }
            for (int i = 1; i < line.Length - 1; ++i)
            {
                char tmp = line[i];
                tmp = char.ToUpper(tmp);
                if (tmp < '0' || tmp > '9' && tmp < 'A' || tmp > 'Z' && tmp != '_')
                {
                    format = false;
                    break;
                }
            }

            if (!format)
            {
                throw new IniParserException("Wrong file format in next line: " + line);
            }

            else
            {
                return new IniSection(line.Substring(1, line.Length - 2));
            }
        }
    }

}

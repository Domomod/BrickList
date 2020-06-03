using System;
using SQLite;
namespace BrickListApp.Models
{
    public class Color
    {
        [PrimaryKey]
        public int Id { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
        public string NamePl { get; set; }

        public Color()
        {
        }

        public Color(int _id, int _Code, string _Name, string _NamePl)
        {
            Id = _id;
            Code = _Code;
            Name = _Name;
            NamePl = _NamePl;
        }
    }
}

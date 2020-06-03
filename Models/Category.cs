using System;
using SQLite;

namespace BrickListApp
{
    public class Category
    {
        [PrimaryKey]
        public int Id { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
        public string NamePL { get; set; }


        public Category()
        {
        }
        public Category(int _id, int _Code, string _Name, string _NamePL)
        {
            Id = _id;
            Code = _Code;
            Name = _Name;
            NamePL = _NamePL;
        }
    }
}

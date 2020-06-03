using System;
using SQLite;
namespace BrickListApp.Models
{
    public class ItemType
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string NamePl { get; set; }


        public ItemType()
        {
        }

        public ItemType(int _id, string _Code, string _Name, string _NamePl)
        {
            Id = _id;
            Code = _Code;
            Name = _Name;
            NamePl = _NamePl;
        }
    }
}

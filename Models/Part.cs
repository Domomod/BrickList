using System;
using SQLite;
namespace BrickListApp.Models
{
    public class Part
    {
        [PrimaryKey]
        public int Id { get; set; }
        public int TypeID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string NamePL { get; set; }
        public int CategoryID { get; set; }

        public Part()
        {
        }

        public Part(int _id, int _TypeID, string _Code, string _Name, string _NamePL, int _categoryID)
        {
            Id = _id;
            TypeID = _TypeID;
            Code = _Code;
            Name = _Name;
            NamePL = _NamePL;
            CategoryID = _categoryID;
        }
    }
}

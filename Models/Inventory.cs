using System;
using SQLite;

namespace BrickListApp.Models
{
    public class Inventory
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Active { get; set; }
        public int LastAccessed { get; set; }
        public Inventory()
        {
        }

        public Inventory(int _id, string _Name, int _Active, int _LastAccessed)
        {
            Id = _id;
            Name = _Name;
            Active = _Active;
            LastAccessed = _LastAccessed;
        }
    }
}

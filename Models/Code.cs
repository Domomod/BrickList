using System;
using SQLite;

namespace BrickListApp.Models
{
    public class Code
    {
        [PrimaryKey]
        public int Id { get; set; }
        public int ItemID { get; set; }
        public int ColorId { get; set; }
        public int code { get; set; }

        public Code()
        {
        }
        public Code(int _id, int _ItemID, int _ColorId, int _code)
        {
            Id = _id;
            ItemID = _ItemID;
            ColorId = _ColorId;
            code = _code;
        }
    }
}

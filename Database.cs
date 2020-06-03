using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using BrickListApp.Models;
using SQLite;
using Xamarin.Essentials;

namespace BrickListApp
{
    public static class Database
    {
        private static SQLiteConnection db;


        public static void CreateDB()
        {
            var file= Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal),
        "database.db");
            if (File.Exists(file))
            {
                db = new SQLiteConnection(file);
            }
            else
            {
                //download
                db = new SQLiteConnection(file);
                db.CreateTable<Category>();
                db.CreateTable<Code>();
                db.CreateTable<Color>();
                db.CreateTable<Inventory>();
                db.CreateTable<InventoryPart>();
                db.CreateTable<ItemType>();

            }
        }

        public static InventoryPart AddBrick(InventoryPart brick)
        {
            if (brick.QuantityInStore < brick.QuantityInSet)
                brick.QuantityInStore++;
            db.Delete(brick);
            db.Insert(brick);
            return brick;
        }

        public static InventoryPart RemoveBrick(InventoryPart brick)
        {
            if (brick.QuantityInStore > 0)
                brick.QuantityInStore--;
            db.Delete(brick);
            db.Insert(brick);
            return brick;
        }

        public static List<string> GetSetList()
        {
            var tab = db.Table<Inventory>();
            var list= new List<string>();
            foreach (var brick in tab)
            {
                var data = brick.Id + " " + brick.Name;
                list.Add(data);
            }
            return list;
        }

        public static List<string> GetBricks(int id)

        {
            var list = new List<string>();
            var tab = db.Table<InventoryPart>();
            foreach (var brick in tab)
            {   if (brick.InventoryID == id)
                {
                    var data = brick.Id + " " + brick.ItemID + " " + brick.ColorID + " " + brick.Extra;
                    list.Add(data);
                }
            }
            return list;
        }

        public static void Download(int num, string name)
        {
            
            var source = "http://fcds.cs.put.poznan.pl/MyWeb/BL/" + num.ToString() + ".xml";
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal),
        num+".xml");
            var client = new WebClient();
            client.DownloadFile(source, path);

            string line;
            var file = new StreamReader(path);
            InventoryPart part = new InventoryPart();
            Inventory inv = new Inventory(num, name, 1, 0);
            db.Insert(inv);
            int idx = num;
            while ((line = file.ReadLine()) != null)
            {
                idx++;
                char[] charsToTrim = { '*', ' ', '\'' };
                line = line.Trim(charsToTrim);
                var mylist = line.Split(">");
                switch (mylist[0])
                {
                    case "<ITEM":
                        part = new InventoryPart();
                        part.Id = idx;
                        break;
                    case "<ITEMTYPE":
                        var type = mylist[1].Split("<")[0];
                        part.TypeID = 0; //int.Parse(type); W bazie na Pana stronie yo ma być integer, ale w xmlach są chary
                        break;
                    case "<QTY":
                        var quantity = mylist[1].Split("<")[0];
                        part.QuantityInSet = int.Parse(quantity);
                        break;
                    case "<ITEMID":
                        var itemid = mylist[1].Split("<")[0];
                        int test;
                        if (int.TryParse(itemid, out test))
                            part.ItemID = test;
                        else
                            part.ItemID = itemid.Length;
                        break;
                    case "<COLOR":
                        var color = mylist[1].Split("<")[0];
                        part.ColorID = int.Parse(color);
                        break;
                    case "<EXTRA":
                        if (mylist[1][1] == 'N')
                            part.Extra = 0;
                        else
                            part.Extra = 1;
                        break;
                    case "</ITEM":
                        part.InventoryID = num;
                        db.Insert(part);
                        break;
                    default:
                        break;
                }
            }
        }


        public static void Save(int id, string email)
        {
           
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal),
        "Result.xml");
  
            string text = "";
            var table = db.Table<InventoryPart>().Where(i => i.InventoryID == id & i.QuantityInStore < i.QuantityInSet);
            text += "<INVENTORY>\n";
            foreach (var brick in table)
            {
                string[] lines =
                {
                        "<ITEM>\n",
                        "<ITEMTYPE>"+brick.ItemID.ToString()+"</ITEMTYPE>\n",
                        "<ITEMID>"+brick.Id.ToString()+"</ITEMID>\n",
                        "<COLOR>"+brick.ColorID.ToString()+"</COLOR>\n",
                        "<QTYFILLED>"+(brick.QuantityInSet-brick.QuantityInStore).ToString()+"</QTYFILLD>\n",
                        "</ITEM>\n"
                    };
                foreach (var line in lines)
                    text += line;
            }
            text += "</INVENTORY>";
            List<string> vs = new List<string>();
            vs.Add(email);
            var message = new EmailMessage
            {
                Subject = "Your Lego set",
                Body = text,
                To = vs,
            };
            Email.ComposeAsync(message);
        }

        public static InventoryPart GetBrick(int id, int InventoryId)
        {
            var brick = db.Table<InventoryPart>().Where(i => i.Id == id & i.InventoryID == InventoryId);
            return brick.First();
        }
    }
}

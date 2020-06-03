
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using BrickListApp.Models;


namespace BrickListApp
{
    [Activity(Label = "ShowListsActivity")]
    public class ShowListsActivity : Activity
    {
        int SetId;
        int BrickId;
        InventoryPart brick;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.SetsList);

            Button add = FindViewById<Button>(Resource.Id.button1);
            Button remove = FindViewById<Button>(Resource.Id.button2);
            Button save = FindViewById<Button>(Resource.Id.button5);
            Button back = FindViewById<Button>(Resource.Id.button4);
            ListView sets = FindViewById<ListView>(Resource.Id.listView1);
            ListView bricks = FindViewById<ListView>(Resource.Id.listView2);
            TextView inStore = FindViewById<TextView>(Resource.Id.textView4);
            TextView inSet = FindViewById<TextView>(Resource.Id.textView5);
            EditText mail = FindViewById<EditText>(Resource.Id.editText1);

            bool isSet = false;
            var SetList = Database.GetSetList();
            List<string> BrickList = new List<string>(); ;
            sets.Adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1,SetList );
            brick = new InventoryPart();
            sets.ItemClick += (sender, e) =>
              {
                  SetId = int.Parse(SetList[e.Position].Split(" ")[0]);
                  BrickList = Database.GetBricks(SetId);
                  bricks.Adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, BrickList); //TO DO: finish
              };

            bricks.ItemClick += (sender, e) =>
             {
                 BrickId = int.Parse(BrickList[e.Position].Split(" ")[0]);
                 brick = Database.GetBrick(BrickId, SetId);
                 isSet = true;
                 inStore.Text = brick.QuantityInStore.ToString();
                 inSet.Text = brick.QuantityInSet.ToString();
             };

            add.Click += (sender, e)=> {
                if (isSet)
                {
                   brick= Database.AddBrick(brick);
                    inStore.Text = brick.QuantityInStore.ToString();
                }
            };

            remove.Click += (sender, e) =>
              {
                  if (isSet)
                  {
                      brick=Database.RemoveBrick(brick);
                      inStore.Text = brick.QuantityInStore.ToString();
                  }
              };

            save.Click += (sender, e) =>
              {
                  Database.Save(SetId, mail.Text);
              };

            back.Click += (sender, e) =>
              {
                  var intent = new Intent(this, typeof(MainActivity));
                  StartActivity(intent);
              };
        }
    }
}

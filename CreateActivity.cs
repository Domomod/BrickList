
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

namespace BrickListApp
{
    [Activity(Label = "CreateActivity")]
    public class CreateActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.CreateSet);

            EditText number = FindViewById<EditText>(Resource.Id.editText1);
            EditText name = FindViewById<EditText>(Resource.Id.editText2);
            Button save = FindViewById<Button>(Resource.Id.button1);
            Button show = FindViewById<Button>(Resource.Id.button2);
            
            save.Click += (sender, e) =>
              {
                  if (number.Text.Length > 0 & name.Text.Length > 0)
                      Database.Download(int.Parse(number.Text), name.Text);
              };

            show.Click += (sender, e) =>
              {
                  var intent = new Intent(this, typeof(ShowListsActivity));
                  StartActivity(intent);
              };
            
        }
    }
}

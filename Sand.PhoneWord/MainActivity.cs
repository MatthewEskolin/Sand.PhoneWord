using System;
using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;

namespace Sand.PhoneWord
{
    [Activity(Label = "Sand.PhoneWord", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        public string TranslatedNumber { get; set; }


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

          
            var translateButton = FindViewById<Button>(Resource.Id.btnTranslate);
            var callButton = FindViewById<Button>(Resource.Id.btnCall);


            callButton.Enabled = false;


            translateButton.Click += TranslateButton;
            callButton.Click += MakeCall;




        }

        private void MakeCall(object sender, EventArgs e)
        {
            var callDialog = new AlertDialog.Builder(this);

            callDialog.SetMessage($"Call {TranslatedNumber} ?");
            callDialog.SetNeutralButton("Call", DialogClick);

            callDialog.SetNegativeButton("Cancel", DialogClick_No_OP);

            callDialog.Show();
        }

        private void DialogClick_No_OP(object sender, DialogClickEventArgs args)
        {
            return;
        }

        private void DialogClick(object sender, DialogClickEventArgs dialogClickEventArgs)
        {
            var callIntent = new Intent(Intent.ActionCall);
            callIntent.SetData(Android.Net.Uri.Parse($"tel:{TranslatedNumber}"));
            StartActivity(callIntent);
        }

        private void TranslateButton(object sender, EventArgs e)
        {
            var phoneNumberText = FindViewById<EditText>(Resource.Id.PhoneNumberText);
            var callButton = FindViewById<Button>(Resource.Id.btnCall);

            TranslatedNumber = PhonewordTranslator.ToNumber(phoneNumberText.Text);
            if (String.IsNullOrWhiteSpace(TranslatedNumber))
            {
                callButton.Text = "Call";
                callButton.Enabled = false;
            }
            else
            {
                callButton.Text = "Call " + TranslatedNumber;
                callButton.Enabled = true;
            }


        }
    }
}


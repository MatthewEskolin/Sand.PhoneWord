using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Widget;
using Android.OS;

namespace Sand.PhoneWord
{
    [Activity(Label = "SandPhone", Icon = "@drawable/icon")]
    [IntentFilter(new[]{Android.Content.Intent.ActionMain},Categories = new[]{Android.Content.Intent.CategoryLauncher})]
    public class MainActivity : Activity
    {
        public string TranslatedNumber { get; set; }

        private const int REQUEST_CALL_PERMISSION = 1;

        private static string[] REQUEST_CALL =
        {
            Manifest.Permission.CallPhone
        };

        private static readonly List<string> phoneNumbers = new List<string>();




        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);




            SetContentView(Resource.Layout.Main);


            var translateButton = FindViewById<Button>(Resource.Id.btnTranslate);
            var callButton = FindViewById<Button>(Resource.Id.btnCall);
            var btnCallHistory = FindViewById<Button>(Resource.Id.btnCallHistory);




            callButton.Enabled = false;

            translateButton.Click += TranslateButton;
            callButton.Click += MakeCall;
            btnCallHistory.Click += ShowHistory;



        }

        private void ShowHistory(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(CallHistoryActivity));
            intent.PutStringArrayListExtra("phone_numbers", phoneNumbers);
            StartActivity(intent);

        }

        public void GetMakeCallPermissionAsync()
        {
            const string permission = Manifest.Permission.CallPhone;
            if (CheckSelfPermission(permission) != (int)Permission.Granted)
            {
                if (ShouldShowRequestPermissionRationale(Manifest.Permission.CallPhone))
                {

                }
                else
                {
                    RequestPermissions(REQUEST_CALL, REQUEST_CALL_PERMISSION);
                }
            }
            else
            {
                return;
            }
        }


        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            switch (requestCode)
            {
                case REQUEST_CALL_PERMISSION:
                {
                    if (grantResults.Length > 0 && grantResults[0] == Permission.Granted)
                    {


                    }

                    break;
                }
            }


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
            GetMakeCallPermissionAsync();

            phoneNumbers.Add(TranslatedNumber);

            var btnCallHistory = FindViewById<Button>(Resource.Id.btnCallHistory);
            btnCallHistory.Enabled = true;




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


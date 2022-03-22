using Android.App;
using Android.Content;
using Android.Gms.Tasks;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Android.Views.View;

namespace EShop.Activities
{
    [Activity(Label = "ActivityForgetPassword")]
    public class ActivityForgetPassword : Activity,IOnClickListener,IOnCompleteListener
    {
        EditText txtEmail;
        Button btnReset;
        TextView btnSignup, btnLogin;

        FirebaseAuth auth;

        

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_forget_password);

            //init firebase
            auth = FirebaseAuth.GetInstance(ActivityLogin.app);

            //view
            txtEmail  = FindViewById<EditText>(Resource.Id.et_forget_email);
            btnReset  = FindViewById<Button>(Resource.Id.btn_forget);
            btnSignup = FindViewById<TextView>(Resource.Id.btn_sign_up_fp);
            btnLogin  = FindViewById<TextView>(Resource.Id.btn_login_fp);

            btnReset.SetOnClickListener(this);
            btnSignup.SetOnClickListener(this);
            btnLogin.SetOnClickListener(this);

        }

        public void OnClick(View v)
        {
           if(v.Id == Resource.Id.btn_forget)
            {
                //reset password
                ResetPassword(txtEmail.Text);
            }
            else if (v.Id == Resource.Id.btn_sign_up_fp)
            {
                //open signup activity
                StartActivity(new Intent(this, typeof(ActivitySignUp)));
                Finish();
            }
            else if (v.Id == Resource.Id.btn_login_fp)
            {
                //open login activity
                StartActivity(new Intent(this, typeof(ActivityLogin)));
                Finish();
            }
            
        }

        private void ResetPassword(string email)
        {
            auth.SendPasswordResetEmail(email)
                 .AddOnCompleteListener(this, this);
        }

        public void OnComplete(Task task)
        {
            if(task.IsSuccessful == false)
            {
                Toast.MakeText(this, "Password Reset Faield Try Again", ToastLength.Short).Show();

            }
            else
            {
                Toast.MakeText(this, "Password Reset success Email Sent To You", ToastLength.Short).Show();

            }
        }
    }
}
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
    [Activity(Label = "ActivitySignUp")]
    public class ActivitySignUp : Activity,IOnClickListener,IOnCompleteListener
    {
        Button btnSignUp;
        EditText txtEmail, txtPassword;
        RelativeLayout activity_signup;

        FirebaseAuth auth;

        

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_sign_up);

            //initFirebase
            auth = FirebaseAuth.GetInstance(ActivityLogin.app);

            //View
            btnSignUp = FindViewById<Button>(Resource.Id.btn_signup);
            txtEmail = FindViewById<EditText>(Resource.Id.et_signup_email);
            txtPassword = FindViewById<EditText>(Resource.Id.et_signup_password);

            activity_signup = FindViewById<RelativeLayout>(Resource.Id.activity_sign_up);

            btnSignUp.SetOnClickListener(this);
        }

        public void OnClick(View v)
        {
            if (v.Id == Resource.Id.btn_signup)
            {
                SignUp(txtEmail.Text, txtPassword.Text);
            }else if (v.Id == Resource.Id.btn_forget_password)
            {
                StartActivity(new Intent(this, typeof(ActivityForgetPassword)));
            }else if (v.Id == Resource.Id.btn_login_a)
            {
                StartActivity(new Intent(this, typeof(ActivityLogin)));

            }
            else
            {

            }
        }

        private void SignUp(string email, string pass)
        {
            auth.CreateUserWithEmailAndPassword(email, pass)
                .AddOnCompleteListener(this, this);
        }

        public void OnComplete(Task task)
        {
            if (task.IsSuccessful)
            {
                Toast.MakeText(this, "Register Successfully", ToastLength.Long);
                StartActivity(new Intent(this, typeof(ActivityLogin)));
            }
        }
    }
}
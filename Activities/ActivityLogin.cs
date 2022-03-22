using Android.App;
using Android.Content;
using Android.Gms.Tasks;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase;
using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Android.Views.View;

namespace EShop.Activities
{
    [Activity(Label = "ActivityLogin", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class ActivityLogin : Activity,IOnClickListener,IOnCompleteListener
    {
        Button btnLogin;
        EditText txtEmail, txtPassword;
        TextView btnSignUp, btnForgetPassword;

        RelativeLayout loginActivity;

        public static FirebaseApp app;
        FirebaseAuth auth;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_login);
            //init firebase
            InitFirebase();

            btnLogin          = FindViewById<Button>(Resource.Id.btn_login);
            btnSignUp         = FindViewById<TextView>(Resource.Id.btn_sign_up);
            btnForgetPassword = FindViewById<TextView>(Resource.Id.btn_forget_password);

            txtEmail = FindViewById<EditText>(Resource.Id.et_email);
            txtPassword = FindViewById<EditText>(Resource.Id.et_password);

            loginActivity = FindViewById<RelativeLayout>(Resource.Id.activity_login);


            //buttons on click listner
            btnLogin.SetOnClickListener(this);
            btnSignUp.SetOnClickListener(this);
            btnForgetPassword.SetOnClickListener(this);
        }

        private void InitFirebase()
        {
            var options = new FirebaseOptions.Builder()
                 .SetApplicationId("1:464198851839:android:ed9ee9894cb6203af380ca")
                 .SetApiKey("AIzaSyCY5Gcira_mwoDNQ08vCmtI3BBWgXNzEcw")
                 .Build();

            if(app == null)
            {
                app = FirebaseApp.InitializeApp(this, options);
            }
            auth = FirebaseAuth.GetInstance(app);
        }

        public void OnClick(View v)
        {
            if(v.Id == Resource.Id.btn_login)
            {
                //make login 
                LoginUser(txtEmail.Text,txtPassword.Text);
            }else if(v.Id == Resource.Id.btn_sign_up)
            {
                //open signup activity
                StartActivity(new Intent(this, typeof(ActivitySignUp)));
                Finish();
            }else if(v.Id == Resource.Id.btn_forget_password)
            {
                //open forgetpassword activity
                StartActivity(new Intent(this, typeof(ActivityForgetPassword)));
                Finish();
            }
            else
            {

            }
        }

        private void LoginUser(string email, string pass)
        {
            auth.SignInWithEmailAndPassword(email, pass)
                .AddOnCompleteListener(this, this);
        }

        public void OnComplete(Task task)
        {
            if (task.IsSuccessful)
            {
                Toast.MakeText(this, "Login Success", ToastLength.Long).Show();
                //make redirect to main activity
                StartActivity(new Intent(this, typeof(MainActivity)));
            }
        }
    }
}
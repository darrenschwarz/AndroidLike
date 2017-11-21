using System;
using System.Threading.Tasks;
using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views;

namespace AndroidLike
{
    [Activity(Label = "AndroidLike", MainLauncher = true)]
    public class MainActivity : Activity
    {

        private bool _liked;
        private Button _likeButton;
        private Button _unlikeButton;
        private Button _likeDiasabledButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Main);

            _likeButton = FindViewById<Button>(Resource.Id.likebutton);
            _unlikeButton = FindViewById<Button>(Resource.Id.unlikebutton);
            _likeDiasabledButton = FindViewById<Button>(Resource.Id.likedisabledbutton);
            _liked = false; //TODO: set _liked based on current value onload

            SetLikeButtonState(); 

            _likeButton.Click += async (o, e) =>
            {
                try
                {
                    _liked = await SetLike();
                    Toast.MakeText(this, "Like Button Clicked", ToastLength.Short).Show();
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                }
                finally
                {
                    SetLikeButtonState();
                }

            };

            _unlikeButton.Click += async (o, e) => {

                try
                {
                    _liked = await SetLike();

                    Toast.MakeText(this, "UnLike Button Clicked", ToastLength.Short).Show();
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                }
                finally
                {
                    SetLikeButtonState();
                }
            };

            // TODO: Remove this only used in development to check button is not enabled, and it can't be called
            _likeDiasabledButton.Click += (o, e) => {
                    Toast.MakeText(this, "LikeDisabled Button Clicked", ToastLength.Short).Show();
            };
        }

        public async Task<bool> SetLike()
        {
            _likeButton.Visibility = ViewStates.Gone;
            _unlikeButton.Visibility = ViewStates.Gone;
            _likeDiasabledButton.Visibility = ViewStates.Visible;
            _likeDiasabledButton.Enabled = false;
          
            return await SetLikeService();
        }

        public void SetLikeButtonState()
        {    
            _likeButton.Visibility = _liked ? ViewStates.Gone : ViewStates.Visible;
            _unlikeButton.Visibility = _liked ? ViewStates.Visible : ViewStates.Gone;
            _likeDiasabledButton.Visibility = ViewStates.Gone;            
        }

        public async Task<bool> SetLikeService()
        {
            var taskFactory = new TaskFactory();

            await Task.Delay(2000);

            // throw new Exception("Wahtevs");

            var res = taskFactory.StartNew<bool>(() => !_liked);

            return await res;
        }
    }
}


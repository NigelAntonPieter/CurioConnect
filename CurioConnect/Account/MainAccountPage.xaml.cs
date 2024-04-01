using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System.Collections.ObjectModel;
using CurioConnect.Data;
using CurioConnect.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.UI.Xaml.Media.Imaging;
using CurioConnect.Main;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CurioConnect.Account
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainAccountPage : Page
    {
        public MainAccountPage()
        {
            this.InitializeComponent();
            LoadCurrentUserAndMatches();
            
        }

        private void LoadCurrentUserAndMatches()
        {
            var currentUser = GetDefaultUser();
            if (currentUser != null)
            {
                // Stel de huidige gebruiker in
                User.CurrentUser = currentUser;

                // Geef de naam van de ingelogde gebruiker weer
                LoggedInUserName = currentUser.Name;

                // Laad de foto van de gebruiker met fallback
                if (!string.IsNullOrEmpty(currentUser.Photo))
                {
                    // Set de source van de afbeelding naar de foto van de gebruiker
                    userImage.Source = new BitmapImage(new Uri(currentUser.Photo));
                }
                else
                {
                    // Als er geen foto is, gebruik de fallback-afbeelding
                    userImage.Source = new BitmapImage(new Uri(currentUser.ImagePathWithFallBack));
                }

                // Laad de matches van de huidige gebruiker in de ListView
                matchListView.ItemsSource = currentUser.Matches;
            }
        }

        private User GetDefaultUser()
        {
            using (var context = new AppDbContext())
            {
                // Query om de standaardgebruiker met ID 1 te selecteren
                return context.Users.Include(u => u.Matches).ThenInclude(m => m.MatchedUser).FirstOrDefault(u => u.Id == 1);
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            // Controleer of de navigatieparameter de Id van de nieuwe gebruiker bevat
            if (e.Parameter != null && e.Parameter is int userId)
            {
                using (var context = new AppDbContext())
                {
                    // Selecteer de nieuwe gebruiker op basis van de ontvangen Id
                    var newUser = context.Users.FirstOrDefault(u => u.Id == userId);
                    if (newUser != null)
                    {
                        // Stel de nieuwe gebruiker in als de CurrentUser
                        User.CurrentUser = newUser;

                        // Geef de naam van de ingelogde gebruiker weer
                        LoggedInUserName = newUser.Name;

                        // Laad de foto van de gebruiker
                        if (!string.IsNullOrEmpty(newUser.Photo))
                        {
                            // Set de source van de afbeelding naar de foto van de gebruiker
                            userImage.Source = new BitmapImage(new Uri(newUser.Photo));
                        }
                        else
                        {
                            // Als er geen foto is, gebruik de fallback-afbeelding
                            userImage.Source = new BitmapImage(new Uri(newUser.ImagePathWithFallBack));
                        }
                        // Laad de matches van de nieuwe gebruiker in de ListView
                        matchListView.ItemsSource = newUser.Matches;
                    }
                }
            }
            else
            {
                // Als er geen nieuwe gebruiker is, laad de standaardgebruiker en zijn matches
                LoadCurrentUserAndMatches();
            }
        }


        // Property voor de naam van de ingelogde gebruiker
        public string LoggedInUserName { get; set; }

        private void deleteUser_Click(object sender, RoutedEventArgs e)
        {
            // Verwijder de huidige gebruiker uit de database
            using (var db = new AppDbContext())
            {
                var currentUser = User.CurrentUser;
                db.Users.Remove(currentUser);
                db.SaveChanges();
            }

            // Navigeer terug naar de MainCurioConnect-pagina
            this.Frame.Navigate(typeof(MainCurioConnect));
        }
    }
}
    


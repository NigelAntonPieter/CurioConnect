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
            using (var context = new AppDbContext())
            {
                // Query om de gebruiker met ID 1 te selecteren
                var currentUser = context.Users.Include(u => u.Matches).ThenInclude(m => m.MatchedUser). FirstOrDefault(u => u.Id ==1);
                if (currentUser != null)
                {
                    // Stel de huidige gebruiker in
                    User.CurrentUser = currentUser;

                    // Geef de naam van de ingelogde gebruiker weer
                    LoggedInUserName = currentUser.Name;

                    // Laad de matches van de huidige gebruiker in de ListView
                    matchListView.ItemsSource = currentUser.Matches;
                }
            }

        }
        

        // Property voor de naam van de ingelogde gebruiker
        public string LoggedInUserName { get; set; }
    }
    
}
    


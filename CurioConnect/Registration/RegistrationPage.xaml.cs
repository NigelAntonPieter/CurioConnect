using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Pickers;
using Windows.Storage;
using WinRT.Interop;
using CurioConnect.Data;
using CurioConnect.Model;
using CurioConnect.Main;
using CurioConnect.Utility;
using CurioConnect.Account;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CurioConnect.Registration
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RegistrationPage : Page
    {
        private StorageFile copiedFile;
        public RegistrationPage()
        {
            this.InitializeComponent();
        }

        private async void fileButton_Click(object sender, RoutedEventArgs e)
        {
            await SelectAndCopyFileAsync();
        }

        private void saveAccount_Click(object sender, RoutedEventArgs e)
        {
            string name = nameTB.Text;
            string email = EmailTB.Text;
            string gender = GetSelectedGender();
            string course = ((ComboBoxItem)courseComboBox.SelectedItem).Content.ToString();




            using var db = new AppDbContext();
            var newUser = new User
            {
                Name = name,
                Email = email,
                Gender = gender,
                Course = course,
                Password   = SecureHasher.Hash (passwordPB.Password),
               Photo = copiedFile.Path
            };
            newUser.Matches = new List<Match>();
            db.Users.Add(newUser);
            db.SaveChanges();
            this.Frame.Navigate(typeof(MainAccountPage), newUser.Id);
        }

        private async Task SelectAndCopyFileAsync()
        {
            var fileopenPicker = new FileOpenPicker()
            {
                FileTypeFilter = { ".jpg", ".jpeg", ".png", ".gif" }
            };


            nint windowHandle = WindowNative.GetWindowHandle(App.ParentWindow);
            InitializeWithWindow.Initialize(fileopenPicker, windowHandle);

            var file = await fileopenPicker.PickSingleFileAsync();

            if (file == null)
            {
                return;
            }

            var localFolder = ApplicationData.Current.LocalFolder;

            var fileExtension = file.FileType;

            var currentTime = DateTime.Now;
            var renamedFileName = $"{currentTime.ToFileTime()}{fileExtension}";

            copiedFile = await file.CopyAsync(localFolder, renamedFileName);
        }
        private string GetSelectedGender()
        {
            // Logica om het geselecteerde geslacht op te halen
            if (maleCheckBox.IsChecked == true)
            {
                return "Male";
            }
            else if (femaleCheckBox.IsChecked == true)
            {
                return "Female";
            }
            else if (confusedCheckBox.IsChecked == true)
            {
                return "Confused";
            }
            else
            {
                return null;
            }
        }
    }
}

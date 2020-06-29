using SQLite;
using SqlLiteConnectivity.Classes;
using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace SqlLiteConnectivity
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        private SQLiteConnection conn = null;

        public bool IsForUpdate { get; set; } = false;
        public MainPage()
        {
            InitializeComponent();
            conn = new SQLiteConnection(App.FilePath);
            var table = conn.Table<Contact>();
            if (table == null)
                conn.CreateTable<Contact>();
        }

        async void SaveButton_Clicked(object sender, EventArgs e)
        {

            if (!IsForUpdate)
            {
                Contact contact = new Contact()
                {

                    Name = nameEntry.Text,
                    Email = emailEntry.Text,
                    //PhoneNumber = PhoneNumberEntry.Text
                };
                conn.Insert(contact);
            }
            else
            {
                var contactInfo = this.BindingContext as Contact;
                contactInfo.Email = emailEntry.Text;
                contactInfo.Name = nameEntry.Text;
                conn.Update(contactInfo); 
            }

            await this.Navigation.PopAsync();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            var contactInfo = this.BindingContext as Contact;
            nameEntry.Text = contactInfo.Name;
            emailEntry.Text = contactInfo.Email;
        }

        //void EditEntry(object sender,ItemTappedEventArgs e)
        //{
        //    if(contact!=null)
        //    {
        //        Navigation.PushAsync(new MainPage(Contact));
        //    }
        //}
    }
}

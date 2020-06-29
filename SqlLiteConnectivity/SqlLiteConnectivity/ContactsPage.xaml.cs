using SQLite;
using SqlLiteConnectivity.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SqlLiteConnectivity
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactsPage : ContentPage
    {

        private ICommand deleteCommand;
        public ICommand DeleteCommand
        {
            get => deleteCommand;
            set
            {
                if(deleteCommand != value)
                {
                    deleteCommand = value;
                    OnPropertyChanged(nameof(DeleteCommand));
                }
            }
        }


        private ICommand updateCommand;
        public ICommand UpdateCommand 
        {
            get => updateCommand;
            set
            {
                if (updateCommand != value)
                {
                    updateCommand = value;
                    OnPropertyChanged(nameof(UpdateCommand));
                }
            }
        }



        private SQLiteConnection conn = null;
        public ContactsPage()
        {
            InitializeComponent();
            DeleteCommand = new Command <Contact>(DeleteAction);
            UpdateCommand = new Command<Contact>(UpdateAction);
            conn = new SQLiteConnection(App.FilePath);
        }

        private void DeleteAction(Contact contact)
        {
            conn.Delete(contact);
            var values = conn.Table<Contact>();
        }

        private async void UpdateAction(Contact contact)
        {
            await this.Navigation.PushAsync(new MainPage() { BindingContext = contact, IsForUpdate = true });
        }

        void NewContactToolbarItem_Clicked(object Sender, System.EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
           this.listView.ItemsSource = conn.Table<Contact>();
        }
    }
}
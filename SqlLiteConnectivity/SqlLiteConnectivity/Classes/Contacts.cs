using SQLite;

namespace SqlLiteConnectivity.Classes
{
    public class Contact
    {
        [PrimaryKey, AutoIncrement]
        public int id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }
        public string Email
        {
            get;
            set;
        }
        //public int PhoneNumber
        //{
        //    get;
        //    set;
        //}

        public Contact()
        { 
        }

        
    }
}

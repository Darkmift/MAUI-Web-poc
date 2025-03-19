namespace Model
{
    public class UserLogged
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Type { get; set; }
        public long ID { get; set; }
        public string Name { get; set; }
        public UserLogged()
        {
            Username = string.Empty;
            Password = string.Empty;
            Type = string.Empty;
            ID = 0;
            Name = string.Empty;
        }
        public UserLogged(string Username, string Password, string Type, long ID, string Name)
        {
            this.Username = Username;
            this.Password = Password;
            this.Type = Type;
            this.ID = ID;
            this.Name = Name;
        }
    }
}

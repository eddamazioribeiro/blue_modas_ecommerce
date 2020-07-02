namespace BlueModas.Domain
{
    public class Address
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int ZipCode { get; set; }
        public string Complement { get; set; }
        public bool MainAddress { get; set; }
        public User User { get; set; }
    }
}
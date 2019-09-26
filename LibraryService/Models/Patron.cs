using System;

namespace LibraryService.Models
{
    public class Patron
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CheckoutDate { get; set; }
        public DateTime DueDate { get; set; }
        public string AreaCode { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Street { get; set; }
    }
}

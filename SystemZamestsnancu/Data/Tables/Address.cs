namespace SystemZamestsnancu.Data;

public class Address
{
    public Guid AddressId { get; set; }
    public string City { get; set; }
    public string Zip { get; set; }
    public string Country { get; set; }
    public string Street { get; set; }
    public string HouseNumber { get; set; }
}
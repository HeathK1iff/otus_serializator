using OtusSerializator.Utils;

namespace OtusSerializator.Entities;

public class Person
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime Birthdate { get; set; }
    public string AddressStreet { get; set; }
    public int AddressBuildNo { get; set; }
    public float Weight { get; set; }

    public override string ToString()
    {
        return string.Join(':', new object[] {FirstName, LastName, Birthdate, AddressStreet, AddressBuildNo, Weight});
    }
}
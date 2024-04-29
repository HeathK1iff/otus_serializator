using OtusSerializator.Utils;

namespace OtusSerializator.Entities;
public class F
{ 
    public int i1 { get; set; }
    public int i2 { get; set; }
    public int i3 { get; set; }
    public int i4 { get; set; }
    public int i5 { get; set; } 
    public static F CreateTestObject() => new F() { i1 = 1, i2 = 2, i3 = 3, i4 = 4, i5 = 5 }; 

     public override string ToString(){
        return string.Join(':', new object[] {i1, i2, i3, i4, i5}); 
     }
}
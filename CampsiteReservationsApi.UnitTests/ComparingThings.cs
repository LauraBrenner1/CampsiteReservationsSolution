using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
namespace CampsiteReservationsApi.UnitTests;

public class ComparingThings
{
    [Fact]
    public void ComparingTwoDogs()
    {
        var dogFromApi = new Dog { Name = "Fido", Breed = "Terrier" };

        var dogExpected = new Dog { Name = "Fido", Breed = "Terrier" };

        Assert.Equal(dogExpected, dogFromApi);
       
    }


   
}

public class Dog 
{
    public string Name { get; set; } = String.Empty;
    public string Breed { get; set; } = String.Empty;

}



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

       // dogFromApi.Name = "Rover";
        //Assert.Equal(dogExpected, dogFromApi);
        Assert.Equal(dogExpected.Name, dogFromApi.Name);
       
    }

    [Fact]
    public void ComparingCats()
    {
        var catFromApi = new Cat("Fluffy", "Persian");
        var expectedCat = new Cat("Fluffy", "Persian");

        //expectedCat.Name = "Tiger";
        Assert.Equal(expectedCat, catFromApi);
    }

    [Fact]
    public void YouCannotModifyRecords()
    {
        var fluffy = new Cat("Fluffy", "Persian");

        var newCat = fluffy with { Breed = "Siamese" };

        Assert.Equal("Persian", fluffy.Breed);
        Assert.Equal("Fluffy", fluffy.Name);

        Assert.Equal("Siamese", newCat.Breed);
        Assert.Equal("Fluffy", newCat.Name);

        var myName = "jeff";

        var newName = myName.ToUpper();

        Assert.Equal("JEFF", newName);
        Assert.Equal("jeff", myName);
    }

   
}

public class Dog : IEquatable<Dog>
{
    public string Name { get; set; } = String.Empty;
    public string Breed { get; set; } = String.Empty;

    public bool Equals(Dog? other)
    {
        return Name == other?.Name  && Breed == other?.Breed;
    }
}


public record Cat(string Name, string Breed);


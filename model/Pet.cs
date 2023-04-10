public class Pet
{
    public int PetId { get; set; }
    public string Species { get; set; }
    public DateTime Age { get; set; }
    public string PhysicalCondition { get; set; }
    public string Personality { get; set; }
    public string Nickname { get; set; }
    public Pet()
    {
    }

    public Pet(int petId, string species, DateTime age, string physicalCondition, string personality, string nickname)
    {
        PetId = petId;
        Species = species;
        Age = age;
        PhysicalCondition = physicalCondition;
        Personality = personality;
        Nickname = nickname;
    }
    public override string ToString()
    {
        return $"PetId: {PetId}, Species: {Species}, Age: {Age}, PhysicalCondition: {PhysicalCondition}, Personality: {Personality}, Nickname: {Nickname}";
    }

}
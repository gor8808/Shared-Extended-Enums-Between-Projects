namespace ProgramSelfContainedExtendedEnum.Models;

public class User
{
    public User(Guid id, string name, List<FieldReference> fields)
    {
        Id = id;
        Name = name;
        Fields = fields;
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<FieldReference> Fields { get; set; }
}
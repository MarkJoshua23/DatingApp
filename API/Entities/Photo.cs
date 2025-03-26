namespace API.Entities;

public class Photo
{
    public int Id { get; set; }
    public required string Url { get; set; }
    //if its the main image
    public bool IsMain { get; set; }
    //where is the image stored
    public string? PublicId { get; set; }
}
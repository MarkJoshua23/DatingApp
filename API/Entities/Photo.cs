using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities;
[Table("Photos")]
public class Photo
{
    public int Id { get; set; }
    public required string Url { get; set; }
    //if its the main image
    public bool IsMain { get; set; }
    //where is the image stored
    public string? PublicId { get; set; }


    //these are needed if we want the appuser to be required when making this
    public int AppUserId { get; set; } //==>foreign key
    //Navigation Property to know what entity it is connected
    public AppUser AppUser { get; set; } =null!; //===> nav property

}
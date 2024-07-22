using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

public class AplicationUser : IdentityUser
{
    public string? RefresToken {get; set;}
    public DateTime RefresTokenExpiryTime {get; set;}
    
}
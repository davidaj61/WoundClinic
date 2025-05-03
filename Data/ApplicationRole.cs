using Microsoft.AspNetCore.Identity;

public class ApplicationRole : IdentityRole
{
    // Add custom display name
    public string DisplayName { get; set; }
    
}

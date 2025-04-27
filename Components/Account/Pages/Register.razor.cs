using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Eventing.Reader;
using System.Text;
using System.Text.Encodings.Web;
using WoundClinic.Data;
using WoundClinic.Repository;
using WoundClinic.ViewModels.Account;

namespace WoundClinic.Components.Account.Pages
{
    public partial class Register
    {
        
        private IEnumerable<IdentityError>? identityErrors;

        [SupplyParameterFromForm]
        private RegisterUserViewModel Input { get; set; } = new();

        [SupplyParameterFromQuery]
        private string? ReturnUrl { get; set; }

        private string? Message => identityErrors is null ? null : $"Error: {string.Join(", ", identityErrors.Select(error => error.Description))}";

        public async Task RegisterUser()
        {
            var person=new Person()
            {
                FirstName=Input.FirstName,
                LastName=Input.LastName,
                Gender=Input.Gender,
                NationalCode=Input.PersonNationalCode,
                
            };
            
            var user = CreateUser();

            await UserStore.SetUserNameAsync(user, Input.PersonNationalCode.ToString(), CancellationToken.None);

            if (await _personRepository.CheckPersonExist(Input.PersonNationalCode))
            {
                user.Person =await _personRepository.GetAsync(Input.PersonNationalCode);
            }
            else
            {
                user.Person = await _personRepository.CreateAsync(person);
            }
            
            
            user.PersonNationalCode=user.Person.NationalCode;
            var result = await UserManager.CreateAsync(user, Input.Password);

            if (!result.Succeeded)
            {
                identityErrors = result.Errors;
                return;
            }

            Logger.LogInformation("User created a new account with password.");

            await SignInManager.SignInAsync(user, isPersistent: false);
            RedirectManager.RedirectTo(ReturnUrl);
        }

        private ApplicationUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<ApplicationUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(ApplicationUser)}'. " +
                    $"Ensure that '{nameof(ApplicationUser)}' is not an abstract class and has a parameterless constructor.");
            }
        }

       

        
    }
}
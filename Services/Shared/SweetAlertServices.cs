using Microsoft.JSInterop;
using static Microsoft.JSInterop.IJSRuntime;


namespace WoundClinic.Services.Shared
{
    public class SweetAlertServices
    {
        IJSRuntime JSRuntime;
        public SweetAlertServices(IJSRuntime _jSRuntime)
        {
            JSRuntime = _jSRuntime;
        }
        public async Task ShowModal(string title, string message,string icon)
        {
            JSRuntime.InvokeVoidAsync("Swal.fire", new
            {
                title = title,
                text = message,
                icon = icon
            });
        }

    }
}

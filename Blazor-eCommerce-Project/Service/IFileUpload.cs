using Microsoft.AspNetCore.Components.Forms;
using System.Threading.Tasks;

namespace Blazor_eCommerce_Project.Service
{
    public interface IFileUpload
    {
        Task<string> UploadFile(IBrowserFile file);

    }
}

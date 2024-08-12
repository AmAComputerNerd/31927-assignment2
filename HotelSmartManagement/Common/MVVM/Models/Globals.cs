using Microsoft.Extensions.Configuration;
using System.IO;

namespace HotelSmartManagement.Common.MVVM.Models
{
    public class Globals
    {
        public string AzureBlobConnectionString { get; }
        public IConfiguration Configuration { get; }
        public User? CurrentUser { get; set; }

        public Globals(IConfiguration configuration)
        {
            AzureBlobConnectionString = configuration.GetConnectionString("AzureBlobConnection") ?? throw new ArgumentException("Configuration does not have an AzureBlobConnection section.");
            Configuration = configuration;
            CurrentUser = null;
        }

        public Uri GetProfilePictureUri()
        {
            Uri.TryCreate(Path.Combine(AzureBlobConnectionString, CurrentUser?.ProfilePictureFileName ?? "unknown-user.png"), UriKind.Absolute, out var profilePicturePath);
            if (profilePicturePath == null)
            {
                throw new ArgumentException("ProfilePicture was invalid or missing, and the backup image could not be found either. Check your Azure connection settings.");
            }
            return profilePicturePath;
        }
    }
}

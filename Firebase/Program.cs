
namespace Firebase
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}"); // Pass the value to the Razor view

            app.Run();
        }

    }

    public class RemoteConfigResponse
    {
        public Dictionary<string, RemoteConfigParameter> Parameters { get; set; }
        public Version Version { get; set; }
    }

    public class RemoteConfigParameter
    {
        public DefaultValue DefaultValue { get; set; }
        public string ValueType { get; set; }
    }

    public class DefaultValue
    {
        public string Value { get; set; }
    }

    public class Version
    {
        public string VersionNumber { get; set; }
        public string UpdateTime { get; set; }
        public UpdateUser UpdateUser { get; set; }
        public string UpdateOrigin { get; set; }
        public string UpdateType { get; set; }
    }

    public class UpdateUser
    {
        public string Email { get; set; }
    }

}




namespace DAWeb3
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // Thêm dịch vụ Session
            services.AddHttpContextAccessor();
            services.AddSession();

            // Các cấu hình và thêm các dịch vụ khác
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Sử dụng Session
            app.UseRouting();

            app.UseSession();

            // Cấu hình ứng dụng
        }
    }
}

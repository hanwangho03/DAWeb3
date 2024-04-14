

namespace DAWeb3
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // Thêm dịch vụ Session
            services.AddHttpContextAccessor();
            services.AddSession();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); // Đặt thời gian timeout của session là 30 phút
            });
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

using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace Feature.Models
{
    public class FeatureContext : DbContext
    {
        public FeatureContext()
        {
            // Empty
        }

        public FeatureContext(DbContextOptions<FeatureContext> options)
            : base(options)
        {
            // 공식과 같은 코드
        }

        // 닷넷 코어 기반이 아닌 닷넷 프레임워크 기반의 
        // Web.config 또는 App.config의 데이터베이스 연결 문자열 가져오기
        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = ConfigurationManager.ConnectionStrings[
                    "ConnectionString"].ConnectionString;
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        /// <summary>
        /// 특징 관리자 
        /// </summary>
        public DbSet<FeatureModel> Features { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;

namespace Workman.DataBase.SqlServer
{


    /// <summary>
    /// 数据访问(SqlServer) 上下文
    /// </summary>
    public class DatabaseContext : DbContext
    {
        private string ConnString { get; set; }

        #region  构造函数
        /// <summary>
        /// 初始化一个 使用指定数据连接名称或连接串 的数据访问上下文类 的新实例
        /// </summary>
        /// <param name="connString">连接字串</param>
        public DatabaseContext(string connString)

        {
            ConnString = connString;





        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnString);

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {





            string assembleFileName = Assembly.GetExecutingAssembly().CodeBase.Replace("Workman.DataBase.SqlServer.dll", "Workman.PlugIns.Mapping.DLL").Replace("file:///", "");
            Assembly asm = Assembly.LoadFile(assembleFileName);
            var typesToRegister = asm.GetTypes().Where(p => !String.IsNullOrEmpty(p.Namespace) && p.BaseType != null);

            foreach (var type in typesToRegister)
            {
                try
                {
                    dynamic configurationInstance = Activator.CreateInstance(type);


                    modelBuilder.ApplyConfiguration(configurationInstance);//应用设置
                }
                catch
                {

                }

            }
            base.OnModelCreating(modelBuilder);


        }

        #endregion


    }
}


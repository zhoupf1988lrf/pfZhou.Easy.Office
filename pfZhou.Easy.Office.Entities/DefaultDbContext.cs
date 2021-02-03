using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;

namespace pfZhou.Easy.Office.Entities
{
    /// <summary>
    /// 数据库上下文
    /// </summary>
    public class DefaultDbContext : DbContext
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="options"></param>
        public DefaultDbContext(DbContextOptions<DefaultDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// 模型初始化
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            IEnumerable<Type> alltypes = Assembly.GetAssembly(typeof(DefaultDbContext)).GetTypes().Where(t => t.IsClass && !t.IsAbstract);
            var instanceTypes = alltypes.Where(t => t.IsDefined(typeof(TableAttribute), true));
            foreach (var type in instanceTypes)
            {
                modelBuilder.Model.AddEntityType(type);
            }

            var configTypes = alltypes.Where(t => { Type inface = t.GetInterfaces().FirstOrDefault(); if (inface != null && inface.IsGenericType && inface.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)) return true; return false; }).ToList();
            if (configTypes != null && configTypes.Count > 0)
            {
                foreach (var t in configTypes)
                {
                    dynamic configItem = Activator.CreateInstance(t);
                    modelBuilder.ApplyConfiguration(configItem);
                }
            }
            base.OnModelCreating(modelBuilder);
        }
    }
}

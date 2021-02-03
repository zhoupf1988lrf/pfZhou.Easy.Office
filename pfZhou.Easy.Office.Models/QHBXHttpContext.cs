using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pfZhou.Easy.Office.Models
{
    public static class QHBXHttpContext
    {
        public static IHttpContextAccessor m_httpContextAccessor { get; private set; }
        public static void Configure(IHttpContextAccessor httpContextAccessor)
        {
            m_httpContextAccessor = httpContextAccessor;
        }
        public static Microsoft.AspNetCore.Http.HttpContext Current
        {
            get
            {
                return m_httpContextAccessor.HttpContext;
            }
        }
    }
}

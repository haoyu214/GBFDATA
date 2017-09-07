using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDLR.Core.Aspects
{
    /// <summary>
    /// 支持AOP拦截的接口,它被认为是一种插件动态注入到系统中
    /// </summary>
    public interface IAspectProxy : LDLR.Core.LindPlugins.IPlugins { }
}

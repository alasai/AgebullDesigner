using System.IO;
using System.Linq;
using System.Text;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.RobotCoder;

namespace Agebull.EntityModel.Designer.WebApi
{
    public sealed class ApiProxyBuilder : CoderWithEntity
    {
        protected override bool CanWrite => true;
        /// <summary>
        /// 名称
        /// </summary>
        protected override string FileSaveConfigName => "File_Api_Proxy_cs";

        /// <summary>
        ///     生成基础代码
        /// </summary>
        protected override void CreateBaCode(string path)
        {
            if (Entity.ExtendConfigListBool["NoApi"])
                return;
            var code = $@"
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Runtime.Serialization;
using System.IO;
using Newtonsoft.Json;

using Agebull.Common;
using Agebull.Common.DataModel;
using Gboxt.Common.DataModel;
using Agebull.Common.WebApi;

{Project.UsingNameSpaces}

namespace {NameSpace}.WebApi.EntityApi
{{
    /// <summary>
    /// {Entity.Description}的实体数据操作接口代理类
    /// </summary>
    public class {Entity.Name}ApiProxy : I{Entity.Name}Api
    {{
        
        /// <summary>
        ///     服务地址
        /// </summary>
        public string Host
        {{
            get
            {{
                return Caller.Host;
            }}
            set
            {{
                Caller.Host = value;
            }}
        }}
        
        /// <summary>
        ///     服务调用对象
        /// </summary>
        public readonly WebApiCaller Caller = new WebApiCaller();

        /// <summary>
        ///     新增
        /// </summary>
        /// <param name=""data"">数据</param>
        /// <returns>如果为真并返回结果数据</returns>
        public ApiResult<{Entity.Name}> AddNew({Entity.Name} data)
        {{
            return Caller.Post<{Entity.Name}>(""entity/{Entity.Name}/AddNew"", data);
        }}

        /// <summary>
        ///     修改
        /// </summary>
        /// <param name=""data"">数据</param>
        /// <returns>如果为真并返回结果数据</returns>
        public ApiResult<{Entity.Name}> Update({Entity.Name} data)
        {{
            return Caller.Post<{Entity.Name}>(""entity/{Entity.Name}/Update"", data);
        }}

        /// <summary>
        ///     删除
        /// </summary>
        /// <param name=""dataKey"">数据主键</param>
        /// <returns>如果为否将阻止后续操作</returns>
        public ApiResult Delete(Argument<{Entity.PrimaryColumn?.LastCsType ?? "int"}> dataKey)
        {{
            return Caller.Post(""entity/{Entity.Name}/Delete"", dataKey);
        }}

        /// <summary>
        ///     分页
        /// </summary>
        /// <param name=""arg"">分页参数</param>
        /// <returns>结果数据</returns>
        public ApiResult<ApiPageData<{Entity.Name}>> Query(PageArgument arg)
        {{
            return Caller.Post<ApiPageData<{Entity.Name}>>(""entity/{Entity.Name}/Query"", arg);
        }}
    }}
}}";
            var file = ConfigPath(Entity, FileSaveConfigName, path, "Proxy", $"{Entity.Name}EntityApi.cs");
            SaveCode(file, code);
        }

        /// <summary>
        ///     生成基础代码
        /// </summary>
        protected override void CreateExCode(string path)
        {
            StringBuilder code = new StringBuilder();
            code.Append($@"using System;
using System.Web.Http;
using GoodLin.Common.Ioc;
using Yizuan.Service.Api;
using Yizuan.Service.Api.WebApi;

namespace {NameSpace}.WebApi
{{
    /// <summary>
    /// {Project.Caption}服务API
    /// </summary>
    public class {Project.ApiName}Proxy : I{Project.ApiName}
    {{
        /// <summary>
        ///     服务地址
        /// </summary>
        public string Host
        {{
            get
            {{
                return caller.Host;
            }}
            set
            {{
                caller.Host = value;
            }}
        }}
        
        /// <summary>
        ///     服务调用对象
        /// </summary>
        WebApiCaller caller = new WebApiCaller();");

            foreach (var item in Project.ApiItems)
            {
                code.Append($@"
        /// <summary>
        ///     {item.Caption}:{item.Description}:
        /// </summary>");
                if (item.Argument != null)
                {
                    code.Append($@"
        /// <param name=""arg"">{item.Argument?.Caption}</param>");
                }
                if (item.Result == null)
                {
                    code.Append(@"
        /// <returns>操作结果</returns>");
                }
                else
                {
                    code.Append($@"
        /// <returns>{item.Result.Caption}</returns>");
                }
                var res = item.Result == null ? null : "<" + item.Result.Name + ">";
                var arg = item.Argument == null ? null : $"{item.Argument.Name} arg";

                var arg2 = item.Argument == null ? "\"\"" : "arg";
                code.Append($@"
        public ApiResult{res} {item.Name}({arg})
        {{
            return caller.Post{res}(""{item.RoutePath}"", {arg2});
        }}");
            }

            code.Append(@"
    }
}");
            var file = ConfigPath(Project, FileSaveConfigName, path, "Proxy", $"{Project.ApiName}Proxy.cs");
            SaveCode(file, code.ToString());
        }

    }
}
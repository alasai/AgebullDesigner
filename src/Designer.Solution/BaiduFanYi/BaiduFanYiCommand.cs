﻿using System.ComponentModel.Composition;
using Agebull.Common;
using Agebull.EntityModel.Config;
using Agebull.Common.Mvvm;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    ///     百度翻译的命令
    /// </summary>
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    public class BaiduFanYiCommand : IAutoRegister
    {
        #region 命令注入

        /// <summary>
        /// 注册代码
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            CommandCoefficient.RegisterCommand(new CommandItemBuilder<ConfigBase>
            {
                Action = Name2CaptionChiness,
                Caption = "翻译成中文(名称到标题)",
                Catalog = "翻译",
                
                IconName = "imgBaidu"
            });
            CommandCoefficient.RegisterCommand(new CommandItemBuilder<ConfigBase>
            {
                Action = ToChiness,
                Caption = "翻译成中文(标题与说明)",
                Catalog = "翻译",
                Description = "通过百度翻译接口,将字段的标题与说明从英文翻译成中文(需要网络连接)",
                
                IconName = "imgBaidu"
            });
            CommandCoefficient.RegisterCommand(new CommandItemBuilder<ConfigBase>
            {
                Catalog = "翻译",
                Caption = "翻译标题(中译英)",
                Action = CaptionToEnglish,
                Description = "通过百度翻译接口,将标题从中文翻译成英文(需要网络连接)",
                
                IconName = "imgBaidu"
            });
            CommandCoefficient.RegisterCommand(new CommandItemBuilder<ConfigBase>
            {
                Catalog = "翻译",
                Caption = "翻译名称(中译英)",
                Action = NameToEnglish,
                
                Description = "通过百度翻译接口,将名称从中文翻译成英文(需要网络连接)",
                IconName = "imgBaidu"
            });

        }

        /// <summary>
        ///     英译中
        /// </summary>
        public void NameToEnglish(ConfigBase item)
        {
            item.Name = BaiduFanYi.ToWord(item.Name);
        }
        /// <summary>
        ///     自动修复
        /// </summary>
        public void CaptionToEnglish(ConfigBase item)
        {
            item.Caption = BaiduFanYi.ToWord(item.Caption);
        }

        #endregion


        #region 实现

        private static void ToChiness(ConfigBase config)
        {
            config.Caption = BaiduFanYi.ToChiness(config.Caption ?? config.Name);
            config.Description = BaiduFanYi.ToChiness(config.Description);
        }


        private static void Name2CaptionChiness(ConfigBase config)
        {
            config.Caption = BaiduFanYi.ToChiness(config.Name);
        }
        #endregion
    }
}
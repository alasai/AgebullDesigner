﻿// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// 作者:bull2
// 配置:CodeRefactor-Agebull.CodeRefactor.CodeAnalyze.Application
// 建立:2014-11-20
// 修改:2014-11-29
// *****************************************************/

#region 引用

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;
using Agebull.EntityModel.Config;
using Agebull.Common.Mvvm;

#endregion

namespace Agebull.EntityModel.Designer
{
    public sealed class NewEntityModel : TraceModelBase
    {
        #region 设计对象

        private EntityConfig _entity;
        /// <summary>
        /// 生成的表格对象
        /// </summary>
        public EntityConfig Entity
        {
            get => _entity;
            set
            {
                if (_entity == value)
                    return;
                _entity = value;
                RaisePropertyChanged(nameof(Entity));
            }
        }

        internal readonly FieldFormatCommand Format = new FieldFormatCommand();
        /// <summary>
        ///     当前文件名
        /// </summary>
        public string Fields
        {
            get => Format.Fields;
            set
            {
                if (Format.Fields == value)
                    return;
                Format.Fields = value;
                RaisePropertyChanged(nameof(Fields));
            }
        }

        /// <summary>
        ///     字段
        /// </summary>
        public NotificationList<PropertyConfig> Columns { get; } = new ConfigCollection<PropertyConfig>();

        #endregion

        #region 操作命令

        private List<CommandItemBase> _exCommands;

        public List<CommandItemBase> ExCommands => _exCommands ?? (_exCommands = new List<CommandItemBase>
        {
            new AsyncCommandItem<string, List<PropertyConfig>>
                (FormatPrepare, Format.DoCheckFieldes, CheckFieldesEnd)
                {
                    IsButton=true,

                    Caption = "分析文本",
                    Image = Application.Current.Resources["tree_Assembly"] as ImageSource
                },
            new AsyncCommandItem<string, string>
                (FormatPrepare, Format.DoFormatCSharp, FormatEnd)
                {
                    IsButton=true,

                    Caption = "规整文本(C#风格属性)",
                    Image = Application.Current.Resources["tree_Assembly"] as ImageSource
                },
            new AsyncCommandItem<string, string>
                (FormatPrepare, Format.DoFormat2,FormatEnd)
                {
                    IsButton=true,

                    Caption = "规整文本(名称 类型 标题)",
                    Image = Application.Current.Resources["tree_Assembly"] as ImageSource
                },
            new AsyncCommandItem<string, string>
                (FormatPrepare, Format.DoFormatMySql, FormatEnd)
                {
                    IsButton=true,

                    Caption = "规整文本(MySql数据库)",
                    Image = Application.Current.Resources["tree_Assembly"] as ImageSource
                },
            new AsyncCommandItem<string, string>
                (FormatPrepare, Format.DoFormatSqlServer, FormatEnd)
                {
                    IsButton=true,

                    Caption = "规整文本(SqlServer数据库)",
                    Image = Application.Current.Resources["tree_Assembly"] as ImageSource
                }
        });

        internal bool FormatPrepare(string arg)
        {
            return !string.IsNullOrWhiteSpace(Fields);
        }

        internal void FormatEnd(CommandStatus status, Exception ex, string code)
        {
            if (status != CommandStatus.Succeed)
                return;
            Fields = code;
        }

        internal void CheckFieldesEnd(CommandStatus status, Exception ex, List<PropertyConfig> columns)
        {
            if (status != CommandStatus.Succeed)
                return;
            Columns.Clear();
            foreach (var col in columns)
            {
                Columns.Add(col);
            }
        }
        #endregion
    }
}
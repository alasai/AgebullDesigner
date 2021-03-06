using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Agebull.EntityModel.Config
{
    /// <summary>
    /// 分类配置
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public partial class ClassifyItem<TConfig> : ParentConfigBase
        where TConfig : ClassifyConfig, new()
    {
        /// <summary>
        /// 构造
        /// </summary>
        public ClassifyItem()
        {
        }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="updateAction"></param>
        public ClassifyItem(Action<string, TConfig> updateAction)
        {
            UpdateAction = updateAction;
        }
        /// <summary>
        ///     属性修改处理
        /// </summary>
        /// <param name="propertyName">属性</param>
        protected override void OnPropertyChangedInner(string propertyName)
        {
            base.OnPropertyChangedInner(propertyName);
            if (_items == null || _items.Count == 0)
                return;
            foreach (var item in _items)
                item.OnClassifyChanged(this);
        }

        /// <summary>
        /// 遍历子级
        /// </summary>
        public override void ForeachChild(Action<ConfigBase> action)
        {
            foreach (var item in _items)
                action(item);
        }

        /// <summary>
        /// 子级
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        private ConfigCollection<TConfig> _items = new ConfigCollection<TConfig>();

        /// <summary>
        /// 子级
        /// </summary>
        [IgnoreDataMember, JsonIgnore, Browsable(false)]
        public ConfigCollection<TConfig> Items
        {
            get => _items;
            set
            {
                if (_items == value)
                {
                    return;
                }
                _items = value;
                RaisePropertyChanged(() => Items);
            }
        }
        /// <summary>
        /// 更新事件
        /// </summary>
        public readonly Action<string, TConfig> UpdateAction;

        /// <summary>
        ///     记录属性修改
        /// </summary>
        /// <param name="propertyName">属性</param>
        protected override void RecordModifiedInner(string propertyName)
        {
            base.RecordModifiedInner(propertyName);
            if (UpdateAction == null || propertyName != nameof(Name))
                return;
            foreach (var item in Items)
                UpdateAction(Name, item);
        }
    }
}
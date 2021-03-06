using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Agebull.EntityModel.Config
{
    /// <summary>
    /// 分类配置
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public partial class ClassifyGroupConfig<TConfig> : ConfigBase
        where TConfig : ClassifyConfig, new()
    {
        /// <summary>
        /// 关联的集合
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        private readonly NotificationList<TConfig> _collection;
        /// <summary>
        /// 更新分类的方法
        /// </summary>
        private readonly Action<string, TConfig> _updateAction;
        /// <summary>
        /// 分组的LAMBDA表达式
        /// </summary>
        private readonly Func<TConfig, string> _groupFunc;
        /// <summary>
        /// 分类的属性名称
        /// </summary>
        private readonly string _propertyName;

        /// <summary>
        /// 节点
        /// </summary>
        public ConfigCollection<ClassifyItem<TConfig>> Classifies { get; set; } 

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="group"></param>
        /// <param name="updateAction"></param>
        /// <param name="classify"></param>
        public ClassifyGroupConfig(NotificationList<TConfig> collection, Expression<Func<TConfig, string>> group, 
            Action<string, TConfig> updateAction, ConfigCollection<ClassifyItem<TConfig>> classify=null)
        {
            _collection = collection;
            _collection.CollectionChanged += CollectionChanged;
            _groupFunc = group.Compile();
            _propertyName = ((MemberExpression)group.Body).Member.Name;
            _updateAction = updateAction;
            Classifies = classify ?? new ConfigCollection<ClassifyItem<TConfig>>();
            ReGroup();
        }
        /// <summary>
        /// 重新分组
        /// </summary>
        private void DoReGroup()
        {
            Thread.Sleep(1000);
            needReGroup--;
            if (needReGroup > 0)
                return;
            ReGroup();
        }

        /// <summary>
        /// 重新分组
        /// </summary>
        private void ReGroup()
        {
            var items = new Dictionary<string, ClassifyItem<TConfig>>();
            var array = Classifies.ToArray();
            foreach (var item in array)
            {
                if (string.IsNullOrWhiteSpace(item.Name))
                    item.Name = item.Caption;
                if (!string.IsNullOrWhiteSpace(item.Name) && !items.ContainsKey(item.Name))
                    items.Add(item.Name, item);
                else
                    Classifies.Remove(item);
            }
            foreach (var g in _collection.GroupBy(_groupFunc).ToArray())
            {
                string nmae = g.Key ?? "未分类";
                if (!items.ContainsKey(nmae))
                {
                    var group = new ClassifyItem<TConfig>(_updateAction)
                    {
                        Name = nmae,
                        Caption = nmae,
                        Items = new ConfigCollection<TConfig>()
                    };
                    foreach (var item in g)
                    {
                        item.PropertyChanged -= Item_PropertyChanged;
                        item.PropertyChanged += Item_PropertyChanged;
                        group.Items.Add(item);
                    }
                    Classifies.Add(group);
                }
                else
                {
                    var group = items[nmae];
                    var old = group.Items.ToList();
                    foreach (var item in g)
                    {
                        if (!old.Contains(item))
                        {
                            item.PropertyChanged -= Item_PropertyChanged;
                            item.PropertyChanged += Item_PropertyChanged;
                            group.Items.Add(item);
                        }
                        else
                        {
                            old.Remove(item);
                        }
                    }
                    foreach (var item in old)
                    {
                        group.Items.Remove(item);
                    }
                }
            }
        }


        private void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            needReGroup++;
            Task.Factory.StartNew(DoReGroup);
        }

        private int needReGroup;
        private void Item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != _propertyName)
                return;
            needReGroup++;
            Task.Factory.StartNew(DoReGroup);
        }

    }
}
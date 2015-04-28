using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Script.Serialization;

namespace Qisda.PMS.Common
{
    public class EasyuiTreegridHelp
    {
        public static string Serializer<T>(IList<T> datas, T footer, string parentIdProperty, string hasChildrenProperty) where T : class
        {
            Type type = typeof(T);
            FieldInfo[] fieldInfos = type.GetFields();
            PropertyInfo[] propertyInfos = type.GetProperties();

            EasyuiTreegrid easyuiTreegrid = new EasyuiTreegrid
            {
                rows = Convert<T>(datas, parentIdProperty, hasChildrenProperty, type, fieldInfos, propertyInfos),
                footer = Convert(footer, parentIdProperty, type, fieldInfos, propertyInfos, false)
            };

            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            string serializer = javaScriptSerializer.Serialize(easyuiTreegrid);

            return serializer;
        }

        public static string SerializerChild<T>(IList<T> datas, string parentIdProperty, string hasChildrenProperty) where T : class
        {
            Type type = typeof(T);
            FieldInfo[] fieldInfos = type.GetFields();
            PropertyInfo[] propertyInfos = type.GetProperties();

            var rows = Convert<T>(datas, parentIdProperty, hasChildrenProperty, type, fieldInfos, propertyInfos);

            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            string serializer = javaScriptSerializer.Serialize(rows);

            return serializer;
        }

        private static List<Hashtable> Convert<T>(IList<T> datas, string parentIdProperty, string hasChildrenProperty, Type type, FieldInfo[] fieldInfos, PropertyInfo[] propertyInfos) where T : class
        {
            if (datas == null)
            {
                return new List<Hashtable>();
            }

            var rows =
                (from data in datas.Where(data => data != null)
                 let hasChildren = string.IsNullOrEmpty(hasChildrenProperty)
                     ? false
                     : type.InvokeMember(hasChildrenProperty, BindingFlags.GetProperty, null, data, null)
                 select Convert(data, parentIdProperty, type, fieldInfos, propertyInfos, (bool)hasChildren))
                .ToList();
            return rows;
        }

        private static Hashtable Convert<T>(T data, string parentIdProperty, Type type, FieldInfo[] fieldInfos, PropertyInfo[] propertyInfos, bool hasChildren) where T : class
        {
            if (data == null)
            {
                return null;
            }

            Hashtable hashtable = new Hashtable();

            if (fieldInfos != null && fieldInfos.Length > 0)
            {
                foreach (FieldInfo fieldInfo in fieldInfos)
                {
                    object fieldValue = type.InvokeMember(fieldInfo.Name, BindingFlags.GetField, null, data, null);
                    hashtable.Add(fieldInfo.Name, fieldValue);
                }
            }

            if (propertyInfos != null && propertyInfos.Length > 0)
            {
                foreach (PropertyInfo propertyInfo in propertyInfos)
                {
                    object propertyValue = type.InvokeMember(propertyInfo.Name, BindingFlags.GetProperty, null, data, null);
                    hashtable.Add(propertyInfo.Name, propertyValue);
                }
            }

            object parentIdValue = InvokePropertyValue(type, parentIdProperty, data);
            if (parentIdValue != null)
            {
                hashtable.Add("_parentId", parentIdValue);
            }

            hashtable.Add("state", hasChildren ? "closed" : "open");

            return hashtable;
        }

        private static object InvokePropertyValue(Type type, string name, object target)
        {
            var names = name.Split('.');
            if (names.Length <= 1)
            {
                return type.InvokeMember(name, BindingFlags.GetProperty | BindingFlags.GetField, null, target, null);
            }
            else
            {
                var t1 = type.InvokeMember(names[0], BindingFlags.GetProperty | BindingFlags.GetField, null, target, null);
                if (t1 == null)
                {
                    return null;
                }
                else
                {
                    string lastname = name.Substring(name.IndexOf('.') + 1);
                    return InvokePropertyValue(t1.GetType(), lastname, t1);
                }
            }
        }
    }

    [Serializable]
    internal class EasyuiTreegrid
    {
        public int total
        {
            get { return rows == null ? 0 : rows.Count; }
        }
        public IList<Hashtable> rows { get; set; }
        public Hashtable footer { get; set; }
    }
}

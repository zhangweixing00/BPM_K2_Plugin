using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;

namespace WFWizard.WF
{
    public static class Expand
    {
        public static List<T> ToList<T>(this DataTable table) where T : new()
        {
            List<T> list = new List<T>();
            T t = new T();
            //获取全部属性
            PropertyInfo[] propertys = t.GetType().GetProperties();
            //循环表
            for (int count = 0; count < table.Rows.Count; count++)
            {
                t = new T();
                foreach (PropertyInfo property in propertys)
                {
                    //T 属性名称
                    string propertyName = property.Name;
                    if (!table.Columns.Contains(propertyName))
                    { continue; }
                    string readerValue = table.Rows[count][propertyName].ToString();

                    //T 属性类型
                    string propertyTypeName = property.PropertyType.Name;
                    if (propertyTypeName != "String")
                    {
                        if (readerValue == "")
                        {
                            property.SetValue(t, null, null);

                        }
                        else
                        {
                            try
                            {
                                if (propertyTypeName == "Guid")
                                {
                                    Guid accountID = new Guid(readerValue);
                                    property.SetValue(t, accountID, null);
                                    continue;
                                }
                                if (propertyTypeName == "Boolean")
                                {
                                    bool bools = System.Convert.ToBoolean(readerValue);
                                    property.SetValue(t, bools, null);
                                    continue;
                                }
                                if (propertyTypeName == "DateTime")
                                {
                                    DateTime dt = DateTime.Parse(readerValue);
                                    property.SetValue(t, dt, null);
                                    continue;
                                }
                                if (propertyTypeName == "Int32")
                                {
                                    int proccessdata = System.Convert.ToInt32(readerValue);
                                    property.SetValue(t, proccessdata, null);
                                    continue;
                                }


                            }
                            catch
                            {
                                property.SetValue(t, null, null);
                                continue;
                            }
                        }

                    }
                    else
                    {
                        bool rederValueBool = true;
                        if (Boolean.TryParse(readerValue, out rederValueBool))
                        {
                            if (rederValueBool)
                                property.SetValue(t, "是", null);
                            else
                                property.SetValue(t, "否", null);
                        }
                        else
                            property.SetValue(t, readerValue, null);
                    }
                }
                list.Add(t);
            }
            return list;
        }

        public static T ToClass<T>(this DataTable table) where T : new()
        {
            T t = new T();
            //获取全部属性
            PropertyInfo[] propertys =typeof(T).GetType().GetProperties();
            //循环表
            for (int count = 0; count < table.Rows.Count; count++)
            {
                 t = new T();
                foreach (PropertyInfo property in propertys)
                {
                    //T 属性名称
                    string propertyName = property.Name;

                    string readerValue = table.Rows[count][propertyName].ToString();

                    //T 属性类型
                    string propertyTypeName = property.PropertyType.Name;
                    if (propertyTypeName != "String")
                    {
                        if (readerValue == "")
                        {
                            property.SetValue(t, null, null);

                        }
                        else
                        {
                            try
                            {
                                if (propertyTypeName == "Guid")
                                {
                                    Guid accountID = new Guid(readerValue);
                                    property.SetValue(t, accountID, null);
                                    continue;
                                }
                                if (propertyTypeName == "Boolean")
                                {
                                    bool bools = System.Convert.ToBoolean(readerValue);
                                    property.SetValue(t, bools, null);
                                    continue;
                                }
                                if (propertyTypeName == "DateTime")
                                {
                                    DateTime dt = DateTime.Parse(readerValue);
                                    property.SetValue(t, dt, null);
                                    continue;
                                }
                                if (propertyTypeName == "Int32")
                                {
                                    int proccessdata = System.Convert.ToInt32(readerValue);
                                    property.SetValue(t, proccessdata, null);
                                    continue;
                                }
                                if (propertyTypeName == "RegardingObject")
                                {
                                    //RegardingObject RegardingObject = new RegardingObject()
                                    //{
                                    //    LogicalName = table.Rows[count]["regardingbjectname"].ToString(),
                                    //    RegardingObjectId = new Guid(table.Rows[count]["regardingobjectid"].ToString()),
                                    //    businessname = table.Rows[count]["regardingobjectidname"].ToString(),
                                    //    regardingobjecttype = table.Rows[count]["objecttypecode"].ToString(),
                                    //    isactivity = table.Rows[count]["isactivity"].ToString()

                                    //};
                                    //property.SetValue(t, RegardingObject, null);
                                    continue;
                                }

                            }
                            catch
                            {
                                property.SetValue(t, null, null);
                                continue;
                            }
                        }

                    }

                    else
                    {
                        bool rederValueBool = true;
                        if (Boolean.TryParse(readerValue, out rederValueBool))
                        {
                            if (rederValueBool)
                                property.SetValue(t, "是", null);
                            else
                                property.SetValue(t, "否", null);
                        }
                        else
                            property.SetValue(t, readerValue, null);
                    }
                }

            }
            return t;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Data;


namespace CreateInvoiceSystem.DAO
{
    public static class DeepCopyUtility
    {

        /// <summary>   
        /// Copies the data of one object to another. The target object gets properties of the first.    
        /// Any matching properties (by name) are written to the target.   
        /// </summary>   
        /// <param name="source">The source object to copy from
        /// <param name="target">The target object to copy to
        public static void CopyObjectData(object source, object target)
        {
            CopyObjectData(source, target, String.Empty, BindingFlags.Public | BindingFlags.Instance);
        }

        /// <summary>   
        /// Copies the data of one object to another. The target object gets properties of the first.    
        /// Any matching properties (by name) are written to the target.   
        /// </summary>   
        /// <param name="source">The source object to copy from
        /// <param name="target">The target object to copy to
        /// <param name="excludedProperties">A comma delimited list of properties that should not be copied
        /// <param name="memberAccess">Reflection binding access
        public static void CopyObjectData(object source, object target, string excludedProperties, BindingFlags memberAccess)
        {
            string[] excluded = null;
            if (!string.IsNullOrEmpty(excludedProperties))
            {
                excluded = excludedProperties.Split(new char[1] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            }

            MemberInfo[] miT = target.GetType().GetMembers(memberAccess);
            foreach (MemberInfo Field in miT)
            {
                string name = Field.Name;

                // Skip over excluded properties   
                if (string.IsNullOrEmpty(excludedProperties) == false
                    && excluded.Contains(name))
                {
                    continue;
                }


                if (Field.MemberType == MemberTypes.Field)
                {
                    FieldInfo sourcefield = source.GetType().GetField(name);
                    if (sourcefield == null) { continue; }

                    object SourceValue = sourcefield.GetValue(source);
                    ((FieldInfo)Field).SetValue(target, SourceValue);
                }
                else if (Field.MemberType == MemberTypes.Property)
                {
                    PropertyInfo piTarget = Field as PropertyInfo;
                    PropertyInfo sourceField = source.GetType().GetProperty(name, memberAccess);
                    if (sourceField == null) { continue; }

                    if (piTarget.CanWrite && sourceField.CanRead)
                    {
                        object targetValue = piTarget.GetValue(target, null);
                        object sourceValue = sourceField.GetValue(source, null);

                        if (sourceValue == null) { continue; }

                        if (sourceField.PropertyType.IsArray
                            && piTarget.PropertyType.IsArray
                            && sourceValue != null)
                        {
                            CopyArray(source, target, memberAccess, piTarget, sourceField, sourceValue);
                        }
                        else
                        {
                            CopySingleData(source, target, memberAccess, piTarget, sourceField, targetValue, sourceValue);
                        }
                    }
                }
            }
        }

        private static void CopySingleData(object source, object target, BindingFlags memberAccess, PropertyInfo piTarget, PropertyInfo sourceField, object targetValue, object sourceValue)
        {
            //instantiate target if needed   
            if (targetValue == null
                && piTarget.PropertyType.IsValueType == false
                && piTarget.PropertyType != typeof(string))
            {
                if (piTarget.PropertyType.IsArray)
                {
                    targetValue = Activator.CreateInstance(piTarget.PropertyType.GetElementType());
                }
                else
                {
                    targetValue = Activator.CreateInstance(piTarget.PropertyType);
                }
            }

            if (piTarget.PropertyType.IsValueType == false
                && piTarget.PropertyType != typeof(string))
            {
                CopyObjectData(sourceValue, targetValue, "", memberAccess);
                piTarget.SetValue(target, targetValue, null);
            }
            else
            {
                if (piTarget.PropertyType.FullName == sourceField.PropertyType.FullName)
                {
                    object tempSourceValue = sourceField.GetValue(source, null);
                    piTarget.SetValue(target, tempSourceValue, null);
                }
                else
                {
                    CopyObjectData(piTarget, target, "", memberAccess);
                }
            }
        }

        private static void CopyArray(object source, object target, BindingFlags memberAccess, PropertyInfo piTarget, PropertyInfo sourceField, object sourceValue)
        {
            int sourceLength = (int)sourceValue.GetType().InvokeMember("Length", BindingFlags.GetProperty, null, sourceValue, null);
            Array targetArray = Array.CreateInstance(piTarget.PropertyType.GetElementType(), sourceLength);
            Array array = (Array)sourceField.GetValue(source, null);

            for (int i = 0; i < array.Length; i++)
            {
                object o = array.GetValue(i);
                object tempTarget = Activator.CreateInstance(piTarget.PropertyType.GetElementType());
                CopyObjectData(o, tempTarget, "", memberAccess);
                targetArray.SetValue(tempTarget, i);
            }
            piTarget.SetValue(target, targetArray, null);
        }


        #region DataTable DB Helper..   
        private static readonly IDictionary<Type, IEnumerable<PropertyInfo>> _Properties = new Dictionary<Type, IEnumerable<PropertyInfo>>();

        public static IEnumerable<T> DataTableToList<T>(this DataTable table) where T : class, new()
        {
            try
            {
                var objType = typeof(T);
                IEnumerable<PropertyInfo> properties;

                lock (_Properties)
                {
                    if (!_Properties.TryGetValue(objType, out properties))
                    {
                        properties = objType.GetProperties().Where(property => property.CanWrite);
                        _Properties.Add(objType, properties);
                    }
                }

                var list = new List<T>(table.Rows.Count);

                foreach (var row in table.AsEnumerable())
                {
                    var obj = new T();

                    foreach (var prop in properties)
                    {
                        try
                        {
                            prop.SetValue(obj, Convert.ChangeType(row[prop.Name], prop.PropertyType), null);
                        }
                        catch
                        {
                        }
                    }

                    list.Add(obj);
                }

                return list;
            }
            catch
            {
                return Enumerable.Empty<T>();
            }
        }
        #endregion

    } 


}
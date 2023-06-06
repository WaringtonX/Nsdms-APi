

using java.beans;
using NPOI.SS.Formula.Functions;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace mersetaWebAPI.Extentions
{
    public class DataTableToObjectConverter
    {
        // function that set the given object from the given data row
        public static void SetItemFromRow<T>(T item, DataRow row) where T : new()
        {
            // go through each column
            var pList = item.GetType().GetProperties();
            foreach (DataColumn c in row.Table.Columns)
            {
                // find the property for the column
                System.Reflection.PropertyInfo p = pList.Where(x => x.Name == c.ColumnName).FirstOrDefault();

                // if exists, set the value
                if (p != null && row[c] != DBNull.Value)
                {
                    p.SetValue(item, row[c], null);
                }
            }
        }

        // function that creates an object from the given data row
        public static T CreateItemFromRow<T>( DataRow row) where T : new()
        {
            // create a new object
            T item = new T();

            // set the item
            SetItemFromRow(item, row);

            // return 
            return item;
        }

        // function that creates a list of an object from the given data table
        public static List<T> CreateListFromTable<T>(DataTable tbl) where T : new ()
        {
            // define return list
            List<T> lst = new List<T>();

            // go through each row
            foreach (DataRow r in tbl.Rows)
            {
                // add to the list
                lst.Add(CreateItemFromRow<T>(r));
            }

            // return the list
            return lst;
        }
           }
}

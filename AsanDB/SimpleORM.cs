using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace AsanDB
{
    class SimpleORM
    {
        private Db db;

        // The table
        private string table;

        // The primary key of the table
        private string pk;

        // The prefix used to distinguish certain properties as columns
        protected char c_prefix = '_';

        public string table_
        {
            set { this.table = value; }
        }
        
        public string pk_ {
            set { this.pk = value; }
        }

        private List<string> internProperties;

        public Dictionary<string,string> Properties;
     
        public SimpleORM() {
            db = new Db();
            Properties = new Dictionary<string, string>();
            internProperties = new List<string>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SimpleORM find(int id = 0) 
        {
            if (id != 0) 
            { 
                string sql = "SELECT * FROM " + this.table + " WHERE " + this.pk + "= @pk LIMIT 1" ;

                DataTable d  = this.db.query(sql, new string[] { "pk", id.ToString() });
                
                Dictionary<string, string> row = new Dictionary<string, string>();

                if (d.Rows.Count > 0) 
                { 
                    for (int i = 0; i < d.Columns.Count; i++)
                    {
                        row.Add(this.c_prefix + d.Columns[i].ColumnName.ToLower(), d.Rows[0][i].ToString());
                    }
                }

                foreach (PropertyInfo pi in this.GetType().GetProperties())
                {
                    string propertyName = pi.Name;

                    if (pi != null && pi.CanWrite)
                    {
                        if (row.ContainsKey(propertyName))
                        {
                            if (pi.PropertyType == typeof(string))
                            {
                                pi.SetValue(this, row[propertyName]);
                            }
                            if (pi.PropertyType == typeof(int))
                            {
                                pi.SetValue(this, int.Parse(row[propertyName]));
                            }
                        }
                    }
                }

                this.internProperties.Clear();
                this.Properties.Clear();
            }

            return this;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Number of affacted rows</returns>
        public int save(int id = 0)
        {
            bindProperties(this);

            if (this.Properties.Count > 0) 
            {
                if (id != 0) {
                    this.Properties[this.pk] = id.ToString();
                }

                string update = string.Empty;

                foreach (string column in this.Properties.Keys)
                {
                    if(column != this.pk)
                    update += column + " =@"+ column+" , ";
                }

                update = update.Trim();
                update =  update.Substring(0, update.Length - 1);

                string sql = "UPDATE "  + this.table +  " SET " + update + " WHERE "  + this.pk + "= @pk LIMIT 1" ;

                int pk_index_intern = this.internProperties.IndexOf(this.pk);

                this.internProperties.RemoveAt(pk_index_intern);
                this.internProperties.RemoveAt(pk_index_intern);

                this.internProperties.Add("@pk");
                this.internProperties.Add(this.Properties[this.pk]);
                
                return this.db.nQuery(sql, this.internProperties.ToArray());
            }

            return 0;
        }

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Number of affacted rows</returns>
        public int delete(int id = 0)
        {
            if (id > 0) {

                string sql = "DELETE FROM " + this.table + " WHERE " + this.pk + "= @pk LIMIT 1";
                return this.db.nQuery(sql, new string[] {"pk", id.ToString()});
            }

            return 0;
        }

        /// <summary>
        ///  To-do : test
        /// </summary>
        /// <returns>Number of affacted rows</returns>
        public int create() 
        {
            if (this.Properties.Count == 0) { 
                bindProperties(this);
            }

            if (this.Properties.Count > 0)
            {
                // The column names
                List<string> fields     = this.Properties.Keys.ToList();

                // The value of the columns
                List<string> fieldsvals = this.Properties.Values.ToList();

                int pk_index = fields.IndexOf(this.pk);
                int pk_index_intern = this.internProperties.IndexOf(this.pk);

                // Remove the primary key, we don't need that in an insert query
                fields.RemoveAt(pk_index);
                fieldsvals.RemoveAt(pk_index);

                this.internProperties.RemoveAt(pk_index_intern);
                this.internProperties.RemoveAt(pk_index_intern);

                string sFields = String.Join(",", fields);

                for (int i = 0; i < fieldsvals.Count; i++)
                {
                    fields[i] = "@" + fields[i];
                }

                string sql = "INSERT INTO " + this.table + "(" + sFields + ")" + " VALUES(" + String.Join(",", fields) + ")";

                int result = this.db.nQuery(sql, this.internProperties.ToArray());

                this.Properties.Clear();
                this.internProperties.Clear();

                return result;
            }
            else 
            {
                string sql = "INSERT INTO " + this.table + "()" + " VALUES()";
                return this.db.nQuery(sql);
            }
        }

        /// <summary>
        /// To-do: test
        /// </summary>
        /// <param name="o"></param>
        /// <returns>Number of affacted rows</returns>
        public int create(object[] obj) {

            foreach (object o in obj)
            {
                bindProperties(o);
                this.create();
            }

            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int create(List<object> obj) 
        {
            foreach (object o in obj)
            {
                bindProperties(o);
                this.create();
            }

            return 0;
        }

        /// <summary>
        /// </summary>
        /// <returns>Datatable</returns>
        public DataTable all()
        {
            return this.db.table(this.table);
        }


        // Aggregates methods

        private double aggregateInit(string sql)
        {
            string value = db.single(sql);

            double result;

            if (value != null && double.TryParse(value, out result))
            {
                return double.Parse(value);
            }

            return 0;
        }

        public double min(string field) 
        {
            return aggregateInit("SELECT min(" + field + ")" + " FROM " + this.table);
        }

        public double max(string field)
        {
            return aggregateInit("SELECT max(" + field + ")" + " FROM " + this.table);
        }

        public double avg(string field)
        {
            return Double.Parse(aggregateInit("SELECT avg(" + field + ")" + " FROM " + this.table).ToString());
        }

        public double sum(string field)
        {
            return aggregateInit("SELECT sum(" + field + ")" + " FROM " + this.table);
        }

        public double count(string field, int limit = 0)
        {
            if (limit > 0) 
            {
                return aggregateInit("SELECT count(" + field + ")" + " FROM " + this.table + " LIMIT " + limit.ToString());
            }

            return aggregateInit("SELECT count(" + field + ")" + " FROM " + this.table);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="o"></param>
        private void bindProperties(object o) 
        {
            foreach (PropertyInfo pi in  o.GetType().GetProperties())
            {
                // Table column prefix
                if (pi.Name[0] == this.c_prefix)
                {
                    object property = pi.GetValue(o, null);
                    if (property != null)
                    {
                        this.Properties[pi.Name.Substring(1)] = property.ToString();

                        this.internProperties.Add(pi.Name.Substring(1));
                        this.internProperties.Add(property.ToString());
                    }
                }
            }        
        }
    }
}

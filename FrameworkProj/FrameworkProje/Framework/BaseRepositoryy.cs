using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Framework
{
    public class BaseRepositoryy<T> : IBaseRepository<T> where T : class
    {
        
        string tableName = typeof(T).Name;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Baglanti"].ConnectionString);
        public void Create(T entity, bool Idremove)
        {
            var props = GetProperties();
            if(Idremove) { props.RemoveAt(0); }
            string cols = GetInsertColumns(props);
            string val = GetVal(props);
            string qry = $"insert into {tableName} {cols} {val}  ";
            con.Execute(qry, entity);
           
        }

        private string GetVal(List<PropertyInfo> props)
        {
            string val = "values (";
            foreach (var item in props)
            {
                val += "@" + item.Name + ",";
            }
            val = val.Remove(val.Length - 1, 1);
            val += ")";
            return val;
        }

        //insert into Unvan (UnvanAd) Values(@UnvanAd)

        private string GetInsertColumns(List<PropertyInfo> props)
        {
            string col = "(";
            foreach (var item in props)
            {
                    col += item.Name + ",";
            }
            col = col.Remove(col.Length - 1, 1);
            col += ")";
            return col;
        }

        public void Delete(dynamic id)
        {
            var props = GetProperties();
            string key = props[0].Name;
            con.ExecuteScalar<int>($"delete  from {tableName} where {key} = {id}");
        }

        public T Find(dynamic id)
        {
            string key = typeof(T).Name + "id";
            return con.Query<T>($"select * from {tableName} where {key} = {id}").First();
        }

        public T Find(dynamic id, string key)
        {
            key = typeof(T).Name + "id";
            return con.Query<T>($"select * from {tableName} where {key} = {id}").First();
        }
        public List<PropertyInfo> GetProperties()
        {
            var props = typeof(T).GetProperties().ToList();
            return props;
        }

        public List<T> List()
        {
            return con.Query<T>($"Select * from {tableName}").ToList();

        }
        public List<T> List(string tablename)
        {
            return con.Query<T>($"Select * from {tablename}").ToList();
            
            
        }

        public void Update(T entity, dynamic Id)
        {
            var props = GetProperties();
            string key = props[0].Name;
            props.RemoveAt(0);
            string val = GetUpdateColumns(props);
            string where = "where";
            string qry = $"update {tableName} {val} {where}   {key} = {Id} ";
            con.Execute(qry, entity);
        }

        private string GetUpdateColumns(List<PropertyInfo> props)
        {
            string val = "set ";
            foreach (var item in props)
            {
                val += item.Name + " " + "=" + "@" + item.Name + ",";
            }
            val = val.Remove(val.Length - 1, 1);
            return val;
        }
    }
}

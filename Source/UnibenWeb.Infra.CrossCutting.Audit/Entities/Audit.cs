using System;
using System.IO;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Infrastructure;
using XmlSerializer = System.Xml.Serialization.XmlSerializer;
using Newtonsoft.Json;
using UnibenWeb.Infra.CrossCutting.Audit.JsonConfig;

namespace UnibenWeb.Infra.CrossCutting.Audit.Entities
{

    public interface IDescribableEntity
    {
        // Override this method to provide a description of the entity for audit purposes
        string Describe();
        string Serialize(object obj);
    }

    public class Audit : IDescribableEntity
    {
        public Audit(DbEntityEntry newEntity)
        {
            AuditID = Guid.NewGuid();
            Created_date = DateTime.Now;
            RecordObj = (DbEntityEntry) newEntity;
            OriginalValue = null;
            NewValue = Serialize(newEntity.Entity);
        }

        public Audit()
        {
            AuditID = Guid.NewGuid();
            Created_date = DateTime.Now;
            //RecordObj = (DbEntityEntry) newEntity;
            RecordObj = null;
            //OriginalValue = null;
            //NewValue = Serialize(newEntity.Entity);
        }

        [Key]
        public Guid AuditID { get; set; }
        public string EventType { get; set; }
        public string TableName { get; set; }
        public string ActionID { get; set; }
        public string RecordID { get; set; }
        public string ColumnName { get; set; }
        public string OriginalValue { get; set; }
        public string NewValue { get; set; }
        public string Created_by { get; set; }
        public DateTime Created_date { get; set; }

        public virtual DbEntityEntry RecordObj { get; set; }

        public string Describe()
        {
            return "{ ItemID : \"" + AuditID + "\", ItemDescription : \"" + NewValue + "\" }";
        }

        public string Serialize(object obj)
        {
            return SerializeJson(obj);
            //return SerializeXml(obj);
        }

        private static string SerializeXml(object obj)
        {
            XmlSerializer xs = new XmlSerializer(obj.GetType());
            using (MemoryStream buffer = new MemoryStream())
            {
                xs.Serialize(buffer, obj);
                return ASCIIEncoding.ASCII.GetString(buffer.ToArray());
            }
        }

        private static string SerializeJson(object obj)
        {
            var jsonSerializer = new JsonSerializer
            {
                MissingMemberHandling = MissingMemberHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                PreserveReferencesHandling = PreserveReferencesHandling.None
            };

            jsonSerializer.ContractResolver = new CustomResolver();

            var sb = new StringBuilder();
            using (var sw = new StringWriter(sb))
            using (var jtw = new JsonTextWriter(sw))
            jsonSerializer.Serialize(jtw, obj);

            return sb.ToString();
        }
    }
}
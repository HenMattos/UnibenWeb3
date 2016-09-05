using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Threading.Tasks;
using UnibenWeb.Infra.CrossCutting.Audit.Entities;
using UnibenWeb.Infra.Data.EntityConfig;

namespace UnibenWeb.Infra.CrossCutting.Audit.Context
{
    public class UnibenLogContext : DbContext
    {
        public UnibenLogContext() : base("UnibenConnection")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<UnibenLogContext>());
        }
        public DbSet<Entities.Audit> Logs { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            // ~~~ 
            // Precisa comentar o modelBuilder para gerar o scatfold, pois ele não entende.

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Properties()
                .Where(p => p.Name == p.ReflectedType.Name + "Id")
                .Configure(p => p.IsKey());

            modelBuilder.Properties<string>()
                .Configure(p => p.HasColumnType("varchar"));

            modelBuilder.Properties<string>()
                .Configure(p => p.HasMaxLength(100));

            modelBuilder.Configurations.Add(new LogEFConfig());

            //modelBuilder.Configurations.Add(new ContratoEFConfig());
            // ~~~

            base.OnModelCreating(modelBuilder);
        }
        public void DoChanges(IEnumerable<DbEntityEntry> entityEntries, string userId)
        {
            // Get all Added/Deleted/Modified entities (not Unmodified or Detached)
            foreach (var ent in entityEntries.Where(p => p.State == EntityState.Added || p.State == EntityState.Deleted || p.State == EntityState.Modified))
            {
                // For each changed record, get the audit record entries and add them
                foreach (var rec in GetAuditRecordsForChange(ent, userId))
                {
                    Logs.Add(rec);
                }
            }
            // Call the original SaveChanges(), which will save both the changes made and the audit records
            //return base.SaveChanges();
        }

        public override int SaveChanges()
        {
            foreach (var ent in ChangeTracker.Entries())
            {
                // Get primary key value (If you have more than one key column, this will need to be adjusted)
                //var props = ent.Entity.GetType().GetProperties();
                //var keyName = props.Single(p => p.GetCustomAttributes(typeof(KeyAttribute), false).Any()).Name;


                //ent.GetDatabaseValues();
                var targetObj = (DbEntityEntry) ent.Property("RecordObj").CurrentValue;
                if (targetObj == null)
                {
                    //var props = ent.Entity.GetType().GetProperties();
                    //var keyName = props.Single(p => p.GetCustomAttributes(typeof(KeyAttribute), false).Any()).Name;
                    //ent.Property("RecordID").CurrentValue = ent.CurrentValues.GetValue<object>(keyName).ToString();
                }
                else if (targetObj.State != EntityState.Detached) //(targetObj.State == EntityState.Added)
                {
                    // se nao for removido, atualizar atributo chave
                    var props = targetObj.Entity.GetType().GetProperties();
                    var keyName = props.Single(p => p.GetCustomAttributes(typeof(KeyAttribute), false).Any()).Name;
                    ent.Property("RecordID").CurrentValue = targetObj.CurrentValues.GetValue<object>(keyName).ToString();
                }
            }
            try
            {
                return base.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException e)
            {
                var outputLines = new List<string>();
                foreach (var eve in e.EntityValidationErrors)
                {
                    outputLines.Add(string.Format(
                        "{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:",
                        DateTime.Now, eve.Entry.Entity.GetType().Name, eve.Entry.State));
                    foreach (var ve in eve.ValidationErrors)
                    {
                        outputLines.Add(string.Format(
                            "- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage));
                    }
                }
                //Write to file
                System.IO.File.AppendAllLines(@"c:\temp\errors.txt", outputLines);
                throw;

                // Showing it on screen
                throw new Exception(string.Join(",", outputLines.ToArray()));
            }
            //throw new InvalidOperationException("User ID é Obrigatório!");
        }

        private static IEnumerable<Entities.Audit> GetAuditRecordsForChange(DbEntityEntry dbEntry, string userId)
        {
            List<Entities.Audit> result = new List<Entities.Audit>();

            // Get the Table() attribute, if one exists
            var tableAttr = dbEntry.Entity.GetType().GetCustomAttributes(typeof(TableAttribute), false).SingleOrDefault() as TableAttribute;

            // Get table name (if it has a Table attribute, use that, otherwise get the pluralized name)
            var tableName = tableAttr != null ? tableAttr.Name : dbEntry.Entity.GetType().Name;



            if (dbEntry.State == EntityState.Added)
            {
                // For Inserts, just add the whole record
                // If the entity implements IDescribableEntity, use the description from Describe(), otherwise use ToString()
                result.Add(new Entities.Audit(dbEntry)
                {
                    //LogID = Guid.NewGuid(),
                    EventType = "A", // Added
                    TableName = tableName,
                    //RecordID = dbEntry.CurrentValues.GetValue<object>(keyName).ToString(),  // Again, adjust this if you have a multi-column key
                    //RecordObj = dbEntry,
                    //ColumnName = "*ALL",    // Or make it nullable, whatever you want
                                            //NewValue = (dbEntry.CurrentValues.ToObject() is IDescribableEntity) ? (dbEntry.CurrentValues.ToObject() as IDescribableEntity).Describe() : dbEntry.CurrentValues.ToObject().ToString(),
                    Created_by = userId
                    //,Created_date = changeTime
                });
            }
            else if (dbEntry.State == EntityState.Deleted)
            {
                var props = dbEntry.Entity.GetType().GetProperties();
                var keyName = props.Single(p => p.GetCustomAttributes(typeof(KeyAttribute), false).Any()).Name;
                // Same with deletes, do the whole record, and use either the description from Describe() or ToString()
                result.Add(new Entities.Audit(dbEntry)
                {
                    //LogID = Guid.NewGuid(),
                    EventType = "D", // Deleted
                    TableName = tableName,
                    RecordID = dbEntry.OriginalValues.GetValue<object>(keyName).ToString(),
                    //RecordObj = dbEntry,
                    //ColumnName = "*ALL",
                    //NewValue = (dbEntry.OriginalValues.ToObject() is IDescribableEntity) ? (dbEntry.OriginalValues.ToObject() as IDescribableEntity).Describe() : dbEntry.OriginalValues.ToObject().ToString(),
                    Created_by = userId
                });
            }
            else if (dbEntry.State == EntityState.Modified)
            {
                var originalEntry = dbEntry.GetDatabaseValues();
                foreach (var propertyName in dbEntry.OriginalValues.PropertyNames)
                {
                    // For updates, we only want to capture the columns that actually changed
                    if (!Equals(originalEntry.GetValue<object>(propertyName), dbEntry.CurrentValues.GetValue<object>(propertyName)))
                    {
                        var props = dbEntry.Entity.GetType().GetProperties();
                        var keyName = props.Single(p => p.GetCustomAttributes(typeof(KeyAttribute), false).Any()).Name;
                        result.Add(new Entities.Audit()
                        {
                            EventType = "M",
                            TableName = tableName,
                            RecordID = dbEntry.OriginalValues.GetValue<object>(keyName).ToString(),
                            ColumnName = propertyName,
                            OriginalValue = originalEntry.GetValue<object>(propertyName).ToString(),
                            NewValue = dbEntry.CurrentValues.GetValue<object>(propertyName).ToString(),
                            Created_by = userId
                        });
                    }
                }
            }
            // Otherwise, don't do anything, we don't care about Unchanged or Detached entities

            return result;
        }
    }
}
using System.Data.Entity.ModelConfiguration;
using com.gpit.Model;

namespace com.gpit.DataContext.Mapping
{
    public class prl_arrear_configurationMap : EntityTypeConfiguration<prl_arrear_configuration>
    {
        public prl_arrear_configurationMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.reason)
                .HasMaxLength(20);

            this.Property(t => t.created_by)
                .HasMaxLength(50);

            this.Property(t => t.updated_by)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("prl_arrear_configuration", "payroll_system");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.emp_id).HasColumnName("emp_id");
            this.Property(t => t.arrear_amount).HasColumnName("arrear_amount");
            this.Property(t => t.effective_from).HasColumnName("effective_from");
            this.Property(t => t.reason).HasColumnName("reason");
            this.Property(t => t.created_by).HasColumnName("created_by");
            this.Property(t => t.created_date).HasColumnName("created_date");
            this.Property(t => t.updated_by).HasColumnName("updated_by");
            this.Property(t => t.updated_date).HasColumnName("updated_date");

            // Relationships
            this.HasRequired(t => t.prl_employee)
                .WithMany(t => t.prl_arrear_configuration)
                .HasForeignKey(d => d.emp_id);

        }
    }
}
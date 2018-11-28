using System.Data.Entity.ModelConfiguration;
using com.gpit.Model;

namespace com.gpit.DataContext.Mapping
{
    public class prl_upload_allowanceMap : EntityTypeConfiguration<prl_upload_allowance>
    {
        public prl_upload_allowanceMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.created_by)
                .HasMaxLength(50);

            this.Property(t => t.updated_by)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("prl_upload_allowance", "payroll_system");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.allowance_name_id).HasColumnName("allowance_name_id");
            this.Property(t => t.emp_id).HasColumnName("emp_id");
            this.Property(t => t.salary_month_year).HasColumnName("salary_month_year");
            this.Property(t => t.amount).HasColumnName("amount");
            this.Property(t => t.percentage).HasColumnName("percentage");
            this.Property(t => t.effective_from).HasColumnName("effective_from");
            this.Property(t => t.effective_to).HasColumnName("effective_to");
            this.Property(t => t.created_by).HasColumnName("created_by");
            this.Property(t => t.created_date).HasColumnName("created_date");
            this.Property(t => t.updated_by).HasColumnName("updated_by");
            this.Property(t => t.updated_date).HasColumnName("updated_date");

            // Relationships
            this.HasRequired(t => t.prl_allowance_name)
                .WithMany(t => t.prl_upload_allowance)
                .HasForeignKey(d => d.allowance_name_id);
            this.HasRequired(t => t.prl_employee)
                .WithMany(t => t.prl_upload_allowance)
                .HasForeignKey(d => d.emp_id);

        }
    }
}
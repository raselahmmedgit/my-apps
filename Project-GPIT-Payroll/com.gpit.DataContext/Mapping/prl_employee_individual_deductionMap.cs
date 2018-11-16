using System.Data.Entity.ModelConfiguration;
using com.gpit.Model;

namespace com.gpit.DataContext.Mapping
{
    public class prl_employee_individual_deductionMap : EntityTypeConfiguration<prl_employee_individual_deduction>
    {
        public prl_employee_individual_deductionMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.created_by)
                .HasMaxLength(50);

            this.Property(t => t.updated_by)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("prl_employee_individual_deduction", "payroll_system");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.emp_id).HasColumnName("emp_id");
            this.Property(t => t.deduction_name_id).HasColumnName("deduction_name_id");
            this.Property(t => t.flat_amount).HasColumnName("flat_amount");
            this.Property(t => t.percentage).HasColumnName("percentage");
            this.Property(t => t.effective_from).HasColumnName("effective_from");
            this.Property(t => t.effective_to).HasColumnName("effective_to");
            this.Property(t => t.created_by).HasColumnName("created_by");
            this.Property(t => t.created_date).HasColumnName("created_date");
            this.Property(t => t.updated_by).HasColumnName("updated_by");
            this.Property(t => t.updated_date).HasColumnName("updated_date");

            // Relationships
            this.HasRequired(t => t.prl_deduction_name)
                .WithMany(t => t.prl_employee_individual_deduction)
                .HasForeignKey(d => d.deduction_name_id);
            this.HasRequired(t => t.prl_employee)
                .WithMany(t => t.prl_employee_individual_deduction)
                .HasForeignKey(d => d.emp_id);

        }
    }
}

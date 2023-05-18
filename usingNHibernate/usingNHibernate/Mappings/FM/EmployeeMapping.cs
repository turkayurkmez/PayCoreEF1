using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace usingNHibernate.Mappings.FM
{
    public class EmployeeMapping : ClassMapping<Employee>
    {
        public EmployeeMapping()
        {
            Id(e => e.Id, mapper => mapper.Generator(Generators.HighLow));
            Property(e => e.Name);
            Property(e => e.LastName);

            OneToOne(e => e.ResidentAddress, mapper =>
            {
                mapper.Cascade(Cascade.Persist);
                mapper.PropertyReference(a => a.Employee);
            });

            //Set(e => e.Benefits, mapper =>
            //{
            //    mapper.Key(k => k.Column("EmployeeId"));
            //    mapper.Cascade(Cascade.All);
            //});

            /*
             */
        }
    }
}

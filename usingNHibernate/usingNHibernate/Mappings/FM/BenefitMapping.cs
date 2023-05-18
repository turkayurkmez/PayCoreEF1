using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace usingNHibernate.Mappings.FM
{
    public class BenefitMapping : ClassMapping<Benefit>
    {
        public BenefitMapping()
        {
            Id(b => b.Id, _mapper => _mapper.Generator(Generators.HighLow));
            Property(b => b.Name);
            Property(b => b.Description);
            ManyToOne(b => b.Employee, mapping =>
            {
                mapping.Class(typeof(Employee));
                mapping.Column("EmployeeId");
                mapping.Unique(true);
            });

            /*
             * 
            ManyToOne(a => a.Employee, _mapper =>
            {
                _mapper.Class(typeof(Employee));
                _mapper.Column("EmployeeId");
                _mapper.Unique(true);
            });
             */
        }
    }


    public class LeaveMapping : JoinedSubclassMapping<Leave>
    {
        public LeaveMapping()
        {
            Key(k => k.Column("Id"));
            Property(l => l.Type);
            Property(l => l.Available);
            Property(l => l.Remaining);


        }
    }

    public class FoodTicketMapping : JoinedSubclassMapping<FoodTicket>
    {
        public FoodTicketMapping()
        {
            Key(k => k.Column("Id"));
            Property(f => f.Amount);
            Property(f => f.StartDate);
            Property(f => f.EndDate);


        }
    }

}

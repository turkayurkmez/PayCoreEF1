namespace usingNHibernate
{
    public class Employee : EntityBase
    {


        public virtual string Name { get; set; }
        public virtual string LastName { get; set; }
        public virtual Address ResidentAddress { get; set; }

        public virtual ICollection<Benefit> Benefits { get; set; }
    }
}

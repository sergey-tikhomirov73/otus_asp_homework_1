namespace Otus.Teaching.PromoCodeFactory.WebHost.Models
{
    public interface ITestEntity
    {
       public string Code { get; }
     //   public void OutPutName();
    }
    public interface ITestEntityTransient: ITestEntity { }
    public interface ITestEntityScoped: ITestEntity { }
    public interface ITestEntitySingleton : ITestEntity { }
    class TestEntity : ITestEntityTransient, ITestEntityScoped, ITestEntitySingleton
    { 
    public string Code { get; set; }
        public TestEntity()
        { 
         Code=this.GetHashCode().ToString();
        }  
        
    }
}

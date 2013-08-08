using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SInnovations.Composite.Data.Mapping.Internal;
using System.Linq;
using SInnovations.Composite.Data;

namespace S_Innovations.Composite.Tests
{
    public interface IData 
    {
        Guid ID{get;set;}
    }
    public interface MyC1DataType : IData
    {
        string PropertyTwo { get; set; }
    }

    /// <summary>
    /// Represent that class C1 wraps the IData in.
    /// </summary>
    internal class HiddenImp : MyC1DataType
    {
        public HiddenImp(string str)
        {
            ID = Guid.NewGuid();
            PropertyTwo = str;
        }

        public string PropertyTwo
        { get; set; }

        public Guid ID
        { get; set; }
    }
    public class MyClass
    {
        public string PropertyTwo { get; set; }
    }

    [TestClass]
    public class IQueryableMappingTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            IQueryable<MyC1DataType> data = new MyC1DataType[] { new HiddenImp("Hej"), new HiddenImp("med"), new HiddenImp("Dig") }.AsQueryable();
            IQueryable<MyClass> mappedData = data.Map().To<MyClass>();
            IQueryable<MyClass> mappedData1 = data.Map().To<MyClass>();

            for (int i = 0; i < data.Count(); i++)
            {
                Assert.AreEqual(data.ElementAt(i).PropertyTwo, mappedData.ElementAt(i).PropertyTwo);
            }            

        }
    }
}

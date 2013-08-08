sinnovations-composite-contrib
==============================


DATA
====
Heres an example for the AutoMapper. 
It builds a lambda expression for all matching properties with same name and type on the first query, and reuses this in the future.

Its the initial commit. Planning to add some attributes that you can put on the Static Data types for configuring the mapping rules.

```
  IQueryable<MyC1DataType> data = new MyC1DataType[] { new HiddenImp("Hej"), new HiddenImp("med"), new HiddenImp("Dig") }.AsQueryable();
  IQueryable<MyClass> mappedData = data.Map().To<MyClass>();
  IQueryable<MyClass> mappedData1 = data.Map().To<MyClass>();
  
  for (int i = 0; i < data.Count(); i++)
  {
      Assert.AreEqual(data.ElementAt(i).PropertyTwo, mappedData.ElementAt(i).PropertyTwo);
  }  
  ```

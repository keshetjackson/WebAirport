Web Terminal Server

logic - 
so the logic of the application is basiclly a data structure with its own logic for specific nodes. 
this data structure is automatic, meaning whener you pass a value (flight) to the first node (leg), each node will handle all the neccessery
things like:
- checking if the next node is free.
- wait a certain amount of time. 
- pass the value to the next node.
- save to database.
- have a unique logic 
this logic was designed using several design patterns: 
- Singletone : each node is created only once thru the application life time and each node is threadsafe.
- States : each node is checking the state of the next node to decide if it can pass the value or wait.
- factory pattern : nodes (legs) are creating thru a generic singletone thread safe leg factory to enable global access to the legs
and keeping them unique and thread safe.

 Repository - 
 the repository of the application is using new instance of the datacontext everytime with the datacontext factory in order to keep the 
 context thread safe and clean.

  Service - 
  the service of the application is rather small, it is being used only to pass a flight from the constructor to the data structure,
  it checks if the fligth is arrival or departure and pass it on to the appropiate node, from there the data structure is completly
  automatic and self managed.
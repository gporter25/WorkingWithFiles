# About

Example unit test against comma delimited files.



### Test data
There are two types of data, public data from California and exported NorthWind data.

![img](../assets/Stop.png)

Each **.CSV** have been setup for **specific test** so do not change the contents else one or more test will fail.

### Important notes

In Base.NorthUnitTests class in `Initialization` method

- `FileName` property changes dependent on before a specific test method executes
- `CustomerItemsList` is initialized dependent on before a specific test method executes
- `InvalidIndices` is initialized dependent on before a specific test method executes
- Events in Base.NorthUnitTests need to be unscribed after each use
## Data schema

MashineParts (Id int, Name string)

Regions (Id int, Name string)

Suppliers (Id int, Name string)

Deliveries (Id int, RegionId int, SupplierId int, [Date] Date)

DeliveryLines (Id int, DeliveryId int, MachinePartId int, Price int, Quantity int)

## Queries

* get list of suppliers’ names that made at least one delivery to region with name "A"

```
select distinct s.name from suppliers s
inner join deliveries d on s.id = d.supplierid
inner join regions r on d.regionid = r.id
where r.name = 'A'
```

* get summary value for all parts supplied by supplier "X" during January 2000

```
select sum(dl.price) from deliverylines dl
inner join deliveries d on dl.deliveryid = d.id
inner join suppliers s on d.supplierid = s.id
where s.name = 'X' and d.[date] >= '2000-01-01' and d.[date] <= '2000-01-30'
```

* get number of deliveries with total cost more than 1000 USD (total cost is sum of price times number of parts by all lines for this delivery), grouped by supplier (result table should have columns "Supplier name" and "Delivery number")

```
select s.Name as 'Supplier name', d.id as 'Delivery number' from deliveries d
inner join suppliers s on d.supplierid = s.id
where (select sum(price) from deliverylines dl where dl.deliveryid = d.id) >= 1000
```

* get all supplier names who had not made any deliveries to regions where supplier "X" has any delivery

```
select distinct s.name from suppliers s
inner join deliveries d on s.id = d.supplierid
inner join regions r on d.regionid = r.id
left join (
	select r1.* from regions r1
	inner join deliveries d1 on r1.id = d1.supplierid
	inner join suppliers s1 on d1.regionid = s1.id
	where s1.name = 'X') ss on r.id = ss.id
where ss.id is null
```

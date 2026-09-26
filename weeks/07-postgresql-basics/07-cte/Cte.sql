-- 1: Подтверждённые бронирования с именами клиентов
with confirmed_bookings as 
(
	select b.id booking_id, c.id customer_id
	from bookings b join customers c on b.customer_id = c.id
	where b.status = 'confirmed'
)
select cb.booking_id, cb.customer_id, c.display_name  
from confirmed_bookings cb
join customers c on cb.customer_id = c.id
order by cb.booking_id;

-- 2: Стоимость бронирований от 1000
with booking_totals as
(
	select bi.booking_id, sum(bi.ticket_count * bi.unit_price) booking_total
	from booking_items bi 
	group by bi.booking_id
)
select bt.booking_id, bt.booking_total 
from booking_totals bt
where bt.booking_total >= 1000
order by 2 desc, 1 asc;

-- 3: Все бронирования, включая пустые
with booking_totals as 
(
	select bi.booking_id, sum(bi.ticket_count  * bi.unit_price) booking_total 
	from booking_items bi 
	group by bi.booking_id 
)
select b.id booking_id, b.customer_id, b.status, coalesce(bt.booking_total, 0) as booking_total
from bookings b
left join booking_totals bt on bt.booking_id = b.id
order by booking_id;

-- 4: Все клиенты и суммы подтверждённых бронирований
with 
confirmed_bookings as
(
	select b.id, b.customer_id 
	from bookings b 
	where b.status = 'confirmed'
),
customer_totals as
(
	select cb.customer_id, sum(bi.booking_id * bi.ticket_count) as customer_totals  
	from booking_items bi join confirmed_bookings cb on bi.booking_id = cb.id
	group by cb.customer_id 
)
select ct.customer_id, c.display_name, ct.customer_totals 
from customer_totals ct
join customers c on ct.customer_id = c.id;

-- 5: Бронирования дороже среднего
with booking_totals as
(
	select bi.booking_id, sum(bi.ticket_count * bi.unit_price) as booking_total
	from booking_items bi 
	group by bi.booking_id 
)
select bt.booking_id, bt.booking_total 
from booking_totals bt
where bt.booking_total > (select avg(booking_total) from booking_totals)
order by bt.booking_total desc, bt.booking_id asc;

-- 6: Сводка по каждому клиенту
with 
booking_totals as
(
	select 
		b.id as booking_id, 
		b.customer_id, 
		coalesce(sum(bi.ticket_count * bi.unit_price), 0) as booking_total
	from booking_items bi right join bookings b on b.id = bi.booking_id
	group by b.id, b.customer_id
),
customer_summary as 
(
	select 
		bt.customer_id, 
		count(*) as booking_count, 
		sum(bt.booking_total) as total_amount
	from booking_totals bt
	group by 1
)
select 
	c.id customer_id, 
	c.display_name, 
	coalesce(cs.booking_count, 0) as booking_count,
	coalesce(cs.total_amount, 0) as total_amount
from customers c left join customer_summary cs on cs.customer_id = c.id 
order by 1;

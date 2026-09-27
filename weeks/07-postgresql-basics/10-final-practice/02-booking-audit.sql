-- Часть 2. Поиск проблемных броней — 02-booking-audit.sql
-- 2: Клиенты с бронированиями, но без единой позиции
select c.id as customer_id, c.display_name 
from customers c
where exists 
(
	select 1 from bookings b
	where b.customer_id  = c.id
)
and not exists 
(
	select 1 from booking_items bi 
	join bookings b on b.id = bi.booking_id 
	where b.customer_id = c.id
)
order by c.id;

-- 1: Подтверждённые брони без позиций
select b.id as booking_id, b.customer_id, c.display_name  
from bookings b
join customers c on b.customer_id = c.id
where b.status = 'confirmed'
and not exists 
(
	select 1 from booking_items bi
	where bi.booking_id = b.id
)
order by b.id asc;

-- Часть 1. Отчёт по клиентам — 01-customer-report.sql
with 
	confirmed_bookings as 
	(
		select c.id as customer_id, count(b.id) as confirmed_booking_count 
		from bookings b
		join customers c on b.customer_id = c.id
		where status = 'confirmed'
		group by c.id
	),
	bookings_per_customer as 
	(
		
		select c.id as customer_id, count(b.id) as booking_count
		from bookings b
		join customers c on (b.customer_id = c.id)
		group by c.id
	),
	booking_total as 
	(
		select b.customer_id, sum(bi.ticket_count * bi.unit_price) as confirmed_total
		from bookings b 
		join booking_items bi on bi.booking_id  = b.id 
		where b.status = 'confirmed'
		group by b.customer_id 
	)
select c.id as customer_id, c.display_name, coalesce(bpc.booking_count, 0) as booking_count, coalesce(cb.confirmed_booking_count, 0) as confirmed_booking_count, coalesce(bt.confirmed_total, 0) as confirmed_total
from customers c
left join bookings_per_customer bpc on bpc.customer_id = c.id 
left join confirmed_bookings cb on cb.customer_id = c.id
left join booking_total bt on bt.customer_id = c.id
ORDER BY confirmed_total DESC, c.id ASC;

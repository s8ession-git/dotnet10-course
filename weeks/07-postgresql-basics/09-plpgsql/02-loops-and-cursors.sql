-- 1
do 
$$
declare
	v_booking record;
	v_counter integer := 0;
begin
	for v_booking in
		select b.id, b.customer_id, b.status
		from bookings b
		order by b.id
	loop
		v_counter = v_counter + 1;
		raise notice 'Бронь: %, Клиент: %, Статус: %', v_booking.id, v_booking.customer_id, v_booking.status;
	end loop;
	
	raise notice 'Всего записей: %', v_counter;
	-- result: 5
end;
$$

select count(*) from bookings; 
-- result: 5

-- 2
do 
$$
declare
	v_booking record;
	v_total_confirmed_bookings int := 0;

	cur_bookings cursor for
		select b.id, b.status 
		from bookings b
		order by b.id;
begin
	open cur_bookings;

	while v_total_confirmed_bookings < 3 
	loop
		fetch cur_bookings into v_booking;
		exit when not found;
		continue when v_booking.status <> 'confirmed';

		raise notice 'Подтвержденная бронь: %', v_booking.id;
		v_total_confirmed_bookings := v_total_confirmed_bookings + 1;
	end loop;

	raise notice 'Всего: %', v_total_confirmed_bookings;
	close cur_bookings;
end;
$$

-- 3
do
$$
declare
	v_booking record;
	v_total int := 0;

	cur_bookings cursor for 
		select b.id, b.customer_id, b.status
		from bookings b
		order by b.id;
begin
	open cur_bookings;
	loop	
		fetch cur_bookings into v_booking;
		exit when not found;
		continue when v_booking.status <> 'confirmed';
		
		v_total := v_total + 1;
		raise notice 'Подтвержденная бронь: %, клиент: %', v_booking.id, v_booking.customer_id;
		exit when v_total >= 3;
	end loop;
	raise notice 'Всего записей: %', v_total;
	close cur_bookings;
end;
$$

-- 4
select * from events; 
-- 1002: 200, 0; 1003: 80, 129; 1005: 60, 25;

begin;
do 
$$
declare
	v_event record;
	v_iteration int := 0;
	v_success_total int := 0;
	v_error_total int := 0;
	v_new_price numeric(12,2);
	
begin
	for v_event in 
		select e.id, e.capacity, e.price
		from events e
		where e.id in (1002, 1003, 1005)
		order by e.id
	loop
		v_iteration := v_iteration + 1;
		begin
			update events e set price = e.price + 10 where e.id = v_event.id returning e.price into v_new_price;
	
			if v_iteration = 2 then
				update events e set capacity = 0 where e.id = v_event.id;
			end if;
			
			v_success_total := v_success_total + 1;
			raise notice 'id: %, price: % -> %', v_event.id, v_event.price, v_new_price;
	
			exception when check_violation then
					v_error_total := v_error_total + 1;
					raise notice 'id: %, изменения отменены. Код: %, Сообщение: %', v_event.id, sqlstate, sqlerrm;		
		end;
	end loop;

	raise notice E'Total: \nSuccess: %. \nError: %.', v_success_total, v_error_total;
end;
$$;

-- Проверяем результат до полного отката
SELECT id, price, capacity
FROM events
WHERE id IN (1002, 1003, 1005)
ORDER BY id;

rollback;
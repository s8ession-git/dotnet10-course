-- Часть 3. Атомарное создание брони — 03-create-booking.sql
begin;
do
$$
declare
	v_customer_id bigint := 1004; -- Dave
	v_event_id bigint := 1005; -- Legacy Systems Evening
	v_ticket_count int := 5; 
	v_selected_event_price numeric(12, 2);
	v_selected_booking_id int;
	v_total_changed_rows int;
begin
	-- 1
	if not exists (select 1 from customers where id = v_customer_id) then
		raise exception 'Не существует покупателя с id: %', v_customer_id; 
	end if;

	-- 2
	if v_ticket_count is null or v_ticket_count <= 0 then
		raise exception 'Количество билетов должно быть положительным. Текущее значение: %', v_ticket_count;
	end if;

	-- 3
	select e.price into v_selected_event_price from events e where e.id = v_event_id;
	
	if not found then
	    raise exception 'Не существует мероприятия с id: %', v_event_id;
	end if;

	-- 4 + 5
	insert into bookings (customer_id, created_at, status) values (v_customer_id, now(), 'pending') 
	returning id into v_selected_booking_id;

	-- 6
	insert into booking_items (booking_id, event_id, ticket_count, unit_price)
	values (v_selected_booking_id, v_event_id, v_ticket_count, v_selected_event_price);
	-- 7
	update bookings set status = 'confirmed' where id = v_selected_booking_id and status = 'pending';

	-- 8
	get diagnostics v_total_changed_rows = ROW_COUNT;
	if v_total_changed_rows <> 1 then
		raise exception 'Ожидалось изменение 1 брони, изменено: %', v_total_changed_rows;
	end if;
	
	-- 9
	raise notice 'booking_id: %, ticket_count: %, total: %', v_selected_booking_id, v_ticket_count, v_selected_event_price * v_ticket_count;

	-- 10
	exception when others then
        raise notice 'Код: %, сообщение: %', SQLSTATE, SQLERRM;
        raise;
end;
$$;
rollback;
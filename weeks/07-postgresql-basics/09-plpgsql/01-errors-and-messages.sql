-- 1
begin;

do $$
begin
	declare 
		v_booking_id_exists bigint := 3;
		v_status_id_exists text;
	-- А. Результат - существует
	begin
		select b.status
		into v_status_id_exists
		from bookings b
		where b.id = v_booking_id_exists;
	
		if not found then
			raise exception 'Бронь % не найдена', v_booking_id_exists;
		end if;
	
		raise notice 'Статус брони %: %', v_booking_id_exists, v_status_id_exists;
	end;

	declare
		v_booking_id_not_exists bigint := 999999;
		v_status_id_not_exists text;
	
		-- Б. Результата - нет
	begin
		select b.status
		into v_status_id_not_exists
		from bookings b
		where b.id = v_booking_id_not_exists;
	
		if not found then
			raise exception 'Бронь % не найдена', v_booking_id_not_exists;
		end if;
	
		raise notice 'Статус брони %: %', v_booking_id_not_exists, v_status_id_not_exists;
	end;
end;
$$;
rollback;


select * from bookings;
-- 2
begin;
do $$
begin
	declare
		v_booking_id_init bigint := 1002;
		v_booking_id bigint;
		v_expected_status text := 'pending';
		v_new_status text := 'confirmed';
		v_affected_rows bigint;
	begin
		update bookings set status = v_new_status 
		where id in (select id from bookings where status = v_expected_status)
		returning id into v_booking_id;
		GET DIAGNOSTICS v_affected_rows = ROW_COUNT;
		if v_affected_rows = 0 then
			raise exception 'Бронь % не подтверждена: она отсутствует или уже не pending', v_booking_id;
		end if;
		raise notice 'Изменено бронирований: %', v_affected_rows;
	end; 
	
	declare
		v_booking_id_init bigint := 1002;
		v_booking_id bigint;
		v_expected_status text := 'pending';
		v_new_status text := 'confirmed';
		v_affected_rows bigint;
	begin
		update bookings set status = v_new_status 
		where id in (select id from bookings where status = v_expected_status)
		returning id into v_booking_id;
		GET DIAGNOSTICS v_affected_rows = ROW_COUNT;
		if v_affected_rows = 0 then
			raise exception 'Бронь % не подтверждена: она отсутствует или уже не pending', v_booking_id;
		end if;
		raise notice 'Изменено бронирований: %', v_affected_rows;
	end;
end;
$$;
rollback;

-- 3
begin;

do $$
begin
	declare
		v_init_event_id bigint := 1002;
		v_init_price numeric(12, 2);
		
	begin
		select price into v_init_price
		from events
		where id = v_init_event_id;
		if not found then raise notice 'event.id = % not found', v_init_event_id; end if;
	
		update events set price = v_init_price + 100 
		where id = v_init_event_id;
	end;

	declare
		v_init_capacity int4;
		v_init_event_id bigint := 1002;
		v_init_price numeric(12, 2);

		v_constraint text;
	begin
		select capacity into v_init_capacity
		from events
		where id = v_init_event_id;
		if not found then raise notice 'event.id = % not found', v_init_event_id; end if;

		select price into v_init_price
		from events
		where id = v_init_event_id;
		if not found then raise notice 'event.id = % not found', v_init_event_id; end if;
	
		update events set price = v_init_price + 50
		where id = v_init_event_id;
		
		update events set capacity = 0 where id = v_init_event_id;
		exception
			when check_violation then get stacked diagnostics
				v_constraint =  CONSTRAINT_NAME;
				raise notice E'Код: % \nСообщение: % \nОграничение: %', sqlstate, sqlerrm, v_constraint;
	end;

		raise notice 'Внешний блок продолжает работу';
end;
$$;
select * from events where id = 1002;
rollback;
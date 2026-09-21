-- 1. events.capacity: not null
BEGIN;

UPDATE events
SET capacity = 1
WHERE capacity IS NULL;

ALTER TABLE events
    ALTER COLUMN capacity SET NOT NULL;

COMMIT;

-- 2. events.price: not null
DO $$
DECLARE
    column_nullable text;
BEGIN
    -- Не позволяет другим транзакциям изменять таблицу
    -- во время проверки и добавления ограничения.
    LOCK TABLE public.events IN ACCESS EXCLUSIVE MODE;

    SELECT is_nullable
    INTO column_nullable
    FROM information_schema.columns
    WHERE table_schema = 'public'
      AND table_name = 'events'
      AND column_name = 'price';

    IF column_nullable IS NULL THEN
        RAISE EXCEPTION
            'Столбец public.events.price не найден';

    ELSIF column_nullable = 'NO' THEN
        RAISE NOTICE
            'Столбец public.events.price уже имеет ограничение NOT NULL';

    ELSIF EXISTS (
        SELECT 1
        FROM public.events
        WHERE price IS NULL
    ) THEN
        RAISE EXCEPTION
            'Невозможно добавить NOT NULL: events.price содержит NULL';

    ELSE
        ALTER TABLE public.events
            ALTER COLUMN price SET NOT NULL;

        RAISE NOTICE
            'Ограничение NOT NULL успешно добавлено к events.price';
    END IF;
END
$$;

-- 3. booking.status: not null
begin;
update bookings set status = 'pending' where status is null;
alter table bookings alter column status set not null;
commit;

-- 4. + 5. booking_items. Обе таблицы изначально не заполнены
alter table booking_items 
	alter column ticket_count set not null,
	alter column unit_price set not null;


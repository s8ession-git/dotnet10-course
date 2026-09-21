# Курс по .NET 10

Практический репозиторий для 24-недельного маршрута изучения .NET 10.

Цель курса — последовательно пройти путь от фундаментального C# и ООП до PostgreSQL, ASP.NET Core, EF Core, архитектуры, security, тестирования, Docker/CI и итогового проекта.

## Прогресс

| Неделя | Тема | Статус |
|---|---|---|
| 01 | C# Fundamentals | ✅ Выполнено |
| 02 | Collections, Equality, Strings, Nullable | ✅ Выполнено |
| 03 | OOP | ✅ Выполнено |
| 04 | Generics, Delegates, Lambdas, Events, Exceptions, IDisposable | ✅ Выполнено |
| 05 | LINQ | ✅ Выполнено |
| 06 | Async/Await, Task, Cancellation, Concurrency | ✅ Выполнено |
| 07 | PostgreSQL / SQL I | 🚧 В работе |
| 08 | PostgreSQL / SQL II | ⏳ Запланировано |
| 09 | HTTP | ⏳ Запланировано |
| 10 | ASP.NET Core I | ⏳ Запланировано |
| 11 | ASP.NET Core II | ⏳ Запланировано |
| 12 | ASP.NET Core III | ⏳ Запланировано |
| 13 | EF Core I | ⏳ Запланировано |
| 14 | EF Core II | ⏳ Запланировано |
| 15 | EF Core III | ⏳ Запланировано |
| 16 | Architecture / CQRS I | ⏳ Запланировано |
| 17 | Architecture / CQRS II | ⏳ Запланировано |
| 18 | Security I | ⏳ Запланировано |
| 19 | Security II | ⏳ Запланировано |
| 20 | Testing I | ⏳ Запланировано |
| 21 | Testing II | ⏳ Запланировано |
| 22 | Docker | ⏳ Запланировано |
| 23 | Docker Compose / CI | ⏳ Запланировано |
| 24 | Final Project — ProjectHub | ⏳ Запланировано |

---

## Пройденные блоки

### Неделя 1 — C# Fundamentals

Фундамент языка C#: типы, переменные, выражения, управление потоком, методы и базовые правила работы компилятора.

### Неделя 2 — Collections, Equality, Strings, Nullable

Основные направления:

- коллекции;
- equality;
- строки;
- nullable reference/value semantics;
- безопасная работа с отсутствующими значениями.

### Неделя 3 — OOP

1. Encapsulation и invariants.
2. Entity, Value Object, immutability, record vs class.
3. Composition vs inheritance.
4. Inheritance и runtime polymorphism.
5. Interfaces и abstraction.
6. Итоговое проектирование объектной модели.

Ключевые идеи:

- инварианты должны следовать из предметной области;
- Entity и Value Object различаются семантикой идентичности;
- composition и inheritance решают разные задачи;
- interfaces описывают capabilities и границы абстракций;
- polymorphism не требует отказа от обычных conditionals там, где они уместны.

### Неделя 4 — Generics, Delegates, Lambdas, Events, Exceptions, IDisposable

1. Generics и constraints.
2. Delegates, `Func<>`, `Action<>`.
3. Lambdas и closures.
4. Events.
5. Exceptions и exception boundaries.
6. `IDisposable`, ownership и deterministic cleanup.
7. Итоговая интегрированная задача `Job<TPayload> / JobRunner`.

### Неделя 5 — LINQ

1. Deferred execution и materialization.
2. Filtering, projection, sorting.
3. Search и проверки: `Any`, `All`, `Contains`, `First`, `Single`.
4. `GroupBy`.
5. `Join` и `GroupJoin`.
6. Aggregation.
7. Set operations и equality.
8. Итоговая LINQ-задача.

### Неделя 6 — Async/Await, Task, Cancellation, Concurrency

#### 01 — Task, Task<T>, async, await

- `Task` ≠ `Thread`;
- выполнение async-метода до первого незавершённого `await`;
- `Task<T>` как контракт eventual result;
- `Task.Delay` vs blocking;
- последовательный `await`;
- `Task.FromResult`;
- почему `async` сам по себе не означает parallelism.

Практика: spacecraft telemetry.

#### 02 — Async composition

- async propagation вверх по call stack;
- “async all the way”;
- отличие forwarding `Task` от метода, которому нужен результат после `await`;
- зависимые и независимые операции;
- причины избегать `.Result` и `.Wait()`.

Практика: deployment pipeline.

#### 03 — Task.WhenAll / Task.WhenAny

- запуск нескольких независимых операций;
- `Task.WhenAll` как композиция завершения;
- `Task.WhenAny` и получение winning task;
- concurrency vs parallelism;
- dependency graph между async-операциями.

#### 04 — Exceptions in async / Task

- faulted tasks;
- propagation исключений через `await`;
- normal `await` vs `.Wait()/.Result`;
- `AggregateException`;
- `throw;` vs `throw ex;`;
- поведение exceptions с `Task.WhenAll` и `Task.WhenAny`.

Практика: distributed catalog import.

#### 05 — CancellationToken

- cooperative cancellation;
- `CancellationTokenSource` и `CancellationToken`;
- token вниз, `Task` вверх;
- `ThrowIfCancellationRequested`;
- `Cancel()` и `CancelAfter()`;
- canceled task vs faulted task;
- propagation cancellation по call stack;
- пример операции, которая token получает, но не наблюдает.

Практика: media processing pipeline.

#### 06 — Concurrency control / shared state

- race conditions;
- shared mutable state;
- `Interlocked`;
- `lock`;
- bounded concurrency;
- `SemaphoreSlim`;
- корректная граница `WaitAsync` / `try-finally` / `Release`;
- cancellation во время ожидания permit;
- bounded vs unbounded concurrency.

Практика: image processing farm.

Эксперименты:

- A — unbounded concurrency;
- B — race condition vs safe counter;
- C — cancellation while waiting for semaphore permit.

#### 07 — Final task

Интегрированная задача: document processing pipeline.

Закреплено:

- последовательные этапы одного документа;
- конкурентная обработка разных документов;
- bounded concurrency;
- `Task.WhenAll`;
- cancellation propagation;
- distinction между document failure и batch cancellation;
- safe shared-state accounting;
- aggregation результатов без лишнего mutable shared state;
- bounded vs unbounded comparison.

Эксперименты:

- A — normal completion;
- B — document failure без fault всего batch;
- C — cancellation;
- D — bounded vs unbounded concurrency.

---

## Неделя 7 — PostgreSQL / SQL I

Текущий план блоков:

1. Реляционная модель, таблицы, PK/FK, constraints, DDL.
2. `SELECT`, `WHERE`, `ORDER BY`, `LIMIT`, expressions и `NULL`.
3. `INSERT`, `UPDATE`, `DELETE`, `RETURNING`.
4. `JOIN`: `INNER`, `LEFT`, связи 1:N и N:M.
5. Aggregation: `COUNT`, `SUM`, `AVG`, `GROUP BY`, `HAVING`.
6. Subqueries, `EXISTS`, `IN`, CTE.
7. Transactions и базовые свойства ACID.
8. Итоговая задача недели.

---

## Дальнейший маршрут

### Неделя 8 — PostgreSQL / SQL II

Углублённая работа с PostgreSQL: индексы, планы выполнения, `EXPLAIN`, транзакционная конкуренция, блокировки и более сложный SQL.

### Неделя 9 — HTTP

HTTP model, methods, headers, status codes, request/response semantics и основы взаимодействия клиента и сервера.

### Недели 10–12 — ASP.NET Core

Построение HTTP API на ASP.NET Core: routing, controllers/endpoints, middleware, DI, configuration, validation и application boundaries.

### Недели 13–15 — Entity Framework Core

Моделирование данных и persistence через EF Core: mapping, relationships, migrations, querying и работа с PostgreSQL.

### Недели 16–17 — Architecture / CQRS

Архитектурные границы, responsibilities, dependency direction, application/use-case orchestration, CQRS и практическое проектирование приложения.

### Недели 18–19 — Security

Authentication, authorization, identity/security concepts и защита web-приложений.

### Недели 20–21 — Testing

Unit и integration testing, test boundaries, xUnit и проверка поведения приложения.

### Неделя 22 — Docker

Контейнеризация .NET-приложений и окружения.

### Неделя 23 — Docker Compose / CI

Оркестрация нескольких сервисов и основы CI pipeline.

### Неделя 24 — Final Project: ProjectHub

Финальный проект, объединяющий основные темы курса.

---

## Структура репозитория

Каждая неделя находится в отдельной директории:

```text
weeks/
  06-async-concurrency/
    01-task-async-await/
    02-async-composition/
    03-task-concurrency/
    04-async-exceptions/
    05-cancellation/
    06-concurrency-control/
    07-final-task/

  07-postgresql-basics/
  08-postgresql-advanced/
  09-http/
  10-aspnet-core-basics/
```

Практические блоки могут содержать:

- `Domain` — модели и domain concepts;
- `Application` — orchestration, contracts и use-case logic;
- `Infrastructure` — технические реализации;
- `Presentation` — взаимодействие с пользователем/console output;
- `Experiments` — отдельные эксперименты для проверки поведения изучаемого механизма;
- `Program.cs` — composition root и запуск сценария.

> Структура конкретного блока определяется учебной задачей и не обязана использовать все слои одновременно.

List<DocumentFile> documentListA = new List<DocumentFile>();
List<DocumentFile> documentListB = new List<DocumentFile>();

DocumentFile document1 = new("document-a.pdf", 100, 200, 300);
DocumentFile document2 = new("document-b.pdf", 200, 300, 400);
DocumentFile document2Fail = new("document-b.pdf", 200, 300, 400, true);
DocumentFile document3 = new("document-c.pdf", 300, 400, 500);
DocumentFile document4 = new("document-d.pdf", 400, 500, 600);
DocumentFile document4Fail = new("document-d.pdf", 400, 500, 600, true);
DocumentFile document5 = new("document-e.pdf", 500, 600, 700);

documentListA.AddRange(document1, document2, document3, document4, document5);
documentListB.AddRange(document1, document2Fail, document3, document4Fail, document5);

IReadOnlyCollection<DocumentFile> documentsA = documentListA;
IReadOnlyCollection<DocumentFile> documentsB = documentListB;


DummyDocumentValidator documentValidator = new();
DummyDocumentExtractor documentExtractor = new();
DummyDocumentIndexer documentIndexer = new();

DummyParseDocumentService parseDocumentService = new(documentValidator, documentExtractor, documentIndexer);

MainScenarioPresenter presenter = new(new MainScenario(parseDocumentService));

using CancellationTokenSource cts = new CancellationTokenSource();

Task experimentA = presenter.ShowAsync(documentsA, 3, CancellationToken.None);

await experimentA;

Console.WriteLine($"Status: {experimentA.Status}");
Console.WriteLine($"IsCompleted: {experimentA.IsCompleted}");
Console.WriteLine($"IsCanceled: {experimentA.IsCanceled}");
Console.WriteLine($"IsFaulted: {experimentA.IsFaulted}");

Task experimentB = presenter.ShowAsync(documentsB, 3, CancellationToken.None);
await experimentB;

Console.WriteLine($"Status: {experimentA.Status}");
Console.WriteLine($"IsCompleted: {experimentA.IsCompleted}");
Console.WriteLine($"IsCanceled: {experimentA.IsCanceled}");
Console.WriteLine($"IsFaulted: {experimentA.IsFaulted}");
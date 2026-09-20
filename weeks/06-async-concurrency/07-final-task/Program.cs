List<DocumentFile> documentList = new List<DocumentFile>();

DocumentFile documentA = new("document-a.pdf", 100, 200, 300);
DocumentFile documentB = new("document-b.pdf", 200, 300, 400);
DocumentFile documentC = new("document-c.pdf", 300, 400, 500);
DocumentFile documentD = new("document-d.pdf", 400, 500, 600);
DocumentFile documentE = new("document-e.pdf", 500, 600, 700);

documentList.AddRange(documentA, documentB, documentC, documentD, documentE);

IReadOnlyCollection<DocumentFile> documents = documentList;


DummyDocumentValidator documentValidator = new();
DummyDocumentExtractor documentExtractor = new();
DummyDocumentIndexer documentIndexer = new();

DummyParseDocumentService parseDocumentService = new(documentValidator, documentExtractor, documentIndexer);

MainScenarioPresenter presenter = new(new MainScenario(parseDocumentService));

using CancellationTokenSource cts = new CancellationTokenSource();
//Task task = presenter.ShowAsync(documents, 2, cts.Token);

Task experimentA = presenter.ShowAsync(documents, 3, CancellationToken.None);

await experimentA;

Console.WriteLine($"Status: {experimentA.Status}");
Console.WriteLine($"IsCompleted: {experimentA.IsCompleted}");
Console.WriteLine($"IsCanceled: {experimentA.IsCanceled}");
Console.WriteLine($"IsFaulted: {experimentA.IsFaulted}");
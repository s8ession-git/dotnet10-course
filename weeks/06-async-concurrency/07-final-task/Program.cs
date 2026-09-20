List<DocumentFile> documentList = new List<DocumentFile>();

DocumentFile documentA = new("document-a.pdf", 100, 200, 300);
DocumentFile documentB = new("document-b.pdf", 200, 300, 400, true);
DocumentFile documentC = new("document-c.pdf", 300, 400, 500);

documentList.AddRange(documentA, documentB, documentC);

IReadOnlyCollection<DocumentFile> documents = documentList;


DummyDocumentValidator documentValidator = new();
DummyDocumentExtractor documentExtractor = new();
DummyDocumentIndexer documentIndexer = new();

DummyParseDocumentService parseDocumentService = new(documentValidator, documentExtractor, documentIndexer);

MainScenarioPresenter presenter = new(new MainScenario(parseDocumentService));

using CancellationTokenSource cts = new CancellationTokenSource();
Task task = presenter.ShowAsync(documents, 2, cts.Token);

await task;
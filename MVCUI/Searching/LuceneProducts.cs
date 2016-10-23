using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using ViewModel.ViewModel.Admin.Product;
using Lucene.Net.Search.Similar;

namespace MVCUI.Searching
{
    public static class LuceneProducts
    {
        #region Fields
        private static readonly Lucene.Net.Util.Version _version = Lucene.Net.Util.Version.LUCENE_30;
        private static String _path = HttpRuntime.AppDomainAppPath + $@"App_Data\Lucene_Index_Ver_{ _version }";
        private static IndexSearcher _seracher;
        private const String Id = "Id";
        private const String PrincipleImagePath = "PrincipleImagePath";
        private const String Name = "Name";
        private const String Description = "Description";
        #endregion

        #region Properties
        public static IndexSearcher Searcher
        {
            get
            {
                if (_seracher == null)
                    _seracher = new IndexSearcher(FSDirectory.Open(new DirectoryInfo(_path)), true);
                return _seracher;
            }
        }
        #endregion

        #region Mapping
        static Document MapProductToDocument(ProductLuceneViewModel viewModel)
        {
            Document productDocument = new Document();
            productDocument.Add(new Field(Id, viewModel.Id.ToString(CultureInfo.InvariantCulture), Field.Store.YES, Field.Index.NOT_ANALYZED));
            productDocument.Add(new Field(PrincipleImagePath, viewModel.ImagePath, Field.Store.YES, Field.Index.NOT_ANALYZED));
            Field nameField = new Field(Name, viewModel.Name, Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.WITH_POSITIONS_OFFSETS) { Boost = 3 };
            productDocument.Add(nameField);
            productDocument.Add(new Field(Description, viewModel.Description, Field.Store.NO, Field.Index.ANALYZED,
              Field.TermVector.WITH_POSITIONS_OFFSETS));
            return productDocument;
        }
        #endregion

        #region Search Result With Lucene
        public static IList<ProductSearchResultViewModel> GetTermsScored(String inputText, Int32 maxItem = 10)
        {
            List<ProductSearchResultViewModel> resultList = new List<ProductSearchResultViewModel>();
            inputText = inputText.ApplyCorrectPersianLucene();
            if (String.IsNullOrEmpty(inputText.Replace("*", "").Replace("?", "")))
                return resultList;
            inputText = inputText.ApplyCorrectPersianLucene();

            StandardAnalyzer analyzer = new StandardAnalyzer(_version, GetStopWords());
            TopDocs result = Searcher.Search(new PrefixQuery(new Lucene.Net.Index.Term(Name, inputText)), null, maxItem);

            if (result.TotalHits.Equals(0))
                result = Searcher.Search(new PrefixQuery(new Lucene.Net.Index.Term(Description, inputText)), null, maxItem);

            if (result.TotalHits == 0)
            {
                MultiFieldQueryParser parser = new MultiFieldQueryParser(_version, new[] { Name, Description }, analyzer);
                Query searchQuery = ParseQuery(inputText, parser);
                result = Searcher.Search(searchQuery, maxItem);
                if (result.TotalHits == 0)
                {
                    inputText = SearchByPartialWords(inputText);
                    searchQuery = ParseQuery(inputText, parser);
                    result = Searcher.Search(searchQuery, maxItem);
                }
            }

            resultList.AddRange(result.ScoreDocs.Select(x => new ProductSearchResultViewModel()
            {
                Id = Int64.Parse(Searcher.Doc(x.Doc).Get(Id)),
                Name = Searcher.Doc(x.Doc).Get(Name),
                ImagePath = Searcher.Doc(x.Doc).Get(PrincipleImagePath)
            }));
            return resultList;
        }

        #endregion

        #region Create Index
        public static void CreateIndexes(IEnumerable<ProductLuceneViewModel> viewModel)
        {
            if (!System.IO.Directory.Exists(_path))
                System.IO.Directory.CreateDirectory(_path);

            FSDirectory directory = FSDirectory.Open(new DirectoryInfo(_path));
            StandardAnalyzer analyzer = new StandardAnalyzer(_version, GetStopWords());
            using (IndexWriter writerIndex = new IndexWriter(directory, analyzer, create: true, mfl: IndexWriter.MaxFieldLength.UNLIMITED))
            {
                foreach (ProductLuceneViewModel item in viewModel)
                    writerIndex.AddDocument(MapProductToDocument(item));

                writerIndex.Optimize();
                writerIndex.Commit();
                writerIndex.Dispose();
                directory.Dispose();
            }
        }
        #endregion

        #region add-delete-update
        public static Boolean ClearLuceneIndex()
        {
            try
            {
                FSDirectory directory = FSDirectory.Open(new DirectoryInfo(_path));
                StandardAnalyzer analyzer = new StandardAnalyzer(_version, GetStopWords());

                using (IndexWriter indexWriter = new IndexWriter(directory, analyzer, create: false, mfl: IndexWriter.MaxFieldLength.UNLIMITED))
                {
                    indexWriter.DeleteAll();
                    indexWriter.Commit();
                    analyzer.Dispose();
                    indexWriter.Dispose();
                    directory.Dispose();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static void UpdateIndex(ProductLuceneViewModel viewModel)
        {
            FSDirectory directory = FSDirectory.Open(new DirectoryInfo(_path));
            StandardAnalyzer analyzer = new StandardAnalyzer(_version, GetStopWords());

            using (IndexWriter indexWriter = new IndexWriter(directory, analyzer, create: false, mfl: IndexWriter.MaxFieldLength.UNLIMITED))
            {
                Document newDoc = MapProductToDocument(viewModel);

                indexWriter.UpdateDocument(new Term(Id, viewModel.Id.ToString(CultureInfo.InvariantCulture)), newDoc);
                indexWriter.Commit();
                indexWriter.Dispose();
                directory.Dispose();
            }
        }
        public static void DeleteIndex(Int64 id)
        {
            FSDirectory directory = FSDirectory.Open(new DirectoryInfo(_path));
            StandardAnalyzer analyzer = new StandardAnalyzer(_version, GetStopWords());

            using (IndexWriter indexWriter = new IndexWriter(directory, analyzer, create: false, mfl: IndexWriter.MaxFieldLength.UNLIMITED))
            {
                indexWriter.DeleteDocuments(new Term(Id, id.ToString(CultureInfo.InvariantCulture)));
                indexWriter.Commit();
                indexWriter.Optimize();
                indexWriter.Dispose();
                directory.Dispose();
            }
        }
        public static void AddIndex(ProductLuceneViewModel viewModel)
        {
            if (!System.IO.Directory.Exists(_path))
                System.IO.Directory.CreateDirectory(_path);

            FSDirectory directory = FSDirectory.Open(new DirectoryInfo(_path));
            StandardAnalyzer analyzer = new StandardAnalyzer(_version, GetStopWords());

            using (IndexWriter writer = new IndexWriter(directory, analyzer, create: false, mfl: IndexWriter.MaxFieldLength.UNLIMITED))
            {
                writer.AddDocument(MapProductToDocument(viewModel));
                writer.Optimize();
                writer.Commit();
                writer.Dispose();
                directory.Dispose();
            }
        }
        #endregion

        #region Optimization
        public static void Optimize()
        {
            FSDirectory directory = FSDirectory.Open(new DirectoryInfo(_path));
            StandardAnalyzer analyzer = new StandardAnalyzer(_version);
            using (IndexWriter writer = new IndexWriter(directory, analyzer, IndexWriter.MaxFieldLength.UNLIMITED))
            {
                analyzer.Close();
                writer.Optimize();
                writer.Dispose();
                directory.Dispose();
            }
        }
        #endregion

        #region Query Parser
        private static Query ParseQuery(String inputText, QueryParser parser)
        {
            Query query;
            try
            {
                query = parser.Parse(inputText.Trim());
            }
            catch (ParseException)
            {
                query = parser.Parse(QueryParser.Escape(inputText.Trim()));
            }
            return query;
        }

        private static String SearchByPartialWords(String inputText)
        {
            inputText = inputText.Replace("*", "").Replace("?", "");
            IEnumerable<String> terms = inputText.Trim().Replace("-", "").Split(' ')
                .Where(x => !String.IsNullOrEmpty(x))
                .Select(x => x.Trim() + "*");
            inputText = String.Join(" ", terms);
            return inputText;
        }

        #endregion

        #region Stop Words
        private static HashSet<string> GetStopWords()
        {
            var result = new HashSet<string>();
            var stopWords = new[]
            {
                "به",
                "با",
                "از",
                "تا",
                "و",
                "است",
                "هست",
                "هستم",
                "هستیم",
                "هستید",
                "هستند",
                "نیست",
                "نیستم",
                "نیستیم",
                "نیستند",
                "اما",
                "یا",
                "این",
                "آن",
                "اینجا",
                "آنجا",
                "بود",
                "باد",
                "برای",
                "که",
                "دارم",
                "داری",
                "دارد",
                "داریم",
                "دارید",
                "دارند",
                "چند",
                "را",
                "ها",
                "های",
                "می",
                "هم",
                "در",
                "باشم",
                "باشی",
                "باشد",
                "باشیم",
                "باشید",
                "باشند",
                "اگر",
                "مگر",
                "بجز",
                "جز",
                "الا",
                "اینکه",
                "چرا",
                "کی",
                "چه",
                "چطور",
                "چی",
                "چیست",
                "آیا",
                "چنین",
                "اینچنین",
                "نخست",
                "اول",
                "آخر",
                "انتها",
                "صد",
                "هزار",
                "میلیون",
                "ملیون",
                "میلیارد",
                "ملیارد",
                "یکهزار",
                "تریلیون",
                "تریلیارد",
                "میان",
                "بین",
                "زیر",
                "بیش",
                "روی",
                "ضمن",
                "همانا",
                "ای",
                "بعد",
                "پس",
                "قبل",
                "پیش",
                "هیچ",
                "همه",
                "واما",
                "شد",
                "شده",
                "شدم",
                "شدی",
                "شدیم",
                "شدند",
                "یک",
                "یکی",
                "نبود",
                "میکند",
                "میکنم",
                "میکنیم",
                "میکنید",
                "میکنند",
                "میکنی",
                "طور",
                "اینطور",
                "آنطور",
                "هر",
                "حال",
                "مثل",
                "خواهم",
                "خواهی",
                "خواهد",
                "خواهیم",
                "خواهید",
                "خواهند",
                "داشته",
                "داشت",
                "داشتی",
                "داشتم",
                "داشتیم",
                "داشتید",
                "داشتند",
                "آنکه",
                "مورد",
                "کنید",
                "کنم",
                "کنی",
                "کنند",
                "کنیم",
                "نکنم",
                "نکنی",
                "نکند",
                "نکنیم",
                "نکنید",
                "نکنند",
                "نکن",
                "بگو",
                "نگو",
                "مگو",
                "بنابراین",
                "بدین",
                "من",
                "تو",
                "او",
                "ما",
                "شما",
                "ایشان",
                "ی",
                "ـ",
                "هایی",
                "خیلی",
                "بسیار",
                "1",
                "بر",
                "l",
                "شود",
                "کرد",
                "کرده",
                "نیز",
                "خود",
                "شوند",
                "اند",
                "داد",
                "دهد",
                "گشت",
                "ز",
                "گفت",
                "آمد",
                "اندر",
                "چون",
                "بد",
                "چو",
                "همی",
                "پر",
                "سوی",
                "دو",
                "گر",
                "بی",
                "گرد",
                "زین",
                "کس",
                "زان",
                "جای",
                "آید"
            };
            foreach (var item in stopWords)
                result.Add(item);
            return result;
        }
        #endregion

        #region More Like
        private static Int32 GetLuceneDocumentNumber(Int64 id)
        {
            StandardAnalyzer analyzer = new StandardAnalyzer(_version);
            QueryParser parser = new QueryParser(_version, Id, analyzer);
            Query query = ParseQuery(id.ToString(CultureInfo.InvariantCulture), parser);
            TopDocs doc = Searcher.Search(query, 1);
            return doc.TotalHits == 0 ? 0 : doc.ScoreDocs[0].Doc;
        }

        private static Query CreateMoreLikeThisQuery(Int64 id)
        {
            Int32 docNum = GetLuceneDocumentNumber(id);
            if (docNum == 0)
                return null;

            StandardAnalyzer analyzer = new StandardAnalyzer(_version, GetStopWords());
            IndexReader reader = Searcher.IndexReader;
            MoreLikeThis MoreLikeThis = new MoreLikeThis(reader) { Analyzer = analyzer };
            MoreLikeThis.SetFieldNames(new[] { Name, Description });
            MoreLikeThis.MinDocFreq = 1;
            MoreLikeThis.MinTermFreq = 1;
            MoreLikeThis.Boost = true;

            return MoreLikeThis.Like(docNum);
        }

        public static IList<ProductSearchResultViewModel> ShowMoreLikeThisPostItems(Int64 id)
        {
            Query query = CreateMoreLikeThisQuery(id);
            if (query == null)
                return null;

            TopDocs hitResult = Searcher.Search(query, n: 7);
            IList<ProductSearchResultViewModel> result = hitResult.ScoreDocs.Select(x => Searcher.Doc(x.Doc))
                .Select(x => new ProductSearchResultViewModel()
                {
                    Id = Int64.Parse(x.Get(Id)),
                    Name = x.Get(Name),
                    ImagePath = x.Get(PrincipleImagePath)
                }).ToList();
            return result;
        }
        #endregion
    }
}

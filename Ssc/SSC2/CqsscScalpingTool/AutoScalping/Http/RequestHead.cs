namespace AutoScalping.Http
{
    public class RequestHead : HttpHead
    {
        public RequestHead(string encoding,
        string transcode,
        string storeId,
        string version) : base(encoding,
        transcode,
        storeId,
        version)
        { }
    }
}

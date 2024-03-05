namespace SecondProject.Wrapper
{
    // basic wrapper
    public class Response<T> where T: class
    {
        public T Data { get; set; }
        public bool success { get; set; }   

        public Response()
        {


        }
        public Response(T data)
        {
            Data = data;
            success = true;

        }
    }
}

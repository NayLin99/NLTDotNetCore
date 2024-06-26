using Microsoft.AspNetCore.Mvc;
using NLTDotNetCore.RestApi.Models;
using NLTDotNetCore.Shared;

namespace NLTDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogAdoDotNet2Controller : ControllerBase
    {
        // private readonly AdoDotNetService _adoDotNetService =
        //     new AdoDotNetService(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);

        private readonly AdoDotNetService _adoDotNetService;

        public BlogAdoDotNet2Controller(AdoDotNetService adoDotNetService)
        {
            _adoDotNetService = adoDotNetService;
        }

        [HttpGet]
        public IActionResult GetBlogs()
        {
            string query = "SELECT * FROM Tbl_Blog";

            var lst = _adoDotNetService.Query<BlogModel>(query);

            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            string query = "SELECT * FROM Tbl_Blog WHERE BlogId = @BlogId";

            // AdoDotNetParameter[] parameters = new AdoDotNetParameter[1];
            // parameters[0] = new AdoDotNetParameter("@BlogId", id);
            // var lst = _adoDotNetService.Query<BlogModel>(query, parameters);

            var item = _adoDotNetService.QueryFirstOrDefault<BlogModel>(query, new AdoDotNetParameter("@BlogId", id));

            if (item is null)
            {
                return NotFound("No data found.");
            }

            return Ok(item);
        }

        [HttpPost]
        public IActionResult CreateBlog(BlogModel blog)
        {
            string query = @"INSERT INTO Tbl_Blog 
                            (BlogTitle, 
                             BlogAuthor, 
                             BlogContent) 
                         VALUES 
                            (@BlogTitle, 
                             @BlogAuthor, 
                             @BlogContent)";

            int result = _adoDotNetService.Execute(query,
                new AdoDotNetParameter("@BlogTitle", blog.BlogTitle),
                new AdoDotNetParameter("@BlogAuthor", blog.BlogAuthor),
                new AdoDotNetParameter("@BlogContent", blog.BlogContent));

            string message = result > 0 ? "Saving Successful." : "Saving Failed.";
            return Ok(message);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, BlogModel blog)
        {
            var item = FindById(id);

            if (item == 0)
            {
                return NotFound("No Data Found.");
            }

            string query = @"UPDATE Tbl_Blog
                            SET
                                BlogTitle = @BlogTitle,
                                BlogAuthor = @BlogAuthor,
                                BlogContent = @BlogContent
                            WHERE
                                BlogId = @BlogId";

            int result = _adoDotNetService.Execute(query,
                    new AdoDotNetParameter("@BlogId", id),
                    new AdoDotNetParameter("@BlogTitle", blog.BlogTitle),
                    new AdoDotNetParameter("@BlogAuthor", blog.BlogAuthor),
                    new AdoDotNetParameter("@BlogContent", blog.BlogContent))
                ;

            string message = result > 0 ? "Updating Successful." : "Updating Failed.";
            return Ok(message);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, BlogModel blog)
        {
            var item = FindById(id);

            if (item == 0)
            {
                return NotFound("No Data Found.");
            }

            string conditions = string.Empty;
            var parametersList = new List<AdoDotNetParameter> { new AdoDotNetParameter("@BlogId", id) };

            if (!string.IsNullOrEmpty(blog.BlogTitle))
            {
                conditions += "BlogTitle = @BlogTitle, ";
                parametersList.Add(new AdoDotNetParameter("@BlogTitle", blog.BlogTitle));
            }

            if (!string.IsNullOrEmpty(blog.BlogAuthor))
            {
                conditions += "BlogAuthor = @BlogAuthor, ";
                parametersList.Add(new AdoDotNetParameter("@BlogAuthor", blog.BlogAuthor));
            }

            if (!string.IsNullOrEmpty(blog.BlogContent))
            {
                conditions += "BlogContent = @BlogContent, ";
                parametersList.Add(new AdoDotNetParameter("@BlogContent", blog.BlogContent));
            }

            if (conditions.Length == 0)
            {
                return NotFound("No data to update.");
            }

            conditions = conditions.Substring(0, conditions.Length - 2);

            string query = $"UPDATE Tbl_Blog SET {conditions} WHERE BlogId = @BlogId";

            int result = _adoDotNetService.Execute(query, parametersList.ToArray());

            string message = result > 0 ? "Updating Successful." : "Updating Failed.";
            return Ok(message);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            var item = FindById(id);

            if (item == 0)
            {
                return NotFound("No Data Found.");
            }

            string query = @"DELETE FROM 
                            Tbl_Blog
                         WHERE 
                            BlogId = @BlogId";

            int result = _adoDotNetService.Execute(query, new AdoDotNetParameter("@BlogId", id));

            string message = result > 0 ? "Deleting Successful." : "Deleting Failed.";
            return Ok(message);
        }

        private int FindById(int id)
        {
            string query = "SELECT * FROM Tbl_Blog WHERE BlogId = @BlogId";

            int result = _adoDotNetService.Execute(query, new AdoDotNetParameter("@BlogId", id));

            return result;
        }
    }
}